using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour {

	public void Shoot()
    {
        print("Shooting");
        Weapon.Instance.Fire();
    }
}
