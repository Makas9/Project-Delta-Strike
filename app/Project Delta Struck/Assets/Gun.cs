using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public enum Type { Handgun, Automatic }
    public Type type;
    GameObject MuzzleFlash;
	// Use this for initialization
	void Start () {
        MuzzleFlash = transform.Find("MuzzleFlash").gameObject;
	}

    public Transform firePoint;
    public int damage = 40;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!PlayerHealth.Instance.Fire()) return;
            StartCoroutine(Shoot());
            StartCoroutine(EnableForFrame(MuzzleFlash));
        }
    }


    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        lineRenderer.enabled = false;

        
    }

    IEnumerator EnableForFrame(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        obj.SetActive(false);
    }
}
