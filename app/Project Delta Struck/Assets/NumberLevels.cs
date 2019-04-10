using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLevels : MonoBehaviour {
    void Awake()
    {
        var buttons = GetComponentsInChildren<Text>();
        int levelReached = 1;

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
