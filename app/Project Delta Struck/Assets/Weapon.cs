using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public static Weapon Instance;
    public enum Type { Gun, Knife }
    public Type type;
	// Use this for initialization
	void Start () {
        Instance = this;
	}

    public Transform firePoint;
    public int damage = 40;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!PlayerHealth.Instance.CanFire()) return;
            PlayerMovement.Instance.OnFire();
        }
    }

    public void Fire()
    {
            print(type);
            print(transform.name);
        switch (type)
        {
            case Type.Gun:
                StartCoroutine(Shoot());
                StartCoroutine(EnableForFrame(firePoint.gameObject));
                break;
            case Type.Knife:
                Stab();
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (gameObject.activeInHierarchy)
        {
            switch (type)
            {
                case Type.Knife:
                    Gizmos.DrawLine(firePoint.position, new Vector3(firePoint.position.x + 0.5f, firePoint.position.y, 0));
                    break;
            }
        }
    }

    void Stab()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, transform.right, 0.5f, ~(1 << PlayerMovement.Instance.gameObject.layer));

        if (hitInfo)
        {
            print(hitInfo.collider.name);
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, transform.right, 200, ~(1 << PlayerMovement.Instance.gameObject.layer));
        if (hitInfo)
        {
            print(hitInfo.collider.name);
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
        }
        else
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
            }
        }
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;

            yield return new WaitForSeconds(0.03f);

            lineRenderer.enabled = false;
        }
    }

    IEnumerator EnableForFrame(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        obj.SetActive(false);
    }
}
