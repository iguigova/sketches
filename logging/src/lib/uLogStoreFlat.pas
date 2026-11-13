unit uLogStoreFlat;

interface

uses
  classes, contnrs, uLogStore, uLogMessage;

type
  // TLogStoreFlat = class(TInterfacedPersistent, ILogStore)
  TLogStoreFlat = class(TLogStoreAbstract)
  private
    FDelimiter: string;
    FEncryption: integer;
    FVerbose: integer;
    FPath: string;
    FMinSize: integer;
    FMaxSize: integer;
    FIncludeOnFilter: boolean;
    FFilters: TObjectList;
    FFileStream: TFileStream;
  protected
    function Parse(Msg: TLogMessage): string; virtual;
    function Include(Msg: TLogMessage): boolean; virtual;
    procedure AppendToText(var Text: string; const TextID: integer; const TextValue: string);
  public
    destructor Destroy; override;
    procedure Purge; override;
    procedure Push(Msg: TLogMessage); override;
    procedure Setup(
      Path: string;
      Filters: TObjectList;
      IncludeOnFilter: boolean;
      Delimiter: string = LOGDEFAULT_DELIMITER;
      Encryption: integer = LOGDEFAULT_ENCRYPTION;
      Verbose: integer = LOGDEFAULT_VERBOSE;
      MinSize: integer = LOGDEFAULT_MINSIZE;
      MaxSize: integer = LOGDEFAULt_MAXSIZE); override;
  end;

implementation

uses
  SysUtils;

{ TLogStoreFlat }

procedure TLogStoreFlat.AppendToText(var Text: string; const TextID: integer; const TextValue: string);
begin
  // TODO: Consider the value of FEncryption
  if ((FVerbose = 0) and (TextValue <> '')) or
     ((LOGDEFAULT_TOKEN_VERBOSEBITS[TextID] and FVerbose) = LOGDEFAULT_TOKEN_VERBOSEBITS[TextID]) then
    Text := Text + LOGDEFAULT_TOKEN_NAMES[TextID] + TextValue + FDelimiter;
end;

destructor TLogStoreFlat.Destroy;
begin
  FFileStream.Free;
  FFilters.Free;
  inherited;
end;

function TLogStoreFlat.Include(Msg: TLogMessage): boolean;
var
  idx: integer;
begin
  Result := True;
  if (FFilters <> nil) then begin
    Result := False; 
    for idx := 0 to FFilters.Count - 1 do begin
      Result := Result or (FFilters.Items[idx] as TLogMessage).Match(Msg);
    end;
  end;
  // IncludeOnFiler = True => Exclude all messages except the ones that pass a filter
  // InlcudeOnFilter = False => Include all messages except the ones that pass a filter
  Result := not(Result xor FIncludeOnFilter);
end;

function TLogStoreFlat.Parse(Msg: TLogMessage): string;
begin
  AppendToText(Result, 0, FormatDateTime('yyyy/mm/dd hh:mm:ss', Msg.Timestamp));
  AppendToText(Result, 1, Msg.UserMsg);
  AppendToText(Result, 2, Msg.Source);
  AppendToText(Result, 3, IntToStr(Ord(Msg.Priority)));
  AppendToText(Result, 4, IntToStr(Ord(Msg.Severity)));
  AppendToText(Result, 5, IntToStr(Ord(Msg.Group)));
  AppendToText(Result, 6, Msg.CategoryStr);
  AppendToText(Result, 7, Msg.SystemMsg);
  AppendToText(Result, 8, Msg.CallStack);
  if Msg.Details <> nil then
    AppendToText(Result, 9, Msg.Details.CommaText);
end;

procedure TLogStoreFlat.Purge;
begin
  if (FFileStream.Handle > 0) and (FFileStream.Seek(0, soFromCurrent) > FMaxSize) then
    FFileStream.Seek(0, soFromBeginning);
end;

procedure TLogStoreFlat.Push(Msg: TLogMessage);
var
  s: string;
begin
  if Msg <> nil then begin
    if (FFileStream.Handle > 0) and (Include(Msg)) then begin
      s := Parse(Msg);
      FFileStream.Write(s[1], Length(s));
      FFileStream.Write(LOGDEFAULT_LINEFEED[1], Length(LOGDEFAULT_LINEFEED));
    end;
  end;
  Purge;
end;

procedure TLogStoreFlat.Setup(
      Path: string;
      Filters: TObjectList;
      IncludeOnFilter: boolean;
      Delimiter: string = LOGDEFAULT_DELIMITER;
      Encryption: integer = LOGDEFAULT_ENCRYPTION;
      Verbose: integer = LOGDEFAULT_VERBOSE;
      MinSize: integer = LOGDEFAULT_MINSIZE;
      MaxSize: integer = LOGDEFAULt_MAXSIZE);
begin
  FPath := Path;
  FFilters := Filters;
  FIncludeOnFilter := IncludeOnFilter;
  if Delimiter = '' then
    FDelimiter := LOGDEFAULT_DELIMITER
  else
    FDelimiter := Delimiter;
  FEncryption := Encryption;
  FVerbose := Verbose;
  FMinSize := MinSize;
  FMaxSize := MaxSize;
  if not FileExists(FPath) then begin
    try
      FFileStream := TFileStream.Create(FPath, fmCreate);
    finally
      FFileStream.Free;
    end;
  end;
  FFileStream := TFileStream.Create(FPath, fmOpenReadWrite or fmShareDenyNone);
  FFileStream.Seek(0, soFromEnd);  
end;

end.
