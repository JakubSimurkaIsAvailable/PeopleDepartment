using System.IO;
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
using CommonLibrary;

namespace ViewerWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PersonCollection PC { get; set; }
        public DepartmentReport[] departments { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PC = new PersonCollection();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV files (*.csv)|*.csv";

            dialog.ShowDialog();
            PC.LoadFromCsv(new FileInfo(dialog.FileName));

            departments = PC.GenerateDepartmentReports();
            foreach (var department in departments)
            {
                DepartmentComboBox.Items.Add(department.Department);
            }
            DepartmentComboBox.SelectedIndex = 0;
            
           
        }

        private void DepartmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HeadLabel.Content = departments[DepartmentComboBox.SelectedIndex].Head?.DisplayName;
            DeputyLabel.Content = departments[DepartmentComboBox.SelectedIndex].Deputy?.DisplayName;
            SecretaryLabel.Content = departments[DepartmentComboBox.SelectedIndex].Secretary?.DisplayName;
            EmployeesCountLabel.Content = departments[DepartmentComboBox.SelectedIndex].NumberOfEmployees;
            PhdStudentsCountLabel.Content = departments[DepartmentComboBox.SelectedIndex].NumberOfPhDStudents;
            EmployeesListView.ItemsSource = departments[DepartmentComboBox.SelectedIndex].Employees;
            PhDStudentsListView.ItemsSource = departments[DepartmentComboBox.SelectedIndex].PhDStudents;
        }
    }
}