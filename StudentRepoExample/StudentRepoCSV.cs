using CsvHelper;
using CsvHelper.Configuration;
using StudentRepoExample.DataModel;
using System.Collections.Generic;
using System.IO;

namespace StudentRepoExample
{
    public class StudentRepoCSV : IRepo
    {
        readonly string csvFilePath = "student.csv";
        //HARDCODED file path for simplicity
        public StudentRepoCSV() => csvFilePath = "student.csv";

        //READ - HOW
        public List<Student> GetStudents()
        {
            // #TODO Validation for file exist
            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var reader = new StreamReader(csvFilePath); //OPENS THE FILE PATH AND BEGINS READING
            var csv = new CsvReader(reader, config); // THIS MAPS THE RECORDS OR READS ROWS

           return csv.GetRecords<Student>().ToList(); //THIS OUTPUTS THE ROWS == STUDENT OBJECT LIST

        }

        #region CRUD Methods NOT DONE
        //CREATE
        public void AddStudent(List<Student> addStudents)
        {
            // #TODO CREATE needs implementation
            //string newPath = $"student2.csv";
            using var writer = new StreamWriter(csvFilePath);
            var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);
            //List<Student> students = new List<Student>();
            //students.Add(addStudent);
            csv.WriteRecords(addStudents);
           //csv.WriteRecord(addStudent);

        }

        //UPDATE
        public void UpdateStudent(Student upStudent)
        {
            // #TODO UPDATE needs implementation
        }

        //DELTE
        public void DeleteSelectedStudent(int delStudent)
        {
            // #TODO DELETE needs implementation
        }
        #endregion
    }
}


//CODE Graveyard
/* allow a user to set a file path
public StudentRepoCSV(string sendFilePath)
{
    csvFilePath = sendFilePath;
}*/