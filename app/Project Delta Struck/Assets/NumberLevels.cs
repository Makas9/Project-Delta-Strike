using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLevels : MonoBehaviour {
    void Awake()
    {
        if (Data.Instance.PlayerDataLoaded)
        {
            NumberLvls(Data.Instance.PlayerData.LevelReached);
        }
        else
        {
            SaveSystem.Instance.CallGetData();
            StartCoroutine(WaitForPlayerData());
        }
    }

    IEnumerator WaitForPlayerData()
    {
        while (!Data.Instance.PlayerDataLoaded)
            yield return null;
        print(Data.Instance.PlayerData.LevelReached);
        NumberLvls(Data.Instance.PlayerData.LevelReached);
    }

    public void NumberLvls(int levelReached)
    {
        var buttons = GetComponentsInChildren<Text>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int level = i + 1;
            buttons[i].text = level.ToString();
            if (level > levelReached)
            {
                Button b = buttons[i].transform.parent.GetComponentInParent<Button>();
                b.interactable = false;
            }
        }
    }
}
