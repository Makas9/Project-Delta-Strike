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
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

        if (HasFallen())
        {
            Respawn();
        }
    }

    public bool HasFallen()
    {
        return transform.position.y < -5;
    }

    public void Respawn()
    {
        StartCoroutine(LateRespawn());
    }

    IEnumerator LateRespawn()
    {
        Camera2DFollow.Instance.enabled = false;
        controller.m_Rigidbody2D.freezeRotation = false;
        animator.SetTrigger("Fall");
        yield return new WaitForSecondsRealtime(SecondsToRespawn);
        controller.m_Rigidbody2D.position = InitialPosition.position;
        if (transform.forward.x < 0)
        {
            controller.Flip();
        }
        animator.SetTrigger("StandUp");
        controller.m_Rigidbody2D.freezeRotation = true;
        Camera2DFollow.Instance.enabled = true;
    }

    public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
