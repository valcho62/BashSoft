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
        public static void TraverseDirectory(string path)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = path.Split('\\').Length;
            Queue<string> subfolders = new Queue<string>();
            subfolders.Enqueue(path);

            while (subfolders.Count != 0)
            {
                string currentFolder = subfolders.Dequeue();
                int identation = currentFolder.Split('\\').Length - initialIdentation;
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentFolder));
                try
                {
                    foreach (var directory in Directory.GetDirectories(currentFolder))
                    {
                        subfolders.Enqueue(directory);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
