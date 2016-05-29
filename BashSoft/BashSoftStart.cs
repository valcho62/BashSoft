using BashSoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class BashSoftStart
{
    
    public static void Main()
    {
        IOManager.ChangeCurrentDirectoryAbsolute(@"c:\windows");
        IOManager.TraverseDirectory(20);
    }
}

