using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator anim;
	private Rigidbody rigid;
	private CapsuleCollider col;

	public Transform rFoot;
	public Transform lFoot;

	private float speed;
	private bool isCarrying = false;
	private bool isShooting = false;

	void Start()
	{
		speed = 0;
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Q))
		{
			isCarrying = !isCarrying;
			anim.SetBool ("isCarrying", isCarrying);
		}
		if(Input.GetMouseButton (0))
		{
			isShooting = true;
		}
		else if(Input.GetMouseButtonUp (0))
		{
			isShooting = false;
		}

		anim.SetBool ("isShooting", isShooting);
	}

	void FixedUpdate()
	{
		if(anim.GetFloat ("VSpeed") > 1 || anim.GetFloat ("HSpeed") > 1)
		{
			rFoot.GetComponent<BoxCollider>().isTrigger = true;
			lFoot.GetComponent<BoxCollider>().isTrigger = true;
		}
		else
		{
			rFoot.GetComponent<BoxCollider>().isTrigger = false;
			lFoot.GetComponent<BoxCollider>().isTrigger = false;
		}

		OnMoving();
		OnJumping();

		Debug.DrawRay(rFoot.position, -transform.up * 0.15f);
		Debug.DrawRay(lFoot.position, -transform.up * 0.15f);
	}

	bool isGrounded()
	{
		//Draw a ray to detect the ground
		if(Physics.Raycast(lFoot.position, -transform.up, 0.15f) || Physics.Raycast(rFoot.position, -transform.up, 0.15f))
		{
			return true;
		}
		return false;
	}

	void OnMoving()
	{
		//Check the condition whether the player is running or walking and 
		//then set the value to the variables in Animator component

		if(Input.GetAxis ("Vertical") > 0)
		{
			if(Input.GetKey (KeyCode.LeftShift))
			{
				if(speed < 2)
				{
					speed += Time.deltaTime * 2;
				}
			}
			else
			{
				if(speed < 1)
				{
					speed += Time.deltaTime;
				}
				else if(speed > 1)
				{
					speed -= Time.deltaTime;
				}
			}
		}
		anim.SetFloat ("VSpeed", Input.GetAxis ("Vertical") * speed);
		transform.localPosition += transform.forward * anim.GetFloat ("VSpeed") * speed * Time.deltaTime;

		anim.SetFloat ("HSpeed", Input.GetAxis ("Horizontal") * speed);
		transform.localPosition += transform.right * anim.GetFloat ("HSpeed") * speed * 2 * Time.deltaTime;
	}

	void OnJumping()
	{
		if(Input.GetKeyDown (KeyCode.Space))
		{
			if(isGrounded())
			{
				anim.SetBool ("isGrounded", true);
				rigid.velocity = 8 * transform.forward + 4 * transform.up;
				rigid.AddForce (transform.forward * anim.GetFloat ("VSpeed") * Time.fixedDeltaTime * 20);
				rigid.AddForce (transform.right * anim.GetFloat ("HSpeed") * Time.fixedDeltaTime * 2);

				Invoke ("cancelJumping", 0.15f);
			}
		}
	}

	void cancelJumping()
	{
		anim.SetBool ("isGrounded", false);
	}
}
