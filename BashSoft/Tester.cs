using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class Tester
    {
        private static string GetMismatchPath(string expectedOutputPath)
        {
            int indexOf = expectedOutputPath.LastIndexOf("\\");
            string directoryPath = expectedOutputPath.Substring(0, indexOf);
            string final = directoryPath + @"\Mismatch.txt";
            return final;
        }
        public static void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading file ...");
            try
            {
                string mismatchPath = GetMismatchPath(expectedOutputPath);
                string[] actualOutputLines = File.ReadAllLines(userOutputPath);
                string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);
                bool hasMismatch;
                string[] mismatches = GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);
                PrintOutput(mismatches, hasMismatch, mismatchPath);
            }
            catch (FileNotFoundException)
            {
                OutputWriter.DisplayExeption(ExeptionMessages.InvalidPath);
            }
            OutputWriter.WriteMessageOnNewLine("File read !");
        }
        public static string[] GetLinesWithPossibleMismatches(string[] actualLines, string[] expectedLines, out bool hasMismatch)
        {
            hasMismatch = false;
            string output = string.Empty;

            OutputWriter.WriteMessageOnNewLine("Comparing files ...");
            int minOutputLines = 0;
            if (actualLines.Length != expectedLines.Length)
            {
                hasMismatch = true;
                minOutputLines = Math.Min(actualLines.Length, expectedLines.Length);
                OutputWriter.DisplayExeption(ExeptionMessages.ComparisonOfFilesWithDifferentSizes);
            }
            else
            {
                minOutputLines = expectedLines.Length;
            }
            string[] mismatches = new string[minOutputLines];
            for (int index = 0; index < minOutputLines; index++)
            {
                string outputLine = actualLines[index];
                string expectedLine = expectedLines[index];
                if (outputLine != expectedLine)
                {
                    output = string.Format("Mismatch at line {0} -- expected \"{1}\", actual \"{2}\"", index, expectedLine, outputLine);
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = expectedLine;
                    output += Environment.NewLine;
                }
                mismatches[index] = output;
            }
            return mismatches;
        }
        public static void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            if (hasMismatch)
            {
                foreach (var line in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(line);
                }
                try
                {
                    File.WriteAllLines(mismatchPath, mismatches);
                }
                catch (DirectoryNotFoundException)
                {
                    OutputWriter.DisplayExeption(ExeptionMessages.InvalidPath);
                }
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine("Files are identical. No mismatches !");
            }
        }
    }
}
