using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class BashSoft
{
    public static void TraverseDir ( string directory)
    {
        Console.WriteLine($"--{directory}--");
        string[] allFiles = Directory.GetFiles(directory);
        foreach (var file in allFiles)
        {
            Console.WriteLine(file);
        }

        string[] allDirs = Directory.GetDirectories(directory);
        foreach (var dir in allDirs)
        {
            TraverseDir(dir);
        }
    }
    static void Main()
    {
        TraverseDir("c:/");
    }
}

