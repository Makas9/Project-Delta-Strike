using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class CollectableItem : MonoBehaviour
{
    Animator animator;

    protected bool Collected = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void PickUp()
    {
        animator.SetTrigger("ElementPickUp");
    }

    public void DestroyElement()
    {
        Destroy(gameObject);
    }
}
