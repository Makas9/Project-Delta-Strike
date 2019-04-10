using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopReferences : MonoBehaviour {
    public static ShopReferences Instance;
    public Transform SelectedOverlay;
    private void Awake()
    {
        Instance = this;
    }
}
