using System.Collections;
using System.Diagnostics;

namespace CommonLibrary;

public class PersonCollection : IEnumerable<Person>
{
    List<Person> _people = new List<Person>();

    public void Add(Person person)
    {
        _people.Add(person);
    }

    public bool Remove(Person person)
    {
        return _people.Remove(person);
    }

    public IEnumerator<Person> GetEnumerator()
    {
        return _people.GetEnumerator();
    }

    public void LoadFromCsv(FileInfo csvFile)
    {
        StreamReader reader = new StreamReader(csvFile.FullName);
        reader.ReadLine(); // Skip header
        while (!reader.EndOfStream)
        {
            string[] values = reader.ReadLine().Split(';');

            _people.Add(
                new Person(values[0], values[1], values[2], values[3], values[4], values[5].Split(',').ToList()));
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public DepartmentReport[] GenerateDepartmentReports()
    {
        var reports = new List<DepartmentReport>();
        var departments = _people.SelectMany(p => p.Department).Distinct();
        foreach (var department in departments)
        {
            reports.Add(new DepartmentReport(department,
                _people.FirstOrDefault(p => p.Department.Contains(department) && p.Position!.Contains("vedúci")),
                _people.FirstOrDefault(p => p.Department.Contains(department) && p.Position!.Contains("zástupca vedúceho")),
                _people.FirstOrDefault(p => p.Department.Contains(department) && p.Position!.Contains("sekretárka")),
                _people.Count(p => p.Department.Contains(department) && p.TitleBefore!.Contains("prof.")),
                _people.Count(p => p.Department.Contains(department) && p.TitleBefore!.Contains("doc.")),
                _people.Where(p => p.Department.Contains(department)),
                _people.Where(p => p.Department.Contains(department) && p.Position!.Contains("doktorand"))));
        }
        
        return reports.OrderBy(d => d.Department).ToArray();
    }
}
// reports.Add(new DepartmentReport(department, _people.FirstOrDefault(p => p.Department)))
/*
reports.Add(new DepartmentReport(department,
    _people.FirstOrDefault(p => p.Department.Contains(department) && p.Position.Contains("vedúci")),
    _people.FirstOrDefault(p => p.Position.Contains(department) && p.Position.Contains("sekretárka")),
    _people.SelectMany(p => p.Department.Contains(department)),
    _people.SelectMany(p => p.Position.Contains("doktorand"))));
*/