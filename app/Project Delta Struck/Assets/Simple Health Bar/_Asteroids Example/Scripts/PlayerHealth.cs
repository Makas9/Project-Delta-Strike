/* Written by Kaz Crowe */
/* PlayerHealth.cs */
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    bool canTakeDamage = true;

	public int maxHealth = 100;
	float currentHealth = 100;

    float currentAmmo = 0;
	float reloadTime = 1.0f;

	public SimpleHealthBar healthBar;
	public SimpleHealthBar ammoBar;

	void Awake ()
	{
		// If the instance variable is already assigned, then there are multiple player health scripts in the scene. Inform the user.
		if( Instance != null )
			Debug.LogError( "There are multiple instances of the Player Health script. Assigning the most recent one to Instance." );
			
		// Assign the instance variable as the Player Health script on this object.
		Instance = GetComponent<PlayerHealth>();
	}



	void Start ()
	{
        SaveSystem.Instance.CallGetItems();
        StartCoroutine(LoadDataInitBar());
    }

    IEnumerator LoadDataInitBar()
    {
        // Set the current health and shield to max values.
        currentHealth = maxHealth;
        if (Data.Instance != null)
        {
            while (!Data.Instance.ItemsLoaded)
            {
                yield return null;
            }
            reloadTime = currentAmmo = Data.Instance.GetGunSettings(Data.Instance.CurrentGun).gunStats.ReloadTime;
        }
        else
            reloadTime = currentAmmo = 0.4f;
        // Update the Simple Health Bar with the updated values of Health and Shield.
        healthBar.UpdateBar(currentHealth, maxHealth);
        ammoBar.UpdateBar(currentAmmo, reloadTime);
    }

	public void HealPlayer ()
	{
		// Increase the current health by 25%.
		currentHealth += ( maxHealth / 4 );

		// If the current health is greater than max, then set it to max.
		if( currentHealth > maxHealth )
			currentHealth = maxHealth;

		// Update the Simple Health Bar with the new Health values.
		healthBar.UpdateBar( currentHealth, maxHealth );
	}

	public void TakeDamage ( int damage )
	{
		currentHealth -= damage;

		// If the health is less than zero...
		if( currentHealth <= 0 )
		{
			// Set the current health to zero.
			currentHealth = 0;

			// Run the Death function since the player has died.
			Death();
		}

		// Update the Health and Shield status bars.
		healthBar.UpdateBar( currentHealth, maxHealth );
	}

    public bool Fire()
    {
        // If the shield is less than max, and the regen cooldown is not in effect...
        if (currentAmmo == reloadTime)
        {
            StartCoroutine(ReloadBar(ammoBar));
            return true;
        }
        return false;
    }

	public void Death ()
	{
        PlayerMovement.Instance.controller.m_Rigidbody2D.freezeRotation = false;
        PlayerMovement.Instance.controller.m_Rigidbody2D.AddTorque(200);
        GameMaster.Instance.LevelEnded(false);
    }

    IEnumerator ReloadBar(SimpleHealthBar bar)
    {
        currentAmmo = 0;

        while (currentAmmo < reloadTime)
        {
            currentAmmo += Time.deltaTime * (1 / reloadTime);
            // Update the Simple Health Bar with the new Shield values.
            ammoBar.UpdateBar(currentAmmo, reloadTime);
            yield return null;
        }
        currentAmmo = reloadTime;
    }
}
