using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //_repo = repo;
            //studentDG.DataContext = repo.GetStudents();

            var studentList = repo.GetStudents();

            //Bind this to a datasource
            studentDG.ItemsSource = studentList;
        }
    }
}