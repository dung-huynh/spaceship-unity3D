using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Explosion : MonoBehaviour {

	public GameObject upGate;
	public GameObject downGate;
	public GameObject player;

	private GameObject missile;

	void Update()
	{

	}

	void OnTriggerEnter(Collider hit)
	{
		if(hit.tag == "Explodable")
		{
			missile = hit.gameObject;
			Invoke ("Explode", 3);
		}
	}

	void Explode()
	{
		Debug.Log ("bummm");
		Destroy (missile);

		upGate.GetComponent<Rigidbody>().isKinematic = false;
		downGate.GetComponent<Rigidbody>().isKinematic = false;

		upGate.GetComponent<Rigidbody>().AddExplosionForce (200000, transform.position , 100);
		downGate.GetComponent<Rigidbody>().AddExplosionForce (200000, transform.position , 100);

		Destroy (transform.gameObject);
	}
}