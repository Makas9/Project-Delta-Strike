using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWorldFollow : MonoBehaviour {
    public Transform target;
    public float yOffset = 1.5f;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void OnRenderObject()
    {
        var screenPos = Camera.main.WorldToScreenPoint(new Vector2(target.transform.position.x, target.transform.position.y + yOffset));
        transform.position = screenPos;
    }
}
