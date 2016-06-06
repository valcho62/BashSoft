using BashSoft;

public static class BashSoftStart
{
    
    public static void Main()
    {
        IOManager.ChangeCurrentDirectoryRelative("..");
        IOManager.ChangeCurrentDirectoryRelative("..");
        IOManager.ChangeCurrentDirectoryRelative("..");
        IOManager.ChangeCurrentDirectoryRelative("..");
        IOManager.ChangeCurrentDirectoryRelative("..");
        
        IOManager.TraverseDirectory(1);
    }
}

