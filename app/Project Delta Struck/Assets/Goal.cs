using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    bool reached = false;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (reached) return;
        reached = true;
        int scene = int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) +1;
        SceneFader.Instance.FadeTo(scene.ToString());
    }
}
