using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {
    public static MoneyDisplay Instance;
    private Text Money;
	// Use this for initialization
	void Awake () {
        Instance = this;
        Money = GetComponent<Text>();
        UpdateMoney();

    }

    public void UpdateMoney()
    {
        Money.text = Data.Instance.Money.ToString();
    }
}
