unit uLogCursorFlat;

interface

uses
  classes, uLogCursor, uLogMessage;

type
  TLogCursorFlat = class(TLogCursorAbstract)
  private
    FEOF: longint;
    FFileStream: TFileStream;
  protected
    procedure Reset;
    function EOF: boolean; 
  public
    constructor Create(Path: string);
    function First: TLogMessage; override;
    function Next: TLogMessage; override;
  end;

implementation

uses
  SysUtils, uLogStore;

{ TLogCursorFlat }

constructor TLogCursorFlat.Create(Path: string);
begin
// http://www.festra.com/wwwboard/messages/12989.html
  FFileStream := TFileStream.Create(Path, fmOpenReadWrite or fmShareDenyNone);
end;

function TLogCursorFlat.EOF: boolean;
begin
  Result := True;
  if FFileStream <> nil then
    Result := (FFileStream.Position < FEOF);
end;

function TLogCursorFlat.First: TLogMessage;
begin
  Reset;
  Result := Next;
end;

function TLogCursorFlat.Next: TLogMessage;
var
  msg: string;
  ch: string;
begin
  Result := nil;
  while (not EOF) and (Pos(LOGDEFAULT_LINEFEED, msg) > 0) do begin
    FFileStream.Read(ch[1], 1);
    msg := msg + ch; 
  end;
end;

procedure TLogCursorFlat.Reset;
begin
  if FFileStream <> nil then begin
    FEOF := FFileStream.Seek(0, soFromEnd);
    FFileStream.Position := 0;
  end;
end;

initialization
  RegisterClass(TLogCursorFlat);

end.
