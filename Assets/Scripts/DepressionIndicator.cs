using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DepressionIndicator : MonoBehaviour {

	public Text UIText;
	public Text insanityIndicator;

	float timeRemaining = 20f;
	int numberPercentage = 0;


	// Use this for initialization
	void Start () {
		
		insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
		
	}
	
	// Update is called once per frame
	void Update () {

		timeRemaining -= Time.deltaTime;

		if (timeRemaining < 0.99f)
		{
			numberPercentage += 1;
			insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";

			timeRemaining = 10f;
		}

		if (numberPercentage == 10)
		{
			Lose();
		}


	}

	void Lose()
	{
		UIText.text = "You Lose";

	}

	void OnTriggerStay(Collider itemInfo)
	{
		UIText.text = "Press 'SPACE' to examine " + itemInfo.gameObject.name;

		if (Input.GetKey(KeyCode.Space))
		{
			if (itemInfo.CompareTag("Couch"))
			{
				if (numberPercentage == 0)
				{
					numberPercentage = 0;
				}
				else {
					numberPercentage -= 1;
				}
				UIText.text = "Normally your cat would be sleeping on the couch.\nBut he is not here today." +
					"\nMaybe hiding in some corners?\nHe loves hiding.";
				insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
				timeRemaining = 10f;

				//StartCoroutine(deleteTextAfterSeconds(5));
			}
			else if (itemInfo.CompareTag("AC"))
			{
				numberPercentage += 1;
				UIText.text = "You see cockroach bodies piling up behind the AC.\nMost cockroaches in your house come from the kitchen." +
					"\nWhy are they here?\nWhat has your cat been doing?";
				insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
				timeRemaining = 10f;

				//StartCoroutine(deleteTextAfterSeconds(5));
			}
			else if (itemInfo.CompareTag("Table"))
			{
				numberPercentage += 2;
				UIText.text = "A dead rat on the dinint table. Gross.\nSeems like the blood is still fresh.";
				insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";
				timeRemaining = 10f;
			}
			else if (itemInfo.CompareTag("Cat"))
			{
				UIText.text = "You've found the cat! You win!";
			}

			itemInfo.gameObject.GetComponent<BoxCollider>().enabled = false;
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

}
