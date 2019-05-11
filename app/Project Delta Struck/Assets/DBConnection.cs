/*using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

public class DBConnection
{
    private string UID = "kosbud_admin";
    private string password = "admin123admin";
    private DBConnection()
    {
    }
    public string DatabaseName { get; set; } = "kosbud_uniproject";

    public string Password { get; set; }
    private MySqlConnection connection = null;
    public MySqlConnection Connection
    {
        get { return connection; }
    }

    private static DBConnection _instance = null;
    public static DBConnection Instance()
    {
        if (_instance == null)
        {
            _instance = new DBConnection();
        }
        return _instance;
    }
    public static DBConnection NewInstance()
    {
        return new DBConnection();
    }
    public bool IsConnect()
    {
        if (Connection == null)
        {
            if (String.IsNullOrEmpty(DatabaseName))
                return false;
            string connstring = string.Format("Server=6; database=; UID="+ UID + "; password=" + password, DatabaseName);
            connection = new MySqlConnection(connstring);
            connection.Open();
        }
        return true;
    }

    public void Close()
    {
        if (connection != null)
            connection.Close();
    }
}*/
