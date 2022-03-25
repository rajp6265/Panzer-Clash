using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public class UsersData
{
    public List<UserData> usersDataList = new List<UserData>();
    private static string _fileName = "/Userdata";
    private static string FilePath => Application.persistentDataPath
                         + _fileName;
    public static void SaveUserDataBinary(UserData userData)
    {
        UsersData currentData = LoadUserDataBinary();

        BinaryFormatter bf = new BinaryFormatter();
        if (currentData == null)
        {
            FileStream file = File.Create(Application.persistentDataPath
                         + "/Userdata.dat");
            UsersData usersData = new UsersData();
            usersData.usersDataList.Add(userData);

            bf.Serialize(file, usersData);
            file.Close();
            Debug.Log("User data saved!");
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath
                         + "/Userdata.dat", FileMode.Append);
            UsersData usersData = new UsersData();
            usersData.usersDataList.AddRange(currentData.usersDataList);
            usersData.usersDataList.Add(userData);

            bf.Serialize(file, usersData);
            file.Close();
        }

    }

    public static UsersData LoadUserDataBinary()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/Userdata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/Userdata.dat", FileMode.Open);
            UsersData data = (UsersData)bf.Deserialize(file);
            file.Close();

            Debug.Log("User data loaded!");
            return data;
        }
        else
        {
            Debug.Log("User data Not found");
            return null;
        }

    }

    public static void SaveUserData(UserData userData)
    {
        UsersData currentData = LoadUserData();


        if (currentData == null)
        {
            // FileStream file = File.Create(FilePath);
            UsersData usersData = new UsersData();
            usersData.usersDataList.Add(userData);
            string json = JsonUtility.ToJson(usersData, true);

            File.WriteAllText(FilePath, json);

            // file.Close();
            Debug.Log("User data saved!");
        }
        else
        {
            // FileStream file = File.Open(FilePath, FileMode.Append);

            UsersData usersData = new UsersData();
            usersData.usersDataList.AddRange(currentData.usersDataList);
            usersData.usersDataList.Add(userData);
            string json = JsonUtility.ToJson(usersData, true);
            File.WriteAllText(FilePath, json);

            // file.Close();
        }

    }

    public static UsersData LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath
                       + _fileName))
        {

            // FileStream file =
            //            File.Open(Application.persistentDataPath
            //            + _fileName, FileMode.Open);
            string contents = File.ReadAllText(FilePath);
            UsersData data = (UsersData)JsonUtility.FromJson<UsersData>(contents);
            // file.Close();

            Debug.Log("User data loaded!");
            return data;
        }
        else
        {
            Debug.Log("User data Not found");
            return null;
        }

    }
}
[System.Serializable]
public class UserData
{
    public string FirstName;
    public string LastName;
    public string Age;
    public Gender Gender;
    public HandColor HandColor;
    public MainHand MainHand;
    public string Name => FirstName + " " + LastName;
    public string GenderIntial => Gender.ToString().Substring(0, 1);



    public UserData(string firstName, string lastName, string age, Gender gender, HandColor handColor, MainHand mainHand)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        this.Gender = gender;
        this.HandColor = handColor;
        this.MainHand = mainHand;
    }
}
public enum Gender
{
    Male, Female
}
public enum MainHand
{
    Left, Right
}
public enum HandColor
{
    Light, Pale, Dark
}

