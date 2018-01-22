using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static string Name;

    public static void Save(string name, List<Employee> workers)
    {
        string path = GeneratePath(name);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = null;

        try
        {
            file = File.Create(path);
        }
        catch (Exception)
        {
            file = File.Create(Application.dataPath + "/Saves/Default.workers");
            Name = "Default";
        }

        if (workers != null && workers.Count > 0)
        {
            List<EmployeeData> data = new List<EmployeeData>();
            foreach (Employee employee in workers)
            {
                data.Add(employee._EmployeeData);
            }
            bf.Serialize(file, data);
        }

        file.Close();
    }

    public static List<EmployeeData> Load(string name)
    {
        string path = GeneratePath(name);
        List<EmployeeData> data = new List<EmployeeData>();
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            try
            {
                data = (List<EmployeeData>) bf.Deserialize(file);
            }
            catch (Exception)
            {
            }
            file.Close();
        }
        else
        {
            Save(name, new List<Employee>());
        }

        return data;
    }

    private static string GeneratePath(string name)
    {
        Directory.CreateDirectory(Application.dataPath + "/Saves/");

        name = name.Trim();
        string path = Application.dataPath + "/Saves/";

        if (name == String.Empty)
            name = "Default";

        Name = name;

        if (!path.EndsWith(".workers"))
            if (name.EndsWith(".workers"))
                path += name;
            else
                path += name + ".workers";

        return path;
    }
}