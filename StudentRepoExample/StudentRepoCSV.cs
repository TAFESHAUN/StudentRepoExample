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

            using var reader = new StreamReader(csvFilePath);
            var csv = new CsvReader(reader, config);

           return csv.GetRecords<Student>().ToList();

        }

        #region CRUD Methods NOT DONE
        //CREATE
        public void AddStudent(Student addStudent)
        {
            // #TODO CREATE needs implementation
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