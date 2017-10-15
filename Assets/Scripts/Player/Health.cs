using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	private RectTransform healthBar;
	private Animator anim;
	private Text healthDisplay;

	private bool isDead;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		healthBar = GameObject.Find ("HealthBar").GetComponent<RectTransform>();
		healthDisplay = healthBar.FindChild ("Health").GetComponent<Text>();
	}

	void Update()
	{
		if(isDead == false)
		{
			healthDisplay.text = ((int)(healthBar.sizeDelta.x * 10)).ToString ();
			if(healthBar.sizeDelta.x <= 0)
			{
				//Set bool isDead to true to stop this if statement
				isDead = true;

				//Make the bar and the number on GUI be 0
				healthBar.sizeDelta = Vector2.zero;
				healthDisplay.text = "0";

				//!Enable scripts 
				GetComponent<PlayerController>().enabled = false;
				GetComponent<UnityStandardAssets.Utility.SimpleMouseRotator>().enabled = false;
				GameObject.Find("Player Camera").GetComponent<UnityStandardAssets.Utility.SimpleMouseRotator>().enabled = false;

				//Set true to isDead in animator and then set false after 0.15f
				anim.SetBool ("isDead", true);
				Invoke("OnDying", 0.15f);
			}
			else if(healthBar.sizeDelta.x < 100)
			{
				healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x + 5 * Time.deltaTime, healthBar.sizeDelta.y);
			}
		}
	}

	void OnDying()
	{
		anim.SetBool ("isDead", false);
	}
}
