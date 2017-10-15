using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Rigidbody rigid;
	private RectTransform healthBar;

	private bool isHit;
	private bool isStopped;

	void Awake()
	{
		healthBar = GameObject.Find ("HealthBar").GetComponent<RectTransform>();
		rigid = GetComponent<Rigidbody>();
	}

	void Start()
	{
		rigid.AddForce (transform.forward * Time.fixedDeltaTime * 10000);
	}

	void Update()
	{
		if(isHit)
		{
			healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x - 20 * Time.fixedDeltaTime, healthBar.sizeDelta.y);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(isStopped == false)
		{
			if(col.transform.root.tag == "Player")
			{
				isHit = true;
				isStopped = true;
			}
			else
			{
				isHit = false;
			}
		}
		Destroy (transform.parent.gameObject,3);
	}
}
