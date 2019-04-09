using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour {
    public SceneFader sceneFader;

    public void LoadByChildText()
    {
        string levelToLoad = GetComponentInChildren<Text>().text;
        sceneFader.FadeTo(levelToLoad);
    }
}
