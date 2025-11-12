using Microsoft.Data.SqlClient;
using StudentRepoExample.DataModel;
using System.Data;


namespace StudentRepoExample
{
    public class StudentRepoDB : IRepo
    {
        private readonly string _conn;
        public StudentRepoDB(string connString) => _conn = connString; 
        //Config Manager

        //HOW - IS THE DB version of our IRepo interface
        public List<Student> GetStudents()
        {
            var results = new List<Student>();

            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("dbo.usp_Student_SelectAll", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                results.Add(new Student
                {
                    Id = rdr.GetInt32(0),   
                    Name = rdr.GetString(1),
                    Age = rdr.GetByte(2),                //Change this later from byte to int BUT validation on UI 
                    YearGroup =  rdr.GetString(3),
                    SportsTeam = rdr.GetString(4)
                });
            }

            return results;
        }


        #region CRUD Methods
        // CREATE 
        public void AddStudent(List<Student> addStudents)
        {
            if (addStudents == null || addStudents.Count == 0) return;

            using var conn = new SqlConnection(_conn);
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                foreach (var s in addStudents)
                {
                    using var cmd = new SqlCommand("dbo.usp_Student_Add", conn, tx);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = s.Name ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Age", SqlDbType.TinyInt) { Value = s.Age });
                    cmd.Parameters.Add(new SqlParameter("@YearGroup", SqlDbType.NVarChar, 20) { Value = s.YearGroup ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@SportsTeam", SqlDbType.NVarChar, 50) { Value = s.SportsTeam ?? (object)DBNull.Value });

                    // Output parameter to retrieve new ID
                    var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(newIdParam);

                    cmd.ExecuteNonQuery();

                    // Update the object’s Id after insert
                    s.Id = Convert.ToInt32(newIdParam.Value);
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }


        // UPDATE
        public void UpdateStudent(Student upStudent)
        {
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("dbo.usp_Student_Update", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt) { Value = upStudent.Id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = upStudent.Name });
            cmd.Parameters.Add(new SqlParameter("@Age", SqlDbType.TinyInt) { Value = upStudent.Age });
            cmd.Parameters.Add(new SqlParameter("@YearGroup", SqlDbType.NVarChar, 20) { Value = upStudent.YearGroup ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@SportsTeam", SqlDbType.NVarChar, 50) { Value = upStudent.SportsTeam ?? (object)DBNull.Value });

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // DELETE
        public void DeleteSelectedStudent(int delStudent)
        {
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("dbo.usp_Student_Delete", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt) { Value = delStudent });

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        #endregion
    }
}
