using System;

[Serializable]
public class EmployeeData
{
    public int ContractId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public string Position { get; set; }
    public double Salary { get; set; }

    public EmployeeData(int contractId, string name, string surname, string patronymic, string position, double salary)
    {
        ContractId = contractId;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Position = position;
        Salary = salary;
    }
}