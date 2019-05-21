using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public static PlayerMovement Instance;
	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    public Transform InitialPosition;
    public float SecondsToRespawn = 1f;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        if (GameMaster.Instance.GameHasEnded) return;
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameMaster.Instance.SwitchPrimaryWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameMaster.Instance.ThrowGrenade();
        }
    }

    public void TurnOffControls()
    {
        animator.enabled = false;
        enabled = false;
    }

    public void OnJump()
    {
        animator.SetTrigger("Jump");
    }

    public void OnFire()
    {
        animator.SetTrigger("Fire");
    }

    public void Respawn()
    {
        StartCoroutine(LateRespawn());
    }

    IEnumerator LateRespawn()
    {
        controller.m_Rigidbody2D.freezeRotation = false;
        yield return new WaitForSecondsRealtime(SecondsToRespawn);
        controller.m_Rigidbody2D.position = InitialPosition.position;
        if (transform.forward.x < 0)
        {
            controller.Flip();
        }
        controller.m_Rigidbody2D.freezeRotation = true;
    }



	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
