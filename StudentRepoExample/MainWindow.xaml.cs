using System.Windows;

namespace StudentRepoExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // #TODO Try out on app load vs hard force here
        //private StudentRepoCSV _repo = new StudentRepoCSV();
        public MainWindow()
        {
            InitializeComponent();
            StudentRepoCSV repo = new StudentRepoCSV();
            StudentRepoDB repo2 = new StudentRepoDB("YOU CONNECTION STRING HERE");
            //_repo = repo;
            //studentDG.DataContext = repo.GetStudents();

            var studentListCSV = repo.GetStudents();

            var studentListDB = repo2.GetStudents();

            //Bind this to a datasource
            studentDG.ItemsSource = studentListCSV;
            studentDG2.ItemsSource = studentListDB;
        }
    }
}