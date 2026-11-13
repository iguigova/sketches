unit uLogStore;

interface

uses
  contnrs, classes, uLogMessage;

const
  LOGDEFAULT_FORMAT = 'Flat';
  LOGDEFAULT_PATH = 'Log.txt';
  LOGDEFAULT_INCLUDEONFILTER = 0;
  LOGDEFAULT_ENCRYPTION = 0;
  LOGDEFAULT_VERBOSE = 0;
  LOGDEFAULT_MINSIZE = 1024;    // 1KB in bytes
  LOGDEFAULT_MAXSIZE = 1048576; // 1MB in bytes
  LOGDEFAULT_DELIMITER = #9;
  LOGDEFAULT_LINEFEED = #13#10;

  LOGDEFAULT_TOKEN_VERBOSEBITS: array[0..9] of integer = (1, 2, 4, 8, 16, 32, 64, 128, 256, 512);
  LOGDEFAULT_TOKEN_NAMES: array[0..9] of string = ('', '', 'Source: ', 'Priority: ', 'Severity: ', 'Group: ', 'Category: ', 'SystemMessage: ', 'CallStack: ', 'Details: ');

type
  ILogStore = interface(IUnknown)
    ['{85F672BE-77D6-49C0-8CCA-29F7205DE7BC}']   // Use Shift-Ctrl-G to generate the string
    procedure Purge;
    procedure Push(Msg: TLogMessage);
    procedure Setup(
      Path: string;
      Filters: TObjectList;
      IncludeOnFilter: boolean;
      Delimiter: string = LOGDEFAULT_DELIMITER;
      Encryption: integer = LOGDEFAULT_ENCRYPTION;
      Verbose: integer = LOGDEFAULT_VERBOSE;
      MinSize: integer = LOGDEFAULT_MINSIZE;
      MaxSize: integer = LOGDEFAULt_MAXSIZE);
  end;

  TLogStoreAbstract = class(TPersistent)
  public
    procedure Purge; virtual; abstract;
    procedure Push(Msg: TLogMessage); virtual; abstract;
    procedure Setup(
      Path: string;
      Filters: TObjectList;
      IncludeOnFilter: boolean;
      Delimiter: string = LOGDEFAULT_DELIMITER;
      Encryption: integer = LOGDEFAULT_ENCRYPTION;
      Verbose: integer = LOGDEFAULT_VERBOSE;
      MinSize: integer = LOGDEFAULT_MINSIZE;
      MaxSize: integer = LOGDEFAULt_MAXSIZE); virtual; abstract;
  end;

  TInterfacedPersistent = class(TPersistent, IUnknown)
  public
    function QueryInterface(const IID: TGUID; out Obj): HResult; stdcall;
    function _AddRef: Integer; stdcall;
    function _Release: Integer; stdcall;
  end;

implementation

uses
  windows;

{ TInterfacedPersistent }

function TInterfacedPersistent._AddRef: Integer;
begin
  Result := -1;
end;

function TInterfacedPersistent._Release: Integer;
begin
  Result := -1;
end;

function TInterfacedPersistent.QueryInterface(const IID: TGUID; out Obj): HResult;
begin
  if GetInterface(IID, Obj) then
    Result := S_OK
  else
    Result := E_NOINTERFACE
end;

end.
