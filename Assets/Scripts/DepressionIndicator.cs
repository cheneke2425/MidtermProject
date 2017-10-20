using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DepressionIndicator : MonoBehaviour {

	public Text UIText;
	public Text depressionIndicator;

	float timeRemaining = 20f;
	int numberPercentage = 0;


	// Use this for initialization
	void Start () {
		
		depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";
		
	}
	
	// Update is called once per frame
	void Update () {

		timeRemaining -= Time.deltaTime;

		if (timeRemaining < 0.99f)
		{
			numberPercentage += 1;
			depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";

			timeRemaining = 15f;
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
			if (itemInfo.CompareTag("Clue"))
			{
				numberPercentage -= 1;
				UIText.text = "You find something good and become slighly less depressed! ";
				depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";
				timeRemaining = 20f;

				//StartCoroutine(deleteTextAfterSeconds(2));
			}
			else if (itemInfo.CompareTag("BadStuff"))
			{
				numberPercentage += 1;
				UIText.text = "You find something very bad and become more depressed! ";
				depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";
				timeRemaining = 20f;

				//StartCoroutine(deleteTextAfterSeconds(2));
			}
			else if (itemInfo.CompareTag("Couch"))
			{
				numberPercentage += 1;
				UIText.text = "Normally your cat would be sleeping on the couch.\nBut he is not here today.\nYou become more depressed.";
				depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";
				timeRemaining = 20f;

				//StartCoroutine(deleteTextAfterSeconds(5));
			}
			else if (itemInfo.CompareTag("AC"))
			{
				if (numberPercentage == 0)
				{
					numberPercentage = 0;
				}
				else {
					numberPercentage -= 1;
				}
				UIText.text = "You see some white fur behind the AC. Your cat was here before.";
				depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";
				timeRemaining = 20f;

				//StartCoroutine(deleteTextAfterSeconds(5));
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
		depressionIndicator.text = "Depression Level: " + numberPercentage.ToString() + "0%";
	}

	IEnumerator deleteTextAfterSeconds(int sec)
	{
		yield return new WaitForSeconds(sec);
		UIText.text = "";
	}

}
