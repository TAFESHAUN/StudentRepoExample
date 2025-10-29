using System.Windows;
using StudentRepoExample.DataModel;

namespace StudentRepoExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // #TODO Try out on app load vs hard force here
        //private StudentRepoCSV _repo = new StudentRepoCSV();
        StudentRepoCSV repo;
        StudentRepoDB repo2;

        List<Student> studentGlobal = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            repo = new StudentRepoCSV();
            repo2 = new StudentRepoDB("Data Source=DESKTOP-FFSLR8G\\SQLEXPRESS01;Initial Catalog=StudentDB2025;Integrated Security=True;Trust Server Certificate=True");
            //_repo = repo;
            //studentDG.DataContext = repo.GetStudents();

            studentDG.ItemsSource = null;
            studentDG2.ItemsSource = null;
            var studentListCSV = repo.GetStudents();
            studentGlobal = studentListCSV;

            var studentListDB = repo2.GetStudents();



            //Bind this to a datasource
            studentDG.ItemsSource = studentListCSV;
            studentDG2.ItemsSource = studentListDB;
        }

        private void Add_Student(object sender, RoutedEventArgs e)
        {
            Student student = new Student
            {
                Name = StudentName.Text,
                Age = Convert.ToInt32(StudentAge.Text),
                YearGroup = StudentYear.Text,
                SportsTeam = StudentSport.Text
            };

            studentGlobal.Add(student);
            repo.AddStudent(studentGlobal);

            MessageBox.Show("Student Added!");

        }
    }
}

//CODE Graveyard
//DataModel.Student myStudent = new DataModel.Student
//{
//    Id = 6,
//    Name = "New Student",
//    Age = 18,
//    YearGroup = "Year 13",
//    SportsTeam = "Hockey"
//};
//studentListCSV.Add(myStudent);