using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class IOManager
    {
        public static void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                OutputWriter.DisplayExeption(ExeptionMessages.InvalidPath);
                return;
            }
            SessionData.currentPath = absolutePath;
        }
        public static void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string curentPath = SessionData.currentPath;
                    int index = curentPath.LastIndexOf("\\");
                    string newPath = curentPath.Substring(0, index);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    OutputWriter.DisplayExeption(ExeptionMessages.UnableToGoHigher);
                }
            }
            else
            {
                string curentPath = SessionData.currentPath;
                curentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(curentPath);
            }
        }
        public static void CreateDirectoryInCurrentFolder(string name)
        {
            string path = SessionData.currentPath + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                OutputWriter.DisplayExeption(ExeptionMessages.InvalidSymbolInNames);
            }
        }
        public static void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;
            Queue<string> subfolders = new Queue<string>();
            subfolders.Enqueue(SessionData.currentPath);

            while (subfolders.Count != 0)
            {
                string currentFolder = subfolders.Dequeue();
                int identation = currentFolder.Split('\\').Length - initialIdentation;
                if (depth - identation < 0)
                {
                    break;
                }
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentFolder));
                try
                {
                    foreach (var file in Directory.GetFiles(currentFolder))
                    {
                        int index = file.LastIndexOf("\\");
                        string fileName = file.Substring(index);
                        OutputWriter.WriteMessageOnNewLine(new string('-', index) + fileName);
                    }

                    foreach (var directory in Directory.GetDirectories(currentFolder))
                    {
                        subfolders.Enqueue(directory);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayExeption(ExeptionMessages.UnauthorizedAccessExceptionMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
