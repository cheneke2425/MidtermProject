using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DepressionIndicator : MonoBehaviour {

	public Text UIText;

	float timeRemaining = 20f;
	int numberPercentage = 0;


	// Use this for initialization
	void Start () {
		
		UIText.text = "Depression Level: " + numberPercentage.ToString() + "0%";
		
	}
	
	// Update is called once per frame
	void Update () {

		timeRemaining -= Time.deltaTime;

		if (timeRemaining < 0.99f)
		{
			numberPercentage += 1;
			UIText.text = "Depression Level: " + numberPercentage.ToString() + "0%";

			timeRemaining = 20f;
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
		UIText.text = "Press 'SPACE' to pick up";

		if (Input.GetKey(KeyCode.Space))
		{
			if (itemInfo.CompareTag("Clue"))
			{
				numberPercentage -= 1;
				UIText.text = "You find something good and become slighly less depressed! " +
						  "Depression Level: " + numberPercentage.ToString() + "0%";
				timeRemaining = 20f;
			}
			else if (itemInfo.CompareTag("BadStuff"))
			{
				numberPercentage += 1;
				UIText.text = "You find something very bad and become more depressed! " +
						  "Depression Level: " + numberPercentage.ToString() + "0%";
				timeRemaining = 20f;
			}
			else if (itemInfo.CompareTag("Cat"))
			{
				UIText.text = "You've found the cat! You win!";
			}

			itemInfo.gameObject.SetActive(false);
		}

	}

}
