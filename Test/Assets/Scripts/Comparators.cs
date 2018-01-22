using System;
using System.Collections.Generic;

public class NameComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y) || ReferenceEquals(null, x)) return 1;
        return String.Compare(x._EmployeeData.Name, y._EmployeeData.Name, StringComparison.Ordinal);
    }
}

public class SurameComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y) || ReferenceEquals(null, x)) return 1;
        return String.Compare(x._EmployeeData.Surname, y._EmployeeData.Surname, StringComparison.Ordinal);
    }
}

public class PatronymicComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y) || ReferenceEquals(null, x)) return 1;

        if (ReferenceEquals(x._EmployeeData.Patronymic, y._EmployeeData.Patronymic)) return 0;
        if (ReferenceEquals(null, x._EmployeeData.Patronymic) ||
            ReferenceEquals(null, y._EmployeeData.Patronymic)) return 1;

        return String.Compare(x._EmployeeData.Patronymic, y._EmployeeData.Patronymic, StringComparison.Ordinal);
    }
}

public class PositionComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return 1;
        return String.Compare(x._EmployeeData.Position, y._EmployeeData.Position, StringComparison.Ordinal);
    }
}

public class SalaryComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return 1;
        return x._EmployeeData.Salary.CompareTo(y._EmployeeData.Salary);
    }
}