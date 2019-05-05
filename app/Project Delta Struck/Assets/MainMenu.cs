using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {
    public Button PlayButton;
    public Text playerDisplay;

	// Use this for initialization
	void Start () {
		if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Logged in as " + DBManager.username;
        }
        else
        {
            PlayButton.interactable = false;
        }
	}
}
