program KLOC;
{$APPTYPE CONSOLE}
uses
  SysUtils, Classes;

type
  TFileMatchEvent = procedure(filename: string);

var
  filePath: string;
  fileMask: array of string;
  fileMaskIndex: integer;
  fileLineCnt: integer;
  totalLineCnt: integer;

procedure CountFileLines(filename: string);
var
  fileData: TStringList;
begin
  fileData := TStringList.Create;
  try
    fileData.LoadFromFile(fileName);
    fileLineCnt := fileData.Count;
  except
    on e: Exception do begin
      fileLineCnt := 0;
      Write('EXCEPTION: ' + e.Message)
    end;
  end;
  fileData.Free;  
end;

procedure FindFiles(path: string; mask: array of string; OnMatch: TFileMatchEvent);
var
  i: integer;
  fileRec: TSearchRec;
  maskLineCnt: integer;
  pathLineCnt: integer;
begin
  path := IncludeTrailingBackslash(path);
  //Writeln;
  //Writeln(path); Writeln;

  pathLineCnt := 0;
  for i := 0 to High(mask) do begin
    maskLineCnt := 0;
    if FindFirst(path + mask[i], faAnyFile - faDirectory, fileRec) = 0 then begin
      try
        repeat
          OnMatch(path + fileRec.Name);
          maskLineCnt := maskLineCnt + fileLineCnt;
          if fileLineCnt <> 0 then
            Writeln(fileRec.Name + ': LineCount= ' + IntToStr(fileLineCnt));
        until FindNext(fileRec) <> 0;
      finally
        FindClose(fileRec);
      end;
    end;
    pathLineCnt := pathLineCnt + maskLineCnt;
    if maskLineCnt <> 0 then begin
      Writeln(mask[i] +' LineCount= ' + IntToStr(maskLineCnt));
      Writeln;
    end;
  end;
  totalLineCnt := totalLineCnt + pathLineCnt;
  if pathLineCnt <> 0 then begin
    Writeln(path + ' LineCount= ' + IntToStr(pathLineCnt));
    Writeln;
  end;
end;

procedure FindAll(path: string; mask: array of string; isRecursive: boolean; OnMatch: TFileMatchEvent);
var
  fileRec: TSearchRec;
begin
  path := IncludeTrailingBackslash(path);
  FindFiles(path, mask, OnMatch);

  if isRecursive then begin
    if FindFirst(path + '*.*', faDirectory, fileRec) = 0 then begin
     try
       repeat
        if ((fileRec.Attr and faDirectory) <> 0) and (fileRec.Name<>'.') and (fileRec.Name<>'..') then
         FindAll(path + fileRec.Name, mask, isRecursive, OnMatch);
       until FindNext(fileRec) <> 0;
     finally
       FindClose(fileRec);
     end;
    end;
  end;
end;

begin
  Writeln('USAGE: kloc <path> -R:[0,1] <space separated masks, e.g. *.pas *.cpp>');
  Writeln;
  WriteLn('Command: ' + CmdLine);
  Writeln;

  totalLineCnt := 0;
  filePath := IncludeTrailingBackslash(ParamStr(1));
  SetLength(fileMask, ParamCount - 2);

  for fileMaskIndex := 0 to High(fileMask) do
    fileMask[fileMaskIndex] := ParamStr(fileMaskIndex + 3);

  FindAll(filePath, fileMask, (ParamStr(2)='-R:1'), CountFileLines);
  Writeln('Total LineCount= ' + IntToStr(totalLineCnt));

  ReadLn; //don't close the window, wait for [Enter]
end.
