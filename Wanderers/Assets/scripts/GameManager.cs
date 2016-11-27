using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public GameObject normalFullScreenTransitionGameObject;

	public GameObject enemyOpeningPageGameObject;

	public GameObject playerPageGameObject;

	public GameObject rubiksSideGameObject;

	#region ABILITIES VARIABLES
	[System.Serializable]
	public class Abilities 
	{
		public string name;
		public string description;
		public Sprite sprite;
		public string type; //att, debuff, buff
		public int resourceCount;
		public string resourceType; //meteor, moon, star
		public int validTime; //only for debuff or buff
	}
	#endregion

	public List<Abilities> abilities;

	public int rubiksSide;

	#region PLAYER INFO VARIABLES
	public int playerCurrentHP = 50;
	public int playerTotalHP = 50;
	public int playerStatusEffect = -1;
	public int playerStatusValidCounter = 3;
	#endregion

	// Use this for initialization
	void Start () 
	{
		ShowEnemyOpeningPage();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnPortraitButtonClicked ()
	{
		//play fullscreen animation
		StartCoroutine(PlayNormalFullScreenTransitionAnimation(ShowPlayerPage));
	}

	public IEnumerator PlayNormalFullScreenTransitionAnimation (Action functionToCall)
	{
		normalFullScreenTransitionGameObject.SetActive(true);

		normalFullScreenTransitionGameObject.transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.4f);

		functionToCall();

		normalFullScreenTransitionGameObject.transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.4f);

		normalFullScreenTransitionGameObject.SetActive(false);
	}

	public void OnAbilityButtonClicked (GameObject button)
	{
		//hide defend button and show confirm and reset buttons
		playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(false);
		playerPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(true);

		GameObject parentAbilitiesGameObject = button.transform.parent.gameObject;

		int totalSelectedResourceCount = 0;

		//get all total resource count that has been selected
		for (int i = 0; i < parentAbilitiesGameObject.transform.childCount; i++)
		{
			int counter = 0;

			for (int j = 0; j < parentAbilitiesGameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.childCount; j++)
			{
				if (parentAbilitiesGameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.GetChild(j).gameObject.activeSelf)
				{
					counter = j + 1;

					break;
				}
			}

			totalSelectedResourceCount += counter * int.Parse(parentAbilitiesGameObject.transform.GetChild(i).gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>().text);
		}	

		Debug.Log("Total Selected Resource Count: " + totalSelectedResourceCount);

		Debug.Log("Selected Resource Count: " + int.Parse(button.transform.FindChild("Text").gameObject.GetComponent<Text>().text));

		//check if still valid
		if (totalSelectedResourceCount + int.Parse(button.transform.FindChild("Text").gameObject.GetComponent<Text>().text) <= 4)
		{
			//increase the counter and show it to the screen
			for (int i = 0; i < button.transform.FindChild("Counters").gameObject.transform.childCount; i++)
			{
				if (button.transform.FindChild("Counters").gameObject.transform.GetChild(i).gameObject.activeSelf)
				{
					button.transform.FindChild("Counters").gameObject.transform.GetChild(i).gameObject.SetActive(false);
					button.transform.FindChild("Counters").gameObject.transform.GetChild(i+1).gameObject.SetActive(true);

					break;
				}

				if (i == button.transform.FindChild("Counters").gameObject.transform.childCount - 1)
				{
					button.transform.FindChild("Counters").gameObject.transform.GetChild(0).gameObject.SetActive(true);	
				}
			}
		}
	}

	public void OnResetButtonClicked (GameObject page)
	{
		//reset all selected abilities
		for (int i = 0; i < page.transform.FindChild("Abilities").gameObject.transform.childCount; i++)
		{
			for (int j = 0; j < page.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.childCount; j++)
			{
				page.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.GetChild(j).gameObject.SetActive(false);
			}
		}

		//show defend button and hide confirm and reset buttons
		playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(true);
		playerPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(false);
	}

	public void ShowEnemyOpeningPage ()
	{
		enemyOpeningPageGameObject.SetActive(true);

		//random which Rubiks' side that player needs to use
		rubiksSide = UnityEngine.Random.Range(1, 7);

		//show Rubiks's side on the screen
		rubiksSideGameObject.transform.GetChild(rubiksSide-1).gameObject.SetActive(true);
	}

	public void ShowPlayerPage ()
	{
		playerPageGameObject.SetActive(true);

		//always show defend button in the beginning
		playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(true);

		//put current HP and total HP
		playerPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + playerCurrentHP;
		playerPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + playerTotalHP;

		//check if player has status effect or not, and change status effect image and counter text
		if (playerStatusEffect == -1)
		{
			
		}
		else
		{
			//show status effect
			playerPageGameObject.transform.FindChild("StatusEffect").gameObject.SetActive(true);

			//put status effect image
			playerPageGameObject.transform.FindChild("StatusEffect").gameObject.transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = abilities[playerStatusEffect].sprite;

			//put valid counter text for status effect
			playerPageGameObject.transform.FindChild("StatusEffect").gameObject.transform.FindChild("ValidCounterText").gameObject.GetComponent<Text>().text = "" + playerStatusValidCounter;
		}
	}
}
