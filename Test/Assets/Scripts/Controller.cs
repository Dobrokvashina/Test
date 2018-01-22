using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject LoadMenu;

    [SerializeField] private GameObject ViewPanel;

    [SerializeField] private GameObject CreatePanel;

    [SerializeField] private GameObject Warning;

    [SerializeField] private GameObject Employees;

    [SerializeField] private GameObject ButtonPrefab;

    [SerializeField] private InputField IdField;

    [SerializeField] private InputField NameField;

    [SerializeField] private InputField SurnameField;

    [SerializeField] private InputField PatronymicField;

    [SerializeField] private InputField PositionField;

    [SerializeField] private InputField SalaryField;

    [SerializeField] private InputField FileName;

    [SerializeField] private Text Name;

    [SerializeField] private Text PathInfo;

    private List<Employee> _employees;

    private Employee currentEmployee;

    private int sort = 0;


    void Start()
    {
        PathInfo.text = "Данные сохраняются в " + Application.dataPath + "/Saves";
        _employees = new List<Employee>();
        OpenLoad();
    }

    public void LoadList()
    {
        _employees = new List<Employee>();
        int count = Employees.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(Employees.transform.GetChild(0).gameObject);
        }

        foreach (EmployeeData data in SaveLoad.Load(FileName.text))
        {
            GameObject employee = Instantiate(ButtonPrefab);

            employee.name = "Employee " + data.ContractId;
            employee.transform.SetParent(Employees.transform);
            employee.GetComponent<Employee>().SetEmployee(data);
            _employees.Add(employee.GetComponent<Employee>());
        }

        Name.text = "Файл :" + SaveLoad.Name;

        OpenViewPanel();
    }

    public void SaveOrCreateNew()
    {
        SaveLoad.Save(FileName.text, _employees);

        Name.text = "Файл :" + SaveLoad.Name;

        OpenViewPanel();
    }

    public void SortBy(int by)
    {
        if (sort == by)
        {
            _employees.Reverse();
        }
        else
        {
            sort = by;
            switch (by)
            {
                case 1:
                    _employees.Sort();
                    break;
                case 2:
                    _employees.Sort(new NameComparer());
                    break;
                case 3:
                    _employees.Sort(new SurameComparer());
                    break;
                case 4:
                    _employees.Sort(new PatronymicComparer());
                    break;
                case 5:
                    _employees.Sort(new PositionComparer());
                    break;
                case 6:
                    _employees.Sort(new SalaryComparer());
                    break;
                default:
                    _employees.Sort();
                    break;
            }
        }
        ChangeIndex();
    }

    private void ChangeIndex()
    {
        for (int i = 0; i < _employees.Count; i++)
        {
            _employees[i].transform.SetSiblingIndex(i);
        }
    }

    public void CheckInfo()
    {
        if (CheckFieldsEmpty())
        {
            OpenWarningWindow();
            return;
        }


        int Id;
        double Salary;

        if (!int.TryParse(IdField.text, out Id) || !double.TryParse(SalaryField.text, out Salary))
        {
            OpenWarningWindow();
            return;
        }


        if (currentEmployee != null)
        {
            if (Id != currentEmployee._EmployeeData.ContractId && !CheckIdUniq(Id))
            {
                OpenWarningWindow();
                return;
            }

            UpdateEmployee(Id, Salary);
        }
        else
        {
            if (!CheckIdUniq(Id))
            {
                OpenWarningWindow();
                return;
            }


            CreateEmployee(Id, Salary);
        }

        ClearInfo();

        OpenViewPanel();

        sort = 0;
    }

    private void UpdateEmployee(int id, double salary)
    {
        currentEmployee.gameObject.name = "Employee " + id;
        currentEmployee.transform.SetParent(Employees.transform);
        EmployeeData data = new EmployeeData(id, NameField.text, SurnameField.text,
            PatronymicField.text, PositionField.text, salary);
        currentEmployee.GetComponent<Employee>().SetEmployee(data);
    }

    private void CreateEmployee(int id, double salary)
    {
        GameObject employee = Instantiate(ButtonPrefab);

        employee.name = "Employee " + id;
        employee.transform.SetParent(Employees.transform);
        EmployeeData data = new EmployeeData(id, NameField.text, SurnameField.text,
            PatronymicField.text, PositionField.text, salary);
        employee.GetComponent<Employee>().SetEmployee(data);
        _employees.Add(employee.GetComponent<Employee>());
    }

    private void OpenWarningWindow()
    {
        Warning.SetActive(true);
    }


    private void SetInfo(Employee employee)
    {
        IdField.text = employee._EmployeeData.ContractId.ToString();
        NameField.text = employee._EmployeeData.Name;
        SurnameField.text = employee._EmployeeData.Surname;
        PatronymicField.text = employee._EmployeeData.Patronymic;
        PositionField.text = employee._EmployeeData.Position;
        SalaryField.text = employee._EmployeeData
            .Salary.ToString();
    }

    private void ClearInfo()
    {
        IdField.text = null;
        NameField.text = null;
        SurnameField.text = null;
        PatronymicField.text = null;
        PositionField.text = null;
        SalaryField.text = null;
    }

    private void GenerateUniqId()
    {
        int id = _employees.Count + 1;
        while (!_employees.TrueForAll(x => x._EmployeeData.ContractId != id))
            id++;

        IdField.text = id.ToString();
    }

    private bool CheckIdUniq(int id)
    {
        return _employees.TrueForAll(x => x._EmployeeData.ContractId != id);
    }

    private bool CheckFieldsEmpty()
    {
        IdField.text = IdField.text.Trim();
        NameField.text = NameField.text.Trim();
        SurnameField.text = SurnameField.text.Trim();
        PatronymicField.text = PatronymicField.text.Trim();
        PositionField.text = PositionField.text.Trim();
        SalaryField.text = SalaryField.text.Trim();

        return (IdField.text == String.Empty || NameField.text == String.Empty || SurnameField.text == String.Empty
                || PositionField.text == String.Empty || SalaryField.text == String.Empty);
    }

    public void OpenViewPanel()
    {
        LoadMenu.SetActive(false);
        ViewPanel.SetActive(true);
        CreatePanel.SetActive(false);
        Warning.SetActive(false);
    }

    public void OpenAddPanel()
    {
        LoadMenu.SetActive(false);
        CreatePanel.SetActive(true);
        ViewPanel.SetActive(false);
        Warning.SetActive(false);
        GenerateUniqId();
        currentEmployee = null;
    }

    public void OpenChangePanel(Employee employee)
    {
        LoadMenu.SetActive(false);
        CreatePanel.SetActive(true);
        ViewPanel.SetActive(false);
        Warning.SetActive(false);

        if (employee != null)
        {
            SetInfo(employee);
            currentEmployee = employee;
        }
    }

    public void OpenLoad()
    {
        LoadMenu.SetActive(true);
        CreatePanel.SetActive(false);
        ViewPanel.SetActive(false);
        Warning.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DeleteFromList(Employee removed)
    {
        _employees.Remove(removed);
    }

    public void CancelLoad()
    {
        if (Name.text != String.Empty)
            OpenViewPanel();
    }
}