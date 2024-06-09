using System.Runtime.InteropServices.JavaScript;

namespace CommonLibrary;

public class DepartmentReport
{
    public string Department { get; private set; }
    public Person? Head { get; private set; }
    public Person? Deputy { get; private set; }
    public Person? Secretary { get; private set; }
    public int NumberOfProfessors { get; private set; }
    public int NumberOfAssociateProfessors { get; private set; }
    public int NumberOfEmployees { get; private set; }
    public int NumberOfPhDStudents { get; private set; }
    public IEnumerable<Person> Employees { get; private set; }
    public IEnumerable<Person> PhDStudents { get; private set; }

    public DepartmentReport(string department, Person? head, Person? deputy, Person? secretary, int numberOfProfessors, int numberOfAssociateProfessors,
        IEnumerable<Person> employees, IEnumerable<Person> phdStudents)
    {
        Department = department;
        Head = head;
        Deputy = deputy;
        Secretary = secretary;
        var enumerable = employees as Person[] ?? employees.ToArray();
        Employees = enumerable;
        var phDStudents = phdStudents as Person[] ?? phdStudents.ToArray();
        PhDStudents = phDStudents;

        NumberOfProfessors = numberOfProfessors;
        NumberOfAssociateProfessors = numberOfAssociateProfessors;
        NumberOfEmployees = enumerable.Count();
        NumberOfPhDStudents = phDStudents.Count();
    }

}