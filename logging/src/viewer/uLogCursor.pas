unit uLogCursor;

interface

uses
  classes, uLogMessage;

type
  TLogCursorAbstract = class(TPersistent)
  public
    function First: TLogMessage; virtual; abstract;
    function Next: TLogMessage; virtual; abstract;
  end;

implementation

end.
