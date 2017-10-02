using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressionIndicator : MonoBehaviour {

	float timeRemaining = 20f;
	int numberPercentage = 10;


	// Use this for initialization
	void Start () {
		
		Debug.Log("Depression Level: " + numberPercentage.ToString() + "0%");
		
	}
	
	// Update is called once per frame
	void Update () {

		timeRemaining -= Time.deltaTime;

		if (timeRemaining < 0.99f)
		{
			numberPercentage -= 1;
			Debug.Log("Depression Level: " + numberPercentage.ToString() + "0%");

			timeRemaining = 20f;
		}
		
		if (numberPercentage < 0)
		{
			Lose();
		}
		
	}

	void Lose()
	{
		Debug.Log("You Lose");
	}

	void OnTriggerEnter(Collider itemInfo)
	{
		if (itemInfo.CompareTag("Clue"))
		{
			numberPercentage += 1;
			Debug.Log("You find a clue and become slighly less depressed! " +
					  "Depression Level: " + numberPercentage.ToString() + "0%");
			timeRemaining = 20f;
		}
		else if (itemInfo.CompareTag("BadStuff"))
		{
			numberPercentage -= 1;
			Debug.Log("You find something very bad and become more depressed! " +
					  "Depression Level: " + numberPercentage.ToString() + "0%");
			timeRemaining = 20f;
		}

		itemInfo.gameObject.SetActive(false);
	}
}
