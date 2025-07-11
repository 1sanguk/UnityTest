using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{

	public float moveSpeed = 5f;
	public float jumpForce = 5f;
	private Vector2 moveInput;
	private Rigidbody rb;
	private bool isGrounded = true;
	private PlayerControls controls;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();

		//컨트롤 초기화
		if(controls == null)
			controls = new PlayerControls();
	}

	void OnEnable()
	{
		controls.GamePlay.Enable();
		controls.GamePlay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
		controls.GamePlay.Move.canceled += ctx => moveInput = Vector2.zero;
	}

	void OnDisable()
	{
		controls.GamePlay.Disable();
	}

	void FixedUpdate()
	{
		Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
		rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);
	}

	void Update()
	{
		if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}
	}

}