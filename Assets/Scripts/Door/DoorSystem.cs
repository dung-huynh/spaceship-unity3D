using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour {

	private float speed = 5f;
	private bool isOpen = false;

	private float openLeftSide, openRightSide;
	private float closedLeftSide, closedRightSide;
	private float leftX, rightX;
	private Transform leftSide, rightSide;

	void Start()
	{
		leftSide = transform.FindChild ("DoorL");
		rightSide = transform.FindChild("DoorR");

		leftX = transform.FindChild ("DoorL").localPosition.x;
		rightX = transform.FindChild("DoorR").localPosition.x;

		closedLeftSide = transform.FindChild ("DoorL").localPosition.x;
		closedRightSide = transform.FindChild("DoorR").localPosition.x;

		openLeftSide = closedLeftSide + 0.3f;
		openRightSide = closedRightSide - 0.3f;
	}

	void Update()
	{
		OnCondition ();
		OnAnimatingDoors ();
	}

	void OnAnimatingDoors()
	{
		if(isOpen)
			{
				leftX = Mathf.Lerp (leftX, openLeftSide, Time.deltaTime * speed);
				rightX = Mathf.Lerp (rightX, openRightSide, Time.deltaTime * speed);
			}
		else
			{
				leftX = Mathf.Lerp (leftX, closedLeftSide, Time.deltaTime * speed);
				rightX = Mathf.Lerp (rightX, closedRightSide, Time.deltaTime * speed);
			}
			
			leftSide.localPosition = new Vector3(leftX, leftSide.localPosition.y, leftSide.localPosition.z);
			rightSide.localPosition = new Vector3(rightX, rightSide.localPosition.y, rightSide.localPosition.z);
	}

	void OnCondition()
	{
		if(Vector3.Distance(transform.position, GameObject.Find ("Player").transform.position) <= 5)
		{
			isOpen = true;
			return;
		}
		isOpen = false;
	}
}
