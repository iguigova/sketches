unit uLogManager;

interface

uses
  classes, contnrs, messages, windows, uLogMessage;

type
  TLogManager = class(TThread)
  private
    FTerminated: boolean;
    FMessages: TObjectList;
    FStores: TObjectList;
    FWinHandle: HWND;
  protected
    constructor Create;
    function GetMessages: TObjectList;
    function GetStores: TObjectList;
    procedure Execute; override;
    procedure WndProc(var msg: TMessage);
    // procedure DeallocateHWnd(Wnd: HWND);

    property Messages: TObjectList read GetMessages;
    property Stores: TObjectList read GetStores;
  public
    destructor Destroy; override;  
  end;

implementation

uses
  ActiveX, MSXML, Sysutils, Variants, uLogStore, uLogStoreFlat;

const
  LOG_CONFIGFILE = 'LogConfig.xml';
  LOG_IDLETIME = 5000;

var
  LogManager: TLogManager;

{ TLogManager }

constructor TLogManager.Create;
begin
  FTerminated := False;
  inherited Create(False);
  FWinHandle := AllocateHWND(WndProc);
end;

//procedure TLogManager.DeallocateHWnd(Wnd: HWND);
//var
//  Instance: Pointer;
//begin
//  // http://delphi.about.com/od/windowsshellapi/l/aa093003c.htm
//
//  Instance := Pointer(GetWindowLong(Wnd, GWL_WNDPROC));
//  if Instance <> @DefWindowProc then
//    // make sure we restore the old, original windows procedure before leaving
//    SetWindowLong(Wnd, GWL_WNDPROC, Longint(@DefWindowProc));
//  FreeObjectInstance(Instance);
//  DestroyWindow(Wnd);
//end;

destructor TLogManager.Destroy;
begin
  Messages.Free;
  Stores.Free;

  DeallocateHWnd(FWinHandle);
  inherited;
end;

procedure TLogManager.Execute;
var
  msg: TObject;
  idx: integer;
begin
  while not FTerminated do begin
    while Messages.Count > 0 do begin
      msg := TLogMessage(Messages.Extract(Messages.First));
      if msg is TLogMessage then begin
        for idx := 0 to Stores.Count - 1 do begin
          //if supports(Stores.Items[idx], ILogStore, store) then
          //  store.Push(msg);
          if (Stores.Items[idx] is TLogStoreAbstract) then
            (Stores.Items[idx] as TLogStoreAbstract).Push(TLogMessage(msg));
        end;
      end;
      msg.Free;
    end;
    Sleep(LOG_IDLETIME);
  end;
end;

function TLogManager.GetMessages: TObjectList;
begin
  if FMessages = nil then
    FMessages := TObjectList.Create;
    
  Result := FMessages;
end;

function TLogManager.GetStores: TObjectList;
var
  cfg: IXMLDOMDocument;
  storeList: IXMLDOMNodeList;
  storeElement: IXMLDOMElement;
  storeClass: TPersistentClass;
  store: TLogStoreAbstract; // ILogStore;
  filterList: IXMLDOMNodeList;
  filterElement: IXMLDOMElement;
  filters: TObjectList;
  filter: TLogMessage;
