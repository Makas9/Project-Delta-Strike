using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    bool itemsLoaded = false;
    string objectsInJson;
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
        CallGetItems();
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

    public void CallGetItems()
    {
        StartCoroutine(GetItems());
    }

    IEnumerator GetItems()
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/getAllItems.php", form);
        yield return www;
        Debug.Log(www.text);
        FromJsonToObjects(www.text);
        CallGetUserItems();
    }
    public void CallGetUserItems()
    {
        StartCoroutine(GetUserItems());
    }

    IEnumerator GetUserItems()
    {
        WWWForm form = new WWWForm();
        Debug.Log(DBManager.username);
        form.AddField("username", DBManager.username);
        WWW www = new WWW("http://codeblacksmith.tk/ProjectDeltaStruct/getUserItems.php", form);
        yield return www;
        Debug.Log(www.text);
        Data.Instance.ItemsLoaded = true;
        SetUserItems(www.text);
    }

    public void SetUserItems(string list)
    {
        string[] items = list.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        Data.Instance.userItems = items.ToList();
    }

    

    public void FillShopsWithData()
    {

    }

    string[] objSeperators = { "Object:" };
    void FromJsonToObjects(string json)
    {
        string[] objects = json.Split(objSeperators, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < objects.Length; i++)
        {
            string[] objectParts = objects[i].Split('|');
            string Type = objectParts[0];
            string Name = objectParts[1];
            string Description = objectParts[2];
            string ImgUrl = objectParts[3];
            string Object = objectParts[4];
            print("Type: " + Type);
            print("Name: " + Name);
            print("Description: " + Description);
            print("ImgUrl: " + ImgUrl);
            print("objecet: " + Object);
            //Sprite spriteFromUrl = GetSprite(ImgUrl, value => spriteFromUrl = value);
            switch (Type)
            {
                case "Gun":
                    GunStats gunStats = JsonUtility.FromJson<GunStats>(Object);
                    GunSettings gun = ScriptableObject.CreateInstance<GunSettings>();
                    gun.Fill(gunStats, Name, Description);
                    //gun.Sprite = spriteFromUrl;
                    Data.Instance.AddGun(gun);
                    break;
                case "Vest":
                    VestStats vestStats = JsonUtility.FromJson<VestStats>(Object);
                    VestSettings vest = ScriptableObject.CreateInstance<VestSettings>();
                    //vest.Sprite = spriteFromUrl;
                    vest.Fill(Name, Description, vestStats);
                    Data.Instance.AddVest(vest);
                    break;
                case "Knife":
                    KnifeStats knifeStats = JsonUtility.FromJson<KnifeStats>(Object);
                    KnifeSettings knife = ScriptableObject.CreateInstance<KnifeSettings>();
                    knife.Fill(knifeStats, Name, Description);
                    //knife.Sprite = spriteFromUrl;
                    Data.Instance.AddKnife(knife);
                    break;
                case "Grenade":
                    GrenadeStats grenadeStats = JsonUtility.FromJson<GrenadeStats>(Object);
                    GrenadeSettings grenade = ScriptableObject.CreateInstance<GrenadeSettings>();
                    grenade.Fill(grenadeStats, Name, Description);
                    //grenade.Sprite = spriteFromUrl;
                    Data.Instance.AddGrenade(grenade);
                    break;
                default:
                    Debug.Log("Unrecognised item type");
                    break;
            }
        }
        itemsLoaded = true;
    }

    Sprite GetSprite(string url, Action<Sprite> action)
    {
        WWW www = new WWW(url);
        while (!www.isDone) ;
        Debug.Log("texture null: " + (www.texture == null));
        if (www.texture != null)
        {
            return Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
        }
        return null;
    }
}
