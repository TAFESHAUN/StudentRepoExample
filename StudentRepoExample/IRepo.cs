using StudentRepoExample.DataModel;

namespace StudentRepoExample
{
    //Defualt Repo for student records
    public interface IRepo
    {
        //APPLY this called CRUD operations

        //1.READ in a list of students - WHAT
        List<Student> GetStudents(); // #TODO Make it more flex by getting generic records instead

        //2.CREATE a new student record
        void AddStudent(Student addStudent);

        //3.UPDATE an existing student record
        void UpdateStudent(Student upStudent);

        //4.DELETE a student record
        void DeleteSelectedStudent(int delStudent); // #TODO Pass obj or ID?


    }
}
