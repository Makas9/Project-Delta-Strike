using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);
    }
    public void SavePlayer(PlayerData data)
    {
        CallUploadData(data);
    }

    public void CallGetData()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/getPlayerData.php", form);
        yield return www;
        Debug.Log(www.text);
        PlayerData data = JsonUtility.FromJson<PlayerData>(www.text);
        if (data == null)
        {
            Debug.Log("data was null");
            Data.Instance.PlayerData = new PlayerData("Vest", "M9", "Wooden knife", DBManager.username);
            CallUploadData(Data.Instance.PlayerData);
        }
        else
        {
            Data.Instance.PlayerData = data;
        }

        Debug.Log("PlayerLoaded");
    }

    public void CallUploadData(PlayerData playerData)
    {
        StartCoroutine(UploadData(playerData));
    }

    IEnumerator UploadData(PlayerData playerData)
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        form.AddField("data", JsonUtility.ToJson(playerData));
        Debug.Log(JsonUtility.ToJson(playerData));
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/uploadPlayerData.php", form);
        yield return www;
        Debug.Log(www.text);
    }

    public void LoadPlayer()
    {
        CallGetData();
    }
    /// <summary>
    /// Adds item to `item` table in dababase
    /// </summary>
    public void CallAddItem(string itemName)
    {
        StartCoroutine(AddItem(itemName));
    }

    public IEnumerator AddItem(string itemName)
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        form.AddField("item", itemName);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/uploadUserItem.php", form);
        yield return www;
        Debug.Log(www.text);
    }
}
