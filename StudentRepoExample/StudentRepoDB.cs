using StudentRepoExample.DataModel;
using Microsoft.Data.SqlClient;


namespace StudentRepoExample
{
    public class StudentRepoDB : IRepo
    {
        private readonly string _conn;
        public StudentRepoDB(string connString) => _conn = connString;

        //HOW - IS THE DB version of our IRepo interface
        public List<Student> GetStudents()
        {
            var returnStudentlist = new List<Student>();

            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(
                "SELECT * FROM Student", conn);

            conn.Open();
            using SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                returnStudentlist.Add(new Student
                {
                    Id = rdr.GetByte(0),
                    Name = rdr.GetString(1),
                    Age = rdr.GetByte(2),
                    YearGroup = rdr.GetString(3),
                    SportsTeam = rdr.GetString(4)
                });
            }
            conn.Close();
            return returnStudentlist;
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
