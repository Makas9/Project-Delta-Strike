using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AnimatedCounting : MonoBehaviour {

    Text m;
    public float CountInSeconds;
    public float Value;
    float CountingInterval;
    void OnEnable()
    {
        m = GetComponent<Text>();
        if (Value == 0)
        {
            m.text = "0";
            return;
        }
        CountingInterval = CountInSeconds / Value;
        StartCoroutine(AnimateText());
        
    }

    void FindRoundIncrement()
    {
        
    }

    public float totalTime = 0; 
    IEnumerator AnimateText()
    {
        int round = 0;
        if (CountingInterval > Time.deltaTime)
        {
            while (round < Value)
            {
                round++;
                m.text = round.ToString();
                yield return new WaitForSecondsRealtime(CountingInterval);
            }
        }
        else
        {
            int roundIncrement = Mathf.RoundToInt(Value / (CountInSeconds / Time.deltaTime));
            Debug.Log(transform.parent.name + " " + roundIncrement);
            while(round < Value)
            {
                round += roundIncrement;
                m.text = round.ToString();
                yield return null;
            }
        }

    }
}
