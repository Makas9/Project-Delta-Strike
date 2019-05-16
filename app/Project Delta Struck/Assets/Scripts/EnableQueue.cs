using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableQueue : MonoBehaviour {
    public Transform Container;
    public List<string> AllowedTags = new List<string>();
    RectTransform[] objects;

	// Use this for initialization
	void Awake ()
    {
        objects = Container.GetComponentsInChildren<RectTransform>(true);
        AllowedTags.Add("EndLevelElement");
    }

    public void StartEnabling()
    {
        StartCoroutine(LateCall());
    }


    IEnumerator LateCall()
    {
        foreach (RectTransform r in objects)
        {
            if (AllowedTags.Contains(r.tag))
            {
                r.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
        }
        //Do Function here...
    }
}
