using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 1f;
    public bool JustTurnedAround = false;
    public float TurnAroundDuration = 1f;
    public int health = 100;
    public GameObject deathEffect;
    public float CliffDetectionDistance = 1f;
    public int Damage = 10;
    public Transform HealthBarLocation;
    SimpleHealthBar HealthBar;
    void Awake()
    {
        GameObject Bar = Instantiate(GameMaster.Instance.SimpleBarPrefab, GameMaster.Instance.GameCanvas.transform);
        HealthBar = Bar.transform.Find("Fill").GetComponent<SimpleHealthBar>();
        ScreenWorldFollow follow = Bar.GetComponent<ScreenWorldFollow>();
        follow.target = transform;
        follow.yOffset = HealthBarLocation.position.y - transform.position.y;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        HealthBar.UpdateBar(health, 100);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(HealthBar.transform.parent.gameObject);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        DetectCliff();

    }
    public void DetectCliff()
    {
        if (JustTurnedAround) return;
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, CliffDetectionDistance, ~(1 << gameObject.layer));
        if (groundInfo.collider == null)
        {
            TurnAround();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)Vector2.down * CliffDetectionDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            print(collision.collider.tag);
            PlayerHealth.Instance.TakeDamage(Damage);
        }
        if (JustTurnedAround) return;
        if (collision.collider.CompareTag("Ground")) return;
        TurnAround();
    }

    public void TurnAround()
    {
        StartCoroutine(TurnAround(TurnAroundDuration));
    }

    IEnumerator TurnAround(float seconds)
    {
        transform.Rotate(new Vector3(0, 180, 0));
        JustTurnedAround = true;
        yield return new WaitForSeconds(seconds);
        JustTurnedAround = false;
    }
}
