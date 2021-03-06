﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DepressionIndicator : MonoBehaviour
{

	public Text UIText;
	public Text insanityIndicator;
	public Text losingText;
	public Text winningText;
	public GameObject bloodBackground;

	public Material mat;

	public AudioClip meow;
	public AudioClip clue;
	public AudioClip horror;
	public AudioClip lose;
	AudioSource source;

	float timeRemaining = 20f;
	int numberPercentage = 0;
	float exposure;

	public bool interactedWithNewspaper = false;
	public bool hasPassword = false;
	public bool hasKey = false;


	// Use this for initialization
	void Start()
	{
		exposure = 0f;
		insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
		UIText.text = "";
		losingText.text = "";
		winningText.text = "";

		bloodBackground.active = false;

	}

	// Update is called once per frame
	void Update()
	{

		timeRemaining -= Time.deltaTime;

		if (timeRemaining < 0.99f)
		{
			numberPercentage += 1;
			insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";

			timeRemaining = 15f;
		}

		if (numberPercentage == 10)
		{
			timeRemaining -= 0f;

			if (numberPercentage != 10)
			{
				numberPercentage = 10;
			}

			Lose();
		}

		exposure = Mathf.Lerp(exposure, numberPercentage * 0.1f, 0.5f * Time.deltaTime);
		RenderSettings.skybox.SetFloat("_Exposure", exposure);

		DynamicGI.UpdateEnvironment();

	}

	void Lose()
	{
		GetComponent<AudioSource>().PlayOneShot(lose, 1f);

		losingText.text = "YOU CAN NEVER FIND YOUR CAT";

		GetComponent<PlayerMovement>().enabled = false;

		bloodBackground.active = true;

	}

	void Win()
	{
		GetComponent<AudioSource>().PlayOneShot(meow, 0.3f);

		timeRemaining -= 0f;

		winningText.text = "YOUR CAT IS SMILING AT YOU IN YOUR ROOM\nCONGRATULATIONS YOU HAVE FOUND HIM";

		GetComponent<PlayerMovement>().enabled = false;

		bloodBackground.active = true;

	}

	void OnTriggerStay(Collider itemInfo)
	{
		UIText.text = "Press 'SPACE' to examine " + itemInfo.gameObject.name;

		if (itemInfo.CompareTag("Couch"))
		{
			insanityDecreases(itemInfo.gameObject, 1,
							  "Normally your cat would be sleeping on the couch." +
							  "\nBut he is not here today." +
							  "\nMaybe hiding in some corners?\nHe loves hiding.");
		}
		else if (itemInfo.CompareTag("AC"))
		{
			insanityIncreases(itemInfo.gameObject, 1,
							  "You see cockroach bodies piling up behind the AC." +
							  "\nMost cockroaches in your house come from the kitchen." +
							  "\nWhy are they here? What has your cat been doing?");
		}
		else if (itemInfo.CompareTag("Table"))
		{
			insanityIncreases(itemInfo.gameObject, 2,
							  "A dead rat on the dining table. Gross." +
							  "\nSeems like the blood is still fresh.");
		}
		else if (itemInfo.CompareTag("Books"))
		{

			if (interactedWithNewspaper == false)
			{
				insanityIncreases(itemInfo.gameObject, 1,
								  "Scratched book pages all over the floor, covered with bloodmarks." +
								  "\nWhere did your cat get all these pages?");
				if (Input.GetKey(KeyCode.Space))
				{
					interactedWithNewspaper = true;
				}

				StartCoroutine(restoreTriggerAfterSeconds(5, itemInfo.gameObject));

			}
			else {
				insanityDecreases(itemInfo.gameObject, 1,
				  "Under a piece of paper you find a sticky note." +
				  "\nSome kind of passcode is written on it.");
				
				if (Input.GetKey(KeyCode.Space))
				{
					hasPassword = true;
				}
			}
		}
		else if (itemInfo.CompareTag("Safe"))
		{
			if (hasPassword == false)
			{
				insanityIncreases(itemInfo.gameObject, 1,
								  "You don't remember you have a safe in the study, but here it is." +
								  "\nAnd you don't have the password to it.");

				StartCoroutine(restoreTriggerAfterSeconds(5, itemInfo.gameObject));

			}
			else {
				insanityDecreases(itemInfo.gameObject, 1,
								  "You open the safe with the password." +
								  "\nInside the safe there is a key.");
				
				if (Input.GetKey(KeyCode.Space))
				{
					hasKey = true;
				}

			}
		}
		else if (itemInfo.CompareTag("Door"))
		{
			if (hasKey == false)
			{
				insanityIncreases(itemInfo.gameObject, 1,
								  "Your bedroom door is locked. Weird." +
								  "\nYou have to find the key.");

				StartCoroutine(restoreTriggerAfterSeconds(5, itemInfo.gameObject));
			}
			else {

				if (Input.GetKey(KeyCode.Space))
				{
					Win();

				}

			}

		}
		else if (itemInfo.CompareTag("Shelf"))
		{
			insanityIncreases(itemInfo.gameObject, 1,
			                  "The shelf is covered with bloodmarks and scratches." +
			                  "\nYou really wish you did not see this");
		}
		else if (itemInfo.CompareTag("Cat"))
		{
			UIText.text = "You've found the cat! You win!";
		}

	}

	void OnTriggerExit()
	{
		UIText.text = "";
		insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
	}

	IEnumerator deleteTextAfterSeconds(int sec)
	{
		yield return new WaitForSeconds(sec);
		UIText.text = "";
	}

	IEnumerator restoreTriggerAfterSeconds(int sec, GameObject gameObj)
	{
		if (Input.GetKey(KeyCode.Space))
		{
			yield return new WaitForSeconds(sec);
			gameObj.GetComponent<BoxCollider>().enabled = true;
		}
	}

	void insanityIncreases(GameObject obj, int incrementNum, string displayText)
	{
		if (Input.GetKey(KeyCode.Space))
		{
			GetComponent<AudioSource>().PlayOneShot(horror, 1f);

			numberPercentage += incrementNum;
			UIText.text = displayText;
			insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
			timeRemaining = 10f;

			obj.GetComponent<BoxCollider>().enabled = false;
		}
	}

	void insanityDecreases(GameObject obj, int decrementNum, string displayText)
	{
		if (Input.GetKey(KeyCode.Space))
		{
			GetComponent<AudioSource>().PlayOneShot(clue, 1f);

			if (numberPercentage <= decrementNum)
			{
				numberPercentage = 0;
			}
			else {
				numberPercentage -= decrementNum;
			}
			UIText.text = displayText;
			insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
			timeRemaining = 10f;

			obj.GetComponent<BoxCollider>().enabled = false;
		}
	}
}


