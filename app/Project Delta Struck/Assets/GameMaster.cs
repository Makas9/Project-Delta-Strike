using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameMaster : MonoBehaviour {
    public static GameMaster Instance;
    public GameObject SimpleBarPrefab;
    public Canvas GameCanvas;
    public CinemachineVirtualCamera Camera;
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
