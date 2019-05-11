using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class DBManager
{
    public static string username = null;
    //public static int score;

    public static bool LoggedIn { get { return username != null; } }
    
    public static void LogOut()
    {
        username = null;
    }
}
