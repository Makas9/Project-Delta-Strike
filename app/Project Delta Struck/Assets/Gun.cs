using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public static Gun Instance;
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
}
