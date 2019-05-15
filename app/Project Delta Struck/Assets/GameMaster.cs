using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    public static GameMaster Instance;
    public GameObject SimpleBarPrefab;
    public Canvas GameCanvas;
    public CinemachineVirtualCamera Camera;
    public Text moneyTxt;

    // Use this for initialization
    void Awake () {
        Instance = this;
        Data.Instance.PlayerData.LevelReached = int.Parse(SceneManager.GetActiveScene().name);
        moneyTxt.text = Data.Instance.PlayerData.Money.ToString();
    }

    /// <summary>
    /// Adds coins to the game
    /// </summary>
    /// <param name="amount"></param>
    public void AddMoney(int amount)
    {
        Data.Instance.Money += amount;
        moneyTxt.text = Data.Instance.Money.ToString();
    }
}
