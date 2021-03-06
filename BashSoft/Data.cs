﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class Data
    {
        public static bool isDataInitialized = false;
        public static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;
        public static void InitilizeData(string fileName)
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data ...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExeptionMessages.DataAlreadyInitialisedException);
            }
        }
        private static void ReadData(string fileName)
        {
            string path = SessionData.currentPath + "\\" + fileName;

           if (Directory.Exists(path))
            {
                string[] allLines = File.ReadAllLines(path);
                for (int i = 0; i < allLines.Length; i++)
                {
                    if (! string.IsNullOrEmpty(allLines[i]) )
                    {
                        string[] tokens = allLines[i].Split(' ');
                        string course = tokens[0];
                        string student = tokens[1];
                        int mark = int.Parse(tokens[2]);
                        if (!studentsByCourse.ContainsKey(course))
                        {
                            studentsByCourse.Add(course, new Dictionary<string, List<int>>());
                        }
                        if (!studentsByCourse[course].ContainsKey(student))
                        {
                            studentsByCourse[course].Add(student, new List<int>());
                        }
                        studentsByCourse[course][student].Add(mark);  
                    }
                }                
            }
           else
            {
                OutputWriter.DisplayExeption(ExeptionMessages.InvalidPath);
            }
            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read !");
        }
        public static bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
               if ( studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }
               else
                {
                    OutputWriter.DisplayExeption(ExeptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayExeption(ExeptionMessages.DataNotInitializedExceptionMessage);

            }
            return false;
        }
        public static bool IsQueryForStudentPossible(string courseName, string studentName)
        {
            if (isDataInitialized)
            {
                if (studentsByCourse[courseName].ContainsKey(studentName))
                {
                    return true; 
                }
                else
                {
                    OutputWriter.DisplayExeption(ExeptionMessages.InexistingStudentInDataBase);
                }
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExeptionMessages.DataNotInitializedExceptionMessage);

            }
            return false;
        }
        public static void GetAllStudentsFromCourse(string course)
        {
            if (IsQueryForCoursePossible(course))
            {
                OutputWriter.WriteMessageOnNewLine($"{course}: ");
                foreach (var courseN in studentsByCourse[course])
                {
                    OutputWriter.PrintStudent(courseN);
                }
            }
        }
        public static void GetStudentScoresFromCourse(string course, string student)
        {
            if (IsQueryForStudentPossible(course,student))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, List<int>>(student, studentsByCourse[course][student]));
            }
        }
    }
}
