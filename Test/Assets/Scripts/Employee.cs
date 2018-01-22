using System;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour, IComparable<Employee>
{
    [SerializeField] private Text Data;

    private Controller Controller;

    public EmployeeData _EmployeeData;

    private void OnEnable()
    {
        Controller = Camera.main.GetComponent<Controller>();
    }

    public void SetEmployee(EmployeeData employeeData)
    {
        _EmployeeData = employeeData;

        UpdateData();
    }


    public int CompareTo(Employee other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return _EmployeeData.ContractId.CompareTo(other._EmployeeData.ContractId);
    }

    public void UpdateData()
    {
        Data.text = string.Format("Договор № {0} \n ФИО: {1} {2} {3} \n Должность: {4} \n Зарплата: {5}",
            _EmployeeData.ContractId, _EmployeeData.Surname, _EmployeeData.Name, _EmployeeData.Patronymic,
            _EmployeeData.Position, _EmployeeData.Salary);
    }

    public void Change()
    {
        Controller.OpenChangePanel(this);
    }

    public void Delete()
    {
        Controller.DeleteFromList(this);
        Destroy(gameObject);
    }
}