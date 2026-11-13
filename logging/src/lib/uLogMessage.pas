unit uLogMessage;

interface

uses
  classes, messages, windows;

const
  LOGMSG_MAXCATEGORYID = 100;

var
  WM_LOG_MESSAGE: Cardinal;

type
  TLogMsgPriority = (lmpNone, lmpLow, lmpMedium, lmpHigh);
  TLogMsgSeverity = (lmsNone, lmsMinor, lmsMajor, lmsCritical, lmsFatal);
  TLogMsgGroup = (lmgNone, lmgHint, lmgWarning, lmgError, lmgException);
  TLogMsgCategory = set of 1..LOGMSG_MAXCATEGORYID;

  TLogMessage = class(TObject)
  private
    FTimestamp: TDateTime;
    FSource: string;
    FUserMsg: string;
    FPriority: TLogMsgPriority;
    FSeverity: TLogMsgSeverity;
    FGroup: TlogMsgGroup;
    FCategory: TLogMsgCategory;
    FSystemMsg: string;
    FCallStack: string;
    FDetails: TStringList;
  protected
    function GetCallStack: string;
    function GetCategory: TLogMsgCategory;
    function GetCategoryStr: string;
    function GetTimestamp: TDateTime;
    function GetDetails: TStringList;
    function GetGroup: TlogMsgGroup;
    function GetPriority: TLogMsgPriority;
    function GetSeverity: TLogMsgSeverity;
    function GetSource: string;
    function GetSystemMsg: string;
    function GetUserMsg: string;

    procedure SetCallStack(const newValue: string);
    procedure SetCategory(const newValue: TLogMsgCategory);
    procedure SetCategoryStr(const newValue: string);
    procedure SetDetails(const newValue: TStringList);
    procedure SetGroup(const newValue: TlogMsgGroup);
    procedure SetPriority(const newValue: TLogMsgPriority);
    procedure SetSeverity(const newValue: TLogMsgSeverity);
    procedure SetSource(const newValue: string);
    procedure SetSystemMsg(const newValue: string);
    procedure SetUserMsg(const newValue: string);
  public
    constructor Create; overload;
    constructor Create(Src: string; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    destructor Destroy; override;
    function Match(Msg: TLogMessage): boolean; 
    procedure Post; overload;
    class procedure Post(Src: TObject; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    class procedure Post(Src: TClass; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    class procedure Post(Src: string; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;

    property Timestamp: TDateTime read GetTimestamp; 
    property Source: string read GetSource write SetSource;
    property UserMsg: string read GetUserMsg write SetUserMsg;
    property Priority: TLogMsgPriority read GetPriority write SetPriority;
    property Severity: TLogMsgSeverity read GetSeverity write SetSeverity;
    property Group: TlogMsgGroup read GetGroup write SetGroup;
    property Category: TLogMsgCategory read GetCategory write SetCategory;
    property CategoryStr: string read GetCategoryStr write SetCategoryStr; 
    property SystemMsg: string read GetSystemMsg write SetSystemMsg;
    property CallStack: string read GetCallStack write SetCallStack;
    property Details: TStringList read GetDetails write SetDetails;
  end;

implementation

uses
  SysUtils, TypInfo;

{$IFOPT W+}
  {$DEFINE STACKFRAMES_ON}
  {$W-}
{$ENDIF}

function GetCurrReturnAddr: Pointer;
var
  Addr: Pointer;
begin
  asm
    mov edx, [ebp+4];    // EDX := (EBP+4)^;
    mov Addr, edx;      // Address := EDX;
  end;
  Result := Addr;
end;

function GetCurrInstructionAddr: Pointer;
asm
  mov eax, [esp];      // Result := ESP^;  // Return Address
end;

{$IFDEF STACKFRAMES_ON}
  {$W+}
  {$UNDEF STACKFRAMES_ON}
{$ENDIF}

{ TLogMessage }

constructor TLogMessage.Create(Src: string; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = '');
begin
  Create;

  Source := Src;
  UserMsg := UsrMsg;
  Priority := P;
  Severity := S;
  Group := G;
  SystemMsg := SysMsg;
end;

destructor TLogMessage.Destroy;
var
  i: integer;
begin
  if Details <> nil then begin
    for i := 0 to Details.Count - 1 do begin
      if Details.Objects[i] <> nil then
        Details.Objects[i].Free;
    end;
    Details.Clear;
    Details.Free;
  end;
  
  inherited;
end;

constructor TLogMessage.Create();
begin
  FTimestamp := Now;
  FCategory := []; 
end;

function TLogMessage.GetCallStack: string;
begin
  Result := FCallStack;
end;

function TLogMessage.GetCategory: TLogMsgCategory;
begin
  Result := FCategory;
end;

function TLogMessage.GetDetails: TStringList;
begin
  Result := FDetails;
end;

function TLogMessage.GetGroup: TlogMsgGroup;
begin
  Result := FGroup;
end;

function TLogMessage.GetPriority: TLogMsgPriority;
begin
  Result := FPriority;
end;

function TLogMessage.GetSeverity: TLogMsgSeverity;
begin
  Result := FSeverity;
end;

function TLogMessage.GetSource: string;
begin
  Result := FSource;
end;

function TLogMessage.GetSystemMsg: string;
begin
  Result := FSystemMsg;
end;

function TLogMessage.GetUserMsg: string;
begin
  Result := FUserMsg;
end;

procedure TLogMessage.Post;
begin
  PostMessage(HWND_BROADCAST, WM_LOG_MESSAGE, Integer(self), 0);
end;

class procedure TLogMessage.Post(Src: TObject; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = '');
var
  srcName: string;
  unitName: string;
  methodName: string;
begin
  if Src <> nil then begin
    unitName := Src.ClassName;
    methodName := Src.MethodName(GetCurrInstructionAddr);
  end;
  srcName := Format('[%s.%s (%p, %p)]', [unitName, methodName, GetCurrInstructionAddr, GetCurrReturnAddr]);
  Post(unitName, UsrMsg, G, P, S, SysMsg);
end;

class procedure TLogMessage.Post(Src: TClass; UsrMsg: string; G: TlogMsgGroup; P: TLogMsgPriority; S: TLogMsgSeverity; SysMsg: string);
var
  srcName: string;
  unitName: string;
  methodName: string;
begin
  if Src <> nil then begin
    unitName := Src.ClassName;
    methodName := Src.MethodName(GetCurrInstructionAddr);
  end;
  srcName := Format('[%s.%s (%p, %p)]', [unitName, methodName, GetCurrInstructionAddr, GetCurrReturnAddr]);
  Post(unitName, UsrMsg, G, P, S, SysMsg);
end;

function TLogMessage.Match(Msg: TLogMessage): boolean;
begin
  Result := True;

  // TODO: Use regex
  if Source <> '' then
    Result := Result and (Pos(Source, Msg.Source) > 0);

  if UserMsg <> '' then
    Result := Result and (Pos(UserMsg, Msg.UserMsg) > 0);

  if Priority > lmpNone then
    Result := Result and (Priority = Msg.Priority);

  if Severity > lmsNone then
    Result := Result and (Severity = Msg.Severity);

  if Group > lmgNone then
    Result := Result and (Group = Msg.Group);

  if Category <> [] then
    Result := Result and ((Category * Msg.Category) <> []);

  if SystemMsg <> '' then
    Result := Result and (Pos(SystemMsg, Msg.SystemMsg) > 0);
end;

class procedure TLogMessage.Post(Src, UsrMsg: string; G: TlogMsgGroup; P: TLogMsgPriority; S: TLogMsgSeverity; SysMsg: string);
begin
  TLogMessage.Create(Src, UsrMsg, G, P, S, SysMsg).Post;
  // Do not free the message; it is the responsibility of the TLogManager
end;

procedure TLogMessage.SetCallStack(const newValue: string);
begin
  FCallStack := newValue;
end;

procedure TLogMessage.SetCategory(const newValue: TLogMsgCategory);
begin
  FCategory := newValue;
end;

procedure TLogMessage.SetDetails(const newValue: TStringList);
begin
  FDetails := newValue;
end;

procedure TLogMessage.SetGroup(const newValue: TLogMsgGroup);
begin
  if (Ord(Low(FGroup)) <= Ord(newValue)) and (Ord(newValue) <= Ord(High(FGroup))) then
    FGroup := newValue;
end;

procedure TLogMessage.SetPriority(const newValue: TLogMsgPriority);
begin
  if (Ord(Low(FPriority)) <= Ord(newValue)) and (Ord(newValue) <= Ord(High(FPriority))) then
    FPriority := newValue;
end;

procedure TLogMessage.SetSeverity(const newValue: TLogMsgSeverity);
begin
  if (Ord(Low(FSeverity)) <= Ord(newValue)) and (Ord(newValue) <= Ord(High(FSeverity))) then
    FSeverity := newValue;
end;

procedure TLogMessage.SetSource(const newValue: string);
begin
  FSource := newValue;
end;

procedure TLogMessage.SetSystemMsg(const newValue: string);
begin
  FSystemMsg := newValue;
end;

procedure TLogMessage.SetUserMsg(const newValue: string);
begin
  FUserMsg := newValue;
end;

function TLogMessage.GetTimestamp: TDateTime;
begin
  Result := FTimestamp; 
end;

function TLogMessage.GetCategoryStr: string;
var
  i: integer;
begin
  Result := '';
  for i := 0 to LOGMSG_MAXCATEGORYID do begin
    if i in Category then
      Result := Result + IntToStr(i) + ', ';
  end;
  Result := Copy(Result, 1, LastDelimiter(',', Result) - 1);
end;

procedure TLogMessage.SetCategoryStr(const newValue: string);
var
  i: integer; 
  tokens: TStrings;
begin
  FCategory := [];
  tokens := TStringList.Create;
  tokens.CommaText := newValue;
  for i := 0 to tokens.Count - 1 do begin
    try
      Include(FCategory, StrToInt(tokens[i]));
    except
    end;
  end;
end;

initialization
  WM_LOG_MESSAGE := RegisterWindowMessage('LogMsg');

end.
