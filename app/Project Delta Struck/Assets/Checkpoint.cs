using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private void Awake()
    {
        transform.SetParent(null, true);
    }
}
