using System;
using System.Diagnostics;


namespace BashSoft
{
    public static class InputReader
    {
        private const string endCommand = "quit";
        public static void StartReadingCommands()
        {
            string input = Console.ReadLine();
            while (input != endCommand)
            {
                InterpredCommands(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine().Trim();
            }

        }
        public static void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayExeption($"The command '{input}' is invalid");
        }

        public static void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
        }
        public static void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string foldereName = data[1];
                IOManager.CreateDirectoryInCurrentFolder(foldereName);
            }
        }
        public static void TryTraverseDirectory(string input, string[] data)
        {
            if (data.Length == 1)
            {
                IOManager.TraverseDirectory(0);
            }
            else if (data.Length == 2)
            {
                int depth = 0;
                try
                {
                    depth = int.Parse(data[1]);
                    IOManager.TraverseDirectory(depth);
                }
                catch (Exception)
                {
                    OutputWriter.DisplayExeption(ExeptionMessages.UnableToParseNumber);
                }

            }
        }
        public static void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string file1 = data[1];
                string file2 = data[2];
                Tester.CompareContent(file1, file2);
            }
        }
        public static void TryChangeDirRelative(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string dir = data[1];

                IOManager.ChangeCurrentDirectoryRelative(dir);
            }
        }
        public static void TryChangeDirAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string dir = data[1];

                IOManager.ChangeCurrentDirectoryAbsolute(dir);
            }
        }
        public static void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];

                Data.InitilizeData(fileName);
            }
        }
        public static void TryGetHelp()
            {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteEmptyLine();
            }
        public static void InterpredCommands(string input)
        {
            string[] data = input.Split();
            string command = data[0];

            switch (command)
            {
                case "open":
                    TryOpenFile(input, data); break;
                case "mkdir":
                    TryCreateDirectory(input, data); break;
                case "ls":
                    TryTraverseDirectory(input, data); break;
                case "cmp":
                    TryCompareFiles(input, data); break;
                case "cdRel":
                    TryChangeDirRelative(input, data); break;
                case "cdAbs":
                    TryChangeDirAbsolute(input, data); break;
                case "readDb":
                    TryReadDatabaseFromFile(input, data); break;
                case "help":
                    TryGetHelp(); break;
                case "filter":
                    //to doTryOpenFile(input, data);
                    break;
                case "order":
                    //to doTryOpenFile(input, data);
                    break;
                case "decoder":
                    //to doTryOpenFile(input, data);
                    break;
                case "download":
                    //to doTryOpenFile(input, data);
                    break;
                case "downloadAsync":
                    //to doTryOpenFile(input, data);
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }

        }
    }
}

