using Microsoft.AspNetCore.Hosting;
using StudentRecord.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentRecord.Helpers
{
    public static class FileHelper
    {
        //information about environment like rootpath,webrootpth,envnmt
        private static string GetFilePath(IWebHostEnvironment env)   //return filepth of txt file create appdata if not exists
        {
            var dir = Path.Combine(env.ContentRootPath, "App_Data"); //combine with appdata to dir
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);//create dir if not exists

            var path = Path.Combine(dir, "students.txt");//combine dir with students.txt to get full path
            if (!File.Exists(path))
            {
                // create empty file
                using (File.Create(path)) { }//create file if not exists
            }
            return path;
        }

        public static void AppendStudent(IWebHostEnvironment env, Student s)//append new student to file
        {
            var path = GetFilePath(env); //get file path
            //format to print in txt file
            var line = $"{s.RollNumber}|{s.Name}|{s.Marks}";
            File.AppendAllLines(path, new[] { line }); //append line to file
        }

        public static List<Student> ReadAll(IWebHostEnvironment env) //read all students from file
        { 
            var path = GetFilePath(env);//get file path
            var result = new List<Student>(); //list to hold students
            foreach (var raw in File.ReadAllLines(path)) //read all lines from file
            {
                if (string.IsNullOrWhiteSpace(raw)) continue; //skip empty lines
                var parts = raw.Split('|'); //split line by |
                if (parts.Length < 3) continue;//if less than 3 parts skip
                int marks = 0;
                int.TryParse(parts[2], out marks); //try parse marks to int
                result.Add(new Student //create new student object and add to list
                {
                    RollNumber = parts[0].Trim(), //trim to remove extra spaces
                    Name = parts[1].Trim(),//trim to remove extra spaces
                    Marks = marks//assign marks
                });
            }
            return result;
        }

        public static Student FindByRoll(IWebHostEnvironment env, string roll) //find student by roll number
        {
            List<Student> students = ReadAll(env); //read all students from file

            foreach (Student s in students) //iterate through students
            {
                if (s.RollNumber == roll) //if roll number matches
                {
                    return s;
                }
            }

            return null;
        }
    }
}