using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObject : MonoBehaviour {
    bool Collided = false;
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (!Collided)
        {
            PlayerMovement.Instance.Respawn();
            Collided = true;
            yield return new WaitForSecondsRealtime(PlayerMovement.Instance.SecondsToRespawn);
            Collided = false;
        }
    }
}
