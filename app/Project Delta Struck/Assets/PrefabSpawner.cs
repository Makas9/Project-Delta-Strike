﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {
    public GameObject Prefab;
    public float Interval = 2f;
    float time;
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > Interval)
        {
            Instantiate(Prefab, transform.position, transform.rotation);
            time = 0f;
        }
	}
}
