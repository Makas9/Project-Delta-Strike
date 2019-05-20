using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    bool reached = false;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (reached) return;
        if (!col.transform.CompareTag("Player")) return;
        reached = true;
        int scene = int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) +1;
        Data.Instance.PlayerData.LevelReached = scene;
        SaveSystem.Instance.SavePlayer(Data.Instance.PlayerData);
        GameMaster.Instance.LevelEnded(true);
    }
}
