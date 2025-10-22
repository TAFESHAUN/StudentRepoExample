namespace StudentRepoExample.DataModel
{
    //Data Model for student records > REPO (CSV OR DB SQL)
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }    
        public string YearGroup { get; set; } = "";
        public string SportsTeam { get; set; } = "";
    }
}