begin
  if FStores = nil then begin
    FStores := TObjectList.Create;

    // http://dn.codegear.com/article/29240 - Microsoft's XML parser, which the XML components use by default, is a COM-based parser, which means that COM needs to be initialized before you can use them. Delphi calls the CoInitialize function for you when an application starts up, but that function only initializes COM on the thread it was called from. You should put a call to CoInitialize at the beginning of your TThread descendant's Execute method and a matching CoUnInitialize at the end.
    CoInitialize(nil);
    cfg := CoDOMDocument.Create;
    cfg.Async := False;

    if cfg.Load(LOG_CONFIGFILE) then
    begin
      storeList := cfg.selectNodes('//Log');       // all elements of type <store>
      storeElement := storeList.nextNode as IXMLDOMElement; // go through all the nodes
      while storeElement <> nil do begin
        filterList := storeElement.SelectNodes('./Filter');
        filterElement := filterList.nextNode as IXMLDOMElement;
        filters := TObjectList.Create;
        while filterElement <> nil do begin
          filter := TLogMessage.Create;
          filter.Source := VarToStr(filterElement.getAttribute('Source'));
          filter.UserMsg := VarToStr(filterElement.getAttribute('UserMsg'));
          filter.Priority := TLogMsgPriority(StrToIntDef(VarToStr(filterElement.getAttribute('Priority')), Ord(Low(TLogMsgPriority))));
          filter.Severity := TLogMsgSeverity(StrToIntDef(VarToStr(filterElement.getAttribute('Severity')), Ord(Low(TLogMsgSeverity))));
          filter.Group := TLogMsgGroup(StrToIntDef(VarToStr(filterElement.getAttribute('Group')), Ord(Low(TLogMsgGroup))));
          filter.CategoryStr := VarToStr(filterElement.getAttribute('Category'));
          filter.SystemMsg := VarToStr(filterElement.getAttribute('SystemMsg'));
          filters.Add(filter);
          filterElement := filterList.nextNode as IXMLDOMElement;
        end;

        try
          storeClass := GetClass('TLogStore' + storeElement.getAttribute('Format'));
          // http://hallvards.blogspot.com/2004/11/object-to-interface-casts.html - Use supports to cast TObject as an interface
          // if supports(TObject(storeClass.Create), ILogStore, store) then begin
          store := storeClass.Create as TLogStoreAbstract;
          if store <> nil then begin
            store.Setup(VarToStr(storeElement.getAttribute('Path')),
                        filters,
                        (StrToIntDef(VarToStr(storeElement.getAttribute('IncludeOnFilter')), LOGDEFAULT_INCLUDEONFILTER) > LOGDEFAULT_INCLUDEONFILTER),
                        VarToStr(storeElement.getAttribute('Delimiter')),
                        StrToIntDef(VarToStr(storeElement.getAttribute('Encryption')), LOGDEFAULT_ENCRYPTION),
                        StrToIntDef(VarToStr(storeElement.getAttribute('Verbose')), LOGDEFAULT_VERBOSE),
                        StrToIntDef(VarToStr(storeElement.getAttribute('MinSize')), LOGDEFAULT_MINSIZE),
                        StrToIntDef(VarToStr(storeElement.getAttribute('MaxSize')), LOGDEFAULt_MAXSIZE));
            FStores.Add(store as storeClass);
          end;
        except
        end;
        storeElement := storeList.nextNode as IXMLDOMElement;
      end;
    end;

//    if FStores.Count < 1 then begin // create default one
//      try
//        storeClass := GetClass('TLogStore' + LOGDEFAULT_FORMAT);
//        // if supports(storeClass.Create, ILogStore, store) then begin
//        store := storeClass.Create as TLogStoreAbstract;
//        if store <> nil then begin
//          store.Setup(LOGDEFAULT_PATH, nil, True);
//          FStores.Add(store as storeClass);
//        end;
//      except
//      end;
//    end;

    CoUnInitialize;
  end;

  Result := FStores;
end;

procedure TLogManager.WndProc(var msg: TMessage);
begin
  FTerminated := (Msg.Msg = WM_NCDESTROY);

  if Msg.Msg = WM_LOG_MESSAGE then
    Messages.Add(TLogMessage(Msg.WParam))
  else
    if not FTerminated then
      Msg.Result := DefWindowProc(FWinHandle, Msg.Msg, Msg.wParam, Msg.lParam);
end;

initialization
  Classes.RegisterClass(TLogStoreFlat);
  LogManager := TLogManager.Create;

finalization
  if LogManager <> nil then
    LogManager.Free;

end.

