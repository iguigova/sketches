object LogAgentForm: TLogAgentForm
  Left = 75
  Top = 269
  Caption = 'LogAgentForm'
  ClientHeight = 360
  ClientWidth = 451
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnCreate = FormCreate
  DesignSize = (
    451
    360)
  PixelsPerInch = 96
  TextHeight = 13
  object PriorityLbl: TLabel
    Left = 16
    Top = 56
    Width = 31
    Height = 13
    Caption = 'Priority'
  end
  object SeverityLbl: TLabel
    Left = 113
    Top = 56
    Width = 38
    Height = 13
    Caption = 'Severity'
  end
  object GroupLbl: TLabel
    Left = 207
    Top = 56
    Width = 29
    Height = 13
    Caption = 'Group'
  end
  object LogBtn: TButton
    Left = 220
    Top = 303
    Width = 97
    Height = 25
    Hint = 'Log message editors'#39' entries'
    Anchors = [akRight, akBottom]
    Caption = 'Log Message(s)'
    TabOrder = 0
    OnClick = LogBtnClick
  end
  object ResetBtn: TButton
    Left = 335
    Top = 304
    Width = 97
    Height = 24
    Hint = 'Reset all message editors'
    Anchors = [akRight, akBottom]
    Caption = 'Reset Form'
    TabOrder = 1
    OnClick = ResetBtnClick
  end
  object LogAgentStatusBar: TStatusBar
    Left = 0
    Top = 341
    Width = 451
    Height = 19
    BiDiMode = bdLeftToRight
    Panels = <>
    ParentBiDiMode = False
    SimpleText = 'Session Log Message Count: '
  end
  object UserMsgEdit: TLabeledEdit
    Left = 16
    Top = 24
    Width = 416
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    EditLabel.Width = 68
    EditLabel.Height = 13
    EditLabel.Caption = 'User Message'
    TabOrder = 3
  end
  object PriorityCheckListBox: TCheckListBox
    Left = 16
    Top = 72
    Width = 81
    Height = 57
    OnClickCheck = CheckListBoxClickCheck
    ItemHeight = 13
    Items.Strings = (
      'Low'
      'Medium'
      'High')
    TabOrder = 4
  end
  object SeverityCheckListBox: TCheckListBox
    Tag = 1
    Left = 113
    Top = 72
    Width = 79
    Height = 57
    OnClickCheck = CheckListBoxClickCheck
    ItemHeight = 13
    Items.Strings = (
      'Minor'
      'Major'
      'Critical'
      'Fatal')
    TabOrder = 5
  end
  object GroupCheckListBox: TCheckListBox
    Tag = 2
    Left = 207
    Top = 72
    Width = 89
    Height = 57
    OnClickCheck = CheckListBoxClickCheck
    ItemHeight = 13
    Items.Strings = (
      'Hint'
      'Warning'
      'Error'
      'Exception')
    TabOrder = 6
  end
  object CategoryEdit: TLabeledEdit
    Tag = 2
    Left = 312
    Top = 72
    Width = 120
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    EditLabel.Width = 61
    EditLabel.Height = 13
    EditLabel.Caption = 'Category IDs'
    TabOrder = 7
  end
  object SystemMsgEdit: TLabeledEdit
    Tag = 1
    Left = 16
    Top = 160
    Width = 416
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    EditLabel.Width = 80
    EditLabel.Height = 13
    EditLabel.Caption = 'System Message'
    TabOrder = 8
  end
  object MultiplicityEdit: TLabeledEdit
    Left = 172
    Top = 305
    Width = 28
    Height = 21
    Anchors = [akRight, akBottom]
    EditLabel.Width = 48
    EditLabel.Height = 13
    EditLabel.Caption = 'Multiplicity'
    LabelPosition = lpLeft
    TabOrder = 9
    Text = '1'
  end
  object DetailsValueListEditor: TValueListEditor
    Left = 16
    Top = 195
    Width = 416
    Height = 79
    Anchors = [akLeft, akTop, akRight, akBottom]
    KeyOptions = [keyEdit, keyAdd, keyDelete]
    TabOrder = 10
    ColWidths = (
      145
      265)
  end
end
