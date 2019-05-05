using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    bool itemsLoaded = false;
    // Use this for initialization
    void Start() {
        if (itemsLoaded) return;
        FillDataFromDB();

    }
	
	public void FillDataFromDB()
    {
        CallGetItems();
    }

    public void CallGetItems()
    {
        StartCoroutine(GetItems());
    }

    IEnumerator GetItems()
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/getUserItems.php", form);
        yield return www;
        Debug.Log(www.text);
        FromJsonToObjects(www.text);
    }

    string[] objSeperators = { "Name:" };
    void FromJsonToObjects(string json)
    {
        string[] objects = json.Split(objSeperators, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < objects.Length; i++)
        {
            int seperatorIndex = objects[i].IndexOf('-', 0);
            string Name = objects[i].Substring(0, seperatorIndex);
            string Object = objects[i].Substring(seperatorIndex + 1, objects[i].Length-seperatorIndex);
            switch (Name)
            {
                //case "Gun":
                //    GunSettings gun = JsonUtility.FromJson<GunData>(Object);
                //    break;
                //case "Vest":
                //    GunData gun = JsonUtility.FromJson<GunData>(Object);
                //    break;
                //case "Gun":
                //    GunData gun = JsonUtility.FromJson<GunData>(Object);
                //    break;
                //case "Gun":
                //    GunData gun = JsonUtility.FromJson<GunData>(Object);
                //    break;
            }
        }
        itemsLoaded = true;
    }

    public static string RemoveFirstLines(string text, int linesCount)
    {
        var lines = Regex.Split(text, "\r\n|\r|\n").Skip(linesCount);
        return string.Join(Environment.NewLine, lines.ToArray());
    }
}
