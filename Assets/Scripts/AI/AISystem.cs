using UnityEngine;
using System.Collections;

public class AISystem : MonoBehaviour {

	private Transform player;
	private UnityEngine.AI.NavMeshAgent agent;
	private Vector3 destination;

	private Vector3 bulletPosition;
	private Quaternion bulletRotation;
	private Transform gun;

	private bool isChasing;

	void Start()
	{
		InvokeRepeating ("OnShooting", 0, 2);
	}

	void Awake()
	{
		player = GameObject.Find ("Player").transform.FindChild("Object001");
		agent = transform.root.GetComponent<UnityEngine.AI.NavMeshAgent>();
		gun = transform.root.FindChild ("Body").FindChild ("Gun Pivot");
	}

	void Update()
	{
		//Draw a shooting distance
		Debug.DrawLine(gun.position, new Vector3(player.position.x, player.position.y + 2, player.position.z), Color.red);

		bulletPosition = gun.FindChild ("Gun").FindChild ("Bullet Position").position;
		bulletRotation = gun.FindChild ("Gun").FindChild ("Bullet Position").rotation;

		OnDrawingAngle();
	}

	void OnDrawingAngle()
	{
		//Debug the angle to see whether it is smaller or equal to 30
		Debug.DrawRay(transform.position, transform.forward * 40, Color.yellow);
		Debug.DrawLine(transform.position, new Vector3(player.position.x, player.position.y + 2, player.position.z), Color.green);

		//Calculate the distance from the player to the enemy
		Vector3 heading = new Vector3(player.position.x, player.position.y + 2, player.position.z) - transform.position;
		float distanceToPlayer = heading.magnitude;
		Vector3 direction = heading/distanceToPlayer;

		//Setup the variables for ray
		RaycastHit hitInfo;
		//Draw a ray to test if the player was in 70m
		//And then calculate the absolute value of the angle from a forward line to the player position

		if(Physics.Raycast (new Ray(transform.position, direction), out hitInfo, 70))
		{
			if(hitInfo.transform.root.tag == "Player")
			{
				if(Mathf.Abs (Vector3.Angle (transform.forward, heading)) <= 50)
				{
					OnChasing (new Vector3(player.position.x, player.position.y + 2, player.position.z));
					return;
				}
			}
			OnRoaming();
		}
	}

	void OnRoaming()
	{
		isChasing = false;
		agent.SetDestination (destination);
	}

	void OnChasing(Vector3 playerPosition)
	{
		isChasing = true;
		agent.SetDestination (playerPosition);
	}

	public void getDestination(Vector3 _destination)
	{
		destination = _destination;
	}

	void OnShooting()
	{
		if(isChasing)
		{
			gun.LookAt (new Vector3(player.position.x, player.position.y + 2, player.position.z));
			Instantiate(Resources.Load ("bullet"), bulletPosition, bulletRotation);
		}
	}
}
