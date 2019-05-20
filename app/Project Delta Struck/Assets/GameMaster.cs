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
    public GameObject Grenade;
    // Use this for initialization
    void Awake () {
        Instance = this;
        Data.Instance.PlayerData.LevelReached = int.Parse(SceneManager.GetActiveScene().name);
        moneyTxt.text = Data.Instance.PlayerData.Money.ToString();
    }

    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(Grenade);
        grenade.transform.position = PlayerMovement.Instance.transform.position;
        Rigidbody2D grenadeRb = grenade.GetComponent<Rigidbody2D>();
        grenadeRb.AddForce(PlayerMovement.Instance.controller.m_Rigidbody2D.velocity*50);
        grenadeRb.AddForce(PlayerMovement.Instance.transform.right * 150);
        grenadeRb.AddForce(PlayerMovement.Instance.transform.up * 50);
    }

    public void SwitchPrimaryWeapon()
    {
        print("Switching primary weapon");
        if (Data.Instance.PrimaryWeapon is GunSettings)
        {
            Gun.Instance.gameObject.SetActive(false);
            Knife.Instance.gameObject.SetActive(true);
            Data.Instance.PrimaryWeapon = Data.Instance.GetKnifeSettings(Data.Instance.CurrentKnife);
            Weapon.Instance = Knife.Instance.GetComponentInChildren<Weapon>();
        }
        else if (Data.Instance.PrimaryWeapon is KnifeSettings)
        {
            Gun.Instance.gameObject.SetActive(true);
            Knife.Instance.gameObject.SetActive(false);
            Data.Instance.PrimaryWeapon = Data.Instance.GetGunSettings(Data.Instance.CurrentGun);
            Weapon.Instance = Gun.Instance.GetComponentInChildren<Weapon>();
        }
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
    public bool gameIsPaused = false;
    public void Pause()
    {
        if (gameIsPaused)
        {
            Unpause();
            return;
        }
        else
        {
            Title.text = "Game paused";
            Title.gameObject.SetActive(true);
            nextLevelBtn.SetActive(false);
            nextLevelBtn.transform.parent.gameObject.SetActive(true);
            endLevelUI.SetActive(true);
            gameIsPaused = true;
        }
    }

    public void Unpause()
    {
        nextLevelBtn.transform.parent.gameObject.SetActive(false);
        Title.gameObject.SetActive(false);
        endLevelUI.SetActive(false);
        gameIsPaused = false;
    }
}
