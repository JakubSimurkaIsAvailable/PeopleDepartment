using System.Text;
using CommonLibrary;

namespace ReportConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="template"></param>
        /// <param name="outputFolder"></param>
        static void Main(string? inputFile, string? template, string? outputFolder)
        {
            if (inputFile == null)
            {
                throw new ArgumentNullException("inputFile");
            }

            if (template == null)
            {
                throw new ArgumentNullException("template");
            }
            
            StringBuilder templateBuilder = new StringBuilder();
            using (StreamReader readerTemplate = new StreamReader(template))
            {
                string line;
                while ((line = readerTemplate.ReadLine()) != null)
                {
                    templateBuilder.AppendLine(line);
                }
            }

            StreamReader reader = new StreamReader(inputFile);
            var people = new PersonCollection();
            people.LoadFromCsv(new FileInfo(inputFile));
            var reports = people.GenerateDepartmentReports();
            foreach (var report in reports)
            {
                StringBuilder builderEmployee = new StringBuilder();
                foreach (var reportEmployee in report.Employees)
                {
                    builderEmployee.AppendLine(reportEmployee.ToFormattedString());
                }

                StringBuilder builderPhDStudent = new StringBuilder();
                foreach (var reportPhDStudent in report.PhDStudents)
                {
                    builderPhDStudent.AppendLine(reportPhDStudent.ToFormattedString());
                }

                string reportText = templateBuilder.ToString()
                    .Replace("[[Department]]", report.Department)
                    .Replace("[[Head]]", report.Head?.DisplayName ?? "N/A")
                    .Replace("[[Deputy]]", report.Deputy?.DisplayName ?? "N/A")
                    .Replace("[[Secretary]]", report.Secretary?.DisplayName ?? "N/A")
                    .Replace("[[NumberOfEmployees]]", report.NumberOfEmployees.ToString())
                    .Replace("[[NumberOfProfessors]]", report.NumberOfProfessors.ToString())
                    .Replace("[[NumberOfAssociateProfessors]]", report.NumberOfAssociateProfessors.ToString())
                    .Replace("[[NumberOfPhDStudents]]", report.NumberOfPhDStudents.ToString())
                    .Replace("[[Employees]]", builderEmployee.ToString())
                    .Replace("[[PhDStudents]]", builderPhDStudent.ToString());
                if (outputFolder != null)
                    File.WriteAllText(Path.Combine(outputFolder, report.Department + ".txt"), reportText);
                Console.WriteLine(reportText);
            }
        }
    }
}
