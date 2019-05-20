using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItems : MonoBehaviour {

    // Use this for initialization
    public Transform WeaponParent;
    public Transform KnifeParent;
    public Transform VestParent;
    public Transform HalmetParent;
	void Start ()
    {
        EquipVest();
        EquipWeapon();
        EquipKnife();
        EquipHalmet();
    }


    public void EquipVest()
    {
        GameObject prefab = Resources.Load<GameObject>("GamePrefabs/Vests/" + Data.Instance.CurrentVest);
        GameObject item = Instantiate(prefab, VestParent, false);
        item.transform.parent = VestParent;
        item.transform.localPosition = Vector3.zero;
    }
    public void EquipWeapon()
    {
        GameObject prefab = Resources.Load<GameObject>("GamePrefabs/Guns/" + Data.Instance.CurrentGun);
        GameObject item = Instantiate(prefab, WeaponParent, false);
        item.transform.parent = WeaponParent;
        Data.Instance.PrimaryWeapon = Data.Instance.GetGunSettings(Data.Instance.CurrentGun);
    }
    public void EquipKnife()
    {
        GameObject prefab = Resources.Load<GameObject>("GamePrefabs/Knives/" + Data.Instance.CurrentKnife);
        GameObject item = Instantiate(prefab, KnifeParent, false);
        item.transform.parent = KnifeParent;
        KnifeParent.gameObject.SetActive(false);
    }
    public void EquipHalmet()
    {
        GameObject prefab = Resources.Load<GameObject>("GamePrefabs/Halmets/" + Data.Instance.CurrentHalmet);
        GameObject item = Instantiate(prefab, HalmetParent, false);

        item.transform.parent = HalmetParent;
        item.transform.localPosition = Vector3.zero;
    }
}
