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
    public Text Title;
    public AnimatedCounting EnemiesKilled;
    public AnimatedCounting CoinsCollected;
    public GameObject endLevelUI;
    public EnableQueue EndLevelElements;
    public GameObject nextLevelBtn;
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
        Data.Instance.LevelCoinsCollected += amount;
        Data.Instance.Money += amount;
        moneyTxt.text = Data.Instance.Money.ToString();
    }

    /// <summary>
    /// Method to be called when level ends
    /// </summary>
    /// <param name="reason">Why the level ended</param>
    /// <param name="win">Was the level failed or won</param>
    public void LevelEnded(bool win)
    {
        {
            if (win)
            {
                Title.text = "Level completed";
            }
            else
            {
                Title.text = "Player died";
            }
            nextLevelBtn.SetActive(win);
            EnemiesKilled.Value = Data.Instance.LevelEnemiesKilled;
            CoinsCollected.Value = Data.Instance.LevelCoinsCollected;
            endLevelUI.SetActive(true);
            EndLevelElements.StartEnabling();
        }
        {
            //Debug.LogWarning("Level ended unexpectedly");
        }
    }
}
