unit uLogAgent;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, CheckLst, ExtCtrls, ComCtrls, Grids, ValEdit;

type
  TLogAgentForm = class(TForm)
    LogBtn: TButton;
    ResetBtn: TButton;
    UserMsgEdit: TLabeledEdit;
    CategoryEdit: TLabeledEdit;
    SystemMsgEdit: TLabeledEdit;
    MultiplicityEdit: TLabeledEdit;
    DetailsValueListEditor: TValueListEditor;
    PriorityCheckListBox: TCheckListBox;
    SeverityCheckListBox: TCheckListBox;
    GroupCheckListBox: TCheckListBox;
    PriorityLbl: TLabel;
    SeverityLbl: TLabel;
    GroupLbl: TLabel;
    LogAgentStatusBar: TStatusBar;

    procedure CheckListBoxClickCheck(Sender: TObject);
    procedure ResetBtnClick(Sender: TObject);
    procedure LogBtnClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);

  private
    FSessionLoggedMsgCount: integer;

    procedure Reset;
    procedure ResetCheckListBox(CheckListBox: TCheckListBox; CheckedItemIndex: integer);
    procedure ResetValueListEditor(ValueListEditor: TValueListEditor);
  end;

var
  LogAgentForm: TLogAgentForm;

implementation

uses
  uLogMessage, uLogManager;

{$R *.DFM}

procedure TLogAgentForm.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  // Release LogManager WndProc and Thread
  PostQuitMessage(1);
  Action := caFree;
end;

procedure TLogAgentForm.FormCreate(Sender: TObject);
begin
  FSessionLoggedMsgCount := 0;
  Reset;
end;

procedure TLogAgentForm.CheckListBoxClickCheck(Sender: TObject);
begin
  if Sender is TCheckListBox then
    ResetCheckListBox(TCheckListBox(Sender), TCheckListBox(Sender).ItemIndex);
end;

procedure TLogAgentForm.LogBtnClick(Sender: TObject);
var
  i: integer;
  multiplicity: integer;
  msg: TLogMessage;
  details: TStringList;
begin
  multiplicity := StrToIntDef(MultiplicityEdit.Text, 1);
  for i := 0 to multiplicity - 1 do begin
    msg := TLogMessage.Create(
      Self.ClassName,
      UserMsgEdit.Text,
      TLogMsgGroup(GroupCheckListBox.ItemIndex + 1),
      TLogMsgPriority(PriorityCheckListBox.ItemIndex + 1),
      TLogMsgSeverity(SeverityCheckListBox.ItemIndex + 1),
      SystemMsgEdit.Text);
    msg.CategoryStr := CategoryEdit.Text;
    details := TStringList.Create;
    details.AddStrings(DetailsValueListEditor.Strings);
    msg.Details := details;
    msg.Post;

    Inc(FSessionLoggedMsgCount);
  end;
  LogAgentStatusBar.SimpleText := Copy(LogAgentStatusBar.SimpleText, 1, Pos(':', LogAgentStatusBar.SimpleText) + 1) + IntToStr(FSessionLoggedMsgCount);
end;

procedure TLogAgentForm.Reset;
begin
  UserMsgEdit.Text := '';
  CategoryEdit.Text := '';
  SystemMsgEdit.Text := '';
  MultiplicityEdit.Text := '1';
  ResetCheckListBox(PriorityCheckListBox, 0);
  ResetCheckListBox(SeverityCheckListBox, 0);
  ResetCheckListBox(GroupCheckListBox, 0);
  ResetValueListEditor(DetailsValueListEditor);
end;

procedure TLogAgentForm.ResetBtnClick(Sender: TObject);
begin
  Reset;
end;

procedure TLogAgentForm.ResetCheckListBox(CheckListBox: TCheckListBox; CheckedItemIndex: integer);
var
  i: integer;
begin
  CheckListBox.ClearSelection;

  for i := 0 to CheckListBox.Count - 1 do
    CheckListBox.Checked[i] := False;

  if (CheckedItemIndex > -1) and (CheckedItemIndex < CheckListBox.Count) then begin
    CheckListBox.Checked[CheckedItemIndex] := True;
    CheckListBox.Selected[CheckedItemIndex] := True; 
    CheckListBox.ItemIndex := CheckedItemIndex;
  end;
end;

procedure TLogAgentForm.ResetValueListEditor(ValueListEditor: TValueListEditor);
var
  i: integer;
begin
  for i := 1 to ValueListEditor.RowCount - 1 do
    ValueListEditor.Strings.Clear;
end;

end.
