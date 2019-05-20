using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
    public static Knife Instance;
	// Use this for initialization
	void Awake () {
        Instance = this;
	}

}
