using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DepressionIndicator : MonoBehaviour
{

	public Text UIText;
	public Text insanityIndicator;

	float timeRemaining = 20f;
	int numberPercentage = 0;

	public bool interactedWithNewspaper = false;


	// Use this for initialization
	void Start()
	{

		insanityIndicator.text = "Insanity: " + numberPercentage.ToString() + "0%";

	}

	// Update is called once per frame
	void Update()
	{

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
			insanityIncreases(itemInfo.gameObject, 1,
							  "A dead rat on the dining table. Gross." +
							  "\nSeems like the blood is still fresh.");
		}
		else if (itemInfo.CompareTag("Newspaper"))
		{

			if (interactedWithNewspaper == false)
			{
				insanityDecreases(itemInfo.gameObject, 1,
								  "You see scratched newpaper scattering on the floor, covered with bloodmarks" +
								  "\nWhere did your cat get all these newspapers?");
				
				StartCoroutine(restoreTriggerAfterSeconds(5, itemInfo.gameObject, interactedWithNewspaper));
			}
			else {
				insanityIncreases(itemInfo.gameObject, 1,
				  "Under the newspapers you find a sticky note." +
				  "\nThe password to the safe is written on it.");
			}
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

	IEnumerator restoreTriggerAfterSeconds(int sec, GameObject gameObj, bool interacted)
	{
		if (Input.GetKey(KeyCode.Space))
		{
			interacted = true;
			yield return new WaitForSeconds(sec);
			gameObj.GetComponent<BoxCollider>().enabled = true;
		}
	}

	void insanityIncreases(GameObject obj, int incrementNum, string displayText)
	{
		if (Input.GetKey(KeyCode.Space))
		{
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


