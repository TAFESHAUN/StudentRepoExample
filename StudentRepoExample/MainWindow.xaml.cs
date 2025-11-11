using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        //StudentRepoCSV repo;                 // CSV OFF
        StudentRepoDB repo2;

        List<Student> studentGlobal = new List<Student>();

        //track the selected DB row's Id for update/delete
        private int? _selectedDbId = null;

        public MainWindow()
        {
            InitializeComponent();
            //repo = new StudentRepoCSV();    // CSV OFF
            repo2 = new StudentRepoDB("Data Source=DESKTOP-FFSLR8G\\SQLEXPRESS01;Initial Catalog=StudentDB2025;Integrated Security=True;Trust Server Certificate=True");
            //_repo = repo;
            //studentDG.DataContext = repo.GetStudents();

            //studentDG.ItemsSource = null;   // CSV OFF
            studentDG2.ItemsSource = null;

            //var studentListCSV = repo.GetStudents();  // CSV OFF
            //studentGlobal = studentListCSV;           // CSV OFF

            var studentListDB = repo2.GetStudents();

            //Bind this to a datasource
            //studentDG.ItemsSource = studentListCSV;   // CSV OFF
            studentDG2.ItemsSource = studentListDB;

            //handle selection changes on the DB grid to load fields for editing
            studentDG2.SelectionChanged += studentDG2_SelectionChanged;
        }

        //ADD handler
        private void Add_Student(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StudentName.Text))
            {
                MessageBox.Show("Please enter a Name.");
                return;
            }
            if (!int.TryParse(StudentAge.Text, out var ageInt) || ageInt < 0 || ageInt > 255)
            {
                MessageBox.Show("Age must be a number between 0 and 255.");
                return;
            }

            Student student = new Student
            {
                Name = StudentName.Text.Trim(),
                Age = ageInt,
                YearGroup = string.IsNullOrWhiteSpace(StudentYear.Text) ? null : StudentYear.Text.Trim(),
                SportsTeam = string.IsNullOrWhiteSpace(StudentSport.Text) ? null : StudentSport.Text.Trim()
            };

            try
            {
                // add to DB via stored procedure repo (sets student.Id via OUTPUT)
                repo2.AddStudent(new List<Student> { student });

                // CSV OFF (keep code for later reuse)
                //studentGlobal.Add(student);
                //repo.AddStudent(studentGlobal);

                RefreshDbGrid();
                ClearInputsAndSelection();

                MessageBox.Show($"Student Added! New ID: {student.Id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB add failed: {ex.Message}");
            }
        }

        //UPDATE handler
        private void Update_Student(object sender, RoutedEventArgs e)
        {
            if (_selectedDbId == null)
            {
                MessageBox.Show("Select a student from the Database grid first.");
                return;
            }

            // reuse inputs
            if (!int.TryParse(StudentAge.Text, out var ageInt))
            {
                MessageBox.Show("Age must be a valid number.");
                return;
            }

            var student = new Student
            {
                Id = _selectedDbId.Value, // assumes Student.Id is int; adjust if byte
                Name = StudentName.Text,
                Age = ageInt,
                YearGroup = StudentYear.Text,
                SportsTeam = StudentSport.Text
            };

            try
            {
                repo2.UpdateStudent(student);
                RefreshDbGrid();
                ClearInputsAndSelection();
                MessageBox.Show("Student Updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}");
            }
        }

        //DELETE handler
        private void Delete_Student(object sender, RoutedEventArgs e)
        {
            if (_selectedDbId == null)
            {
                MessageBox.Show("Select a student from the Database grid first.");
                return;
            }

            if (MessageBox.Show("Delete this student?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            try
            {
                repo2.DeleteSelectedStudent(_selectedDbId.Value);
                RefreshDbGrid();
                ClearInputsAndSelection();
                MessageBox.Show("Student Deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed: {ex.Message}");
            }
        }

        //when you click a row in the DB grid, load it into the inputs
        private void studentDG2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (studentDG2.SelectedItem is Student s)
            {
                _selectedDbId = Convert.ToInt32(s.Id);
                StudentName.Text = s.Name;
                StudentAge.Text = s.Age.ToString();
                StudentYear.Text = s.YearGroup;
                StudentSport.Text = s.SportsTeam;
            }
        }

        //helpers
        private void RefreshDbGrid()
        {
            studentDG2.ItemsSource = null;
            studentDG2.ItemsSource = repo2.GetStudents();
        }

        private void ClearInputsAndSelection()
        {
            _selectedDbId = null;
            StudentName.Text = "";
            StudentAge.Text = "";
            StudentYear.Text = "";
            StudentSport.Text = "";
            studentDG2.SelectedItem = null;
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
