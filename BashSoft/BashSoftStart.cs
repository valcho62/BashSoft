using BashSoft;

public static class BashSoftStart
{
    
    public static void Main()
    {
        IOManager.ChangeCurrentDirectoryAbsolute(@"c:\windows");
        IOManager.TraverseDirectory(20);
    }
}

