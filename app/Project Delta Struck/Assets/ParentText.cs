using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script for a button
/// Purpose: write button's text quicker, by simply changing its parent gameobject name
/// </summary>
[ExecuteInEditMode]
public class ParentText : MonoBehaviour {
    public bool InitialLetterDifferent = true;
    public Color InitialLetterColor;
	// Use this for initialization
	void Start () {
        string parentName = transform.parent.name;
        if (InitialLetterDifferent)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(InitialLetterColor);
            string coloredText = "<color=#" + hexColor + ">" + parentName[0] + "</color>" + parentName.Substring(1, parentName.Length - 1);
            GetComponent<Text>().text = coloredText;
        }
    }
}
