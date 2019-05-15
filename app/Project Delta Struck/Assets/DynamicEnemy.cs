using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnemy : Enemy {
    Rigidbody2D rb;
    public float MaxRotationSpeed = 10000f;
    float CurrentRotationSpeed;
    public float DefaultRotationSpeed = 5f;
    public AnimationCurve curve;
    bool Overturned = false;
    float OverturnValue;
    private void Start()
    {
        CurrentRotationSpeed = DefaultRotationSpeed;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {
        print(Vector2.up);
        print(transform.rotation.eulerAngles.normalized);
        OverturnValue = Vector2.Dot(transform.up, Vector2.up);
        RotateWithForce(transform.up);
        if (OverturnValue < 0.5f)
        {
            if (Overturned) return;
            Overturned = true;
            CurrentRotationSpeed = DefaultRotationSpeed;
            Stabilize();
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    public void Stabilize()
    {
        if (curve.keys.Length == 0) return;
        StartCoroutine(StabilizeOverTime());
    }

    IEnumerator StabilizeOverTime()
    {
        float t = 0f;

        while (t < curve.keys[curve.keys.Length - 1].value)
        {
            t += Time.deltaTime;
            CurrentRotationSpeed = curve.Evaluate(t) * MaxRotationSpeed;
            yield return null;
        }

        CurrentRotationSpeed = MaxRotationSpeed;
        Overturned = false;
    }

    public void RotateWithForce(Vector3 Axis)
    {
        float rotateAmount = Vector3.Cross(Vector3.up, Axis).z;
        rb.angularVelocity = -rotateAmount * Time.fixedDeltaTime * CurrentRotationSpeed;
    }
}
