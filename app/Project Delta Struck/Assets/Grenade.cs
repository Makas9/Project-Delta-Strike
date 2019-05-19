using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public GameObject DeathEffect;
    public float Radius = 2f;
    public float Damage = 50f;
    public float seconds;
	// Use this for initialization
	void Start () {
        StartCoroutine(Detonate(seconds));
	}
	
    public IEnumerator Detonate(float s)
    {
        yield return new WaitForSeconds(s);
        var obj = Instantiate(DeathEffect);
        obj.transform.position = transform.position;
        DetectDamage();
        Destroy(gameObject);

    }

    public void DetectDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                var enemy = colliders[i].GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
                else if (colliders[i].transform.parent != null)
                {
                    enemy = colliders[i].transform.parent.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(Damage);
                    }
                }
                else
                {
                    var player = colliders[i].GetComponent<PlayerHealth>();
                    if (player != null)
                    {
                        player.TakeDamage(Damage);
                    }
                }
            }
            
        }
    }
}
