using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public List<GameObject> fullScreenTransitionGameObjects;

	public GameObject enemyOpeningPageGameObject;

	#region PLAYER AND ENEMY PAGE VARIABLES
	public GameObject playerPageGameObject;
	public GameObject enemyPageGameObject;
	#endregion

	public GameObject rubiksSideGameObject;

	public int playerTurnCount = 0;
	public int enemyTurnCount = 0;

	#region ABILITIES CLASS
	[System.Serializable]
	public class Abilities
	{
		public string name;
		public int id;
		public string description;
		public Sprite sprite;
		public string type; //att, debuff, buff
		public int resourceCount;
		public string resourceType; //meteor, moon, star
		public int fullScreenTransitionAnimationID;
		public int validTime; //only for debuff or buff

		public Abilities Clone ()
		{
			Abilities copy = new Abilities();
			
			copy.id = this.id;
			copy.name = this.name;
			copy.description = this.description;
			copy.sprite = this.sprite;
			copy.type = this.type;
			copy.resourceCount = this.resourceCount;
			copy.resourceType = this.resourceType;
			copy.fullScreenTransitionAnimationID = this.fullScreenTransitionAnimationID;
			copy.validTime = this.validTime;

			return copy;
		}
	}
	#endregion

	#region ENEMIES CLASS
	[System.Serializable]
	public class Enemies
	{
		public string name;
		public int id;
		public string description;
		public string defeatedText;
		public Sprite sprite;
		public int enemyCurrentHP;
		public int enemyTotalHP;
		public int enemyStatusEffect;
		public bool isEnemyDefend;
		public string enemyDefaultAffinity;
		public int enemyStatusValidCounter;
		public List<int> listOfAbilities;

		public Enemies Clone ()
		{
			Enemies copy = new Enemies();
			
			copy.id = this.id;
			copy.name = this.name;
			copy.description = this.description;
			copy.defeatedText = this.defeatedText;
			copy.sprite = this.sprite;
			copy.enemyCurrentHP = this.enemyCurrentHP;
			copy.enemyTotalHP = this.enemyTotalHP;
			copy.enemyStatusEffect = this.enemyStatusEffect;
			copy.isEnemyDefend = this.isEnemyDefend;
			copy.enemyDefaultAffinity = this.enemyDefaultAffinity;
			copy.enemyStatusValidCounter = this.enemyStatusValidCounter;
			copy.listOfAbilities = this.listOfAbilities;

			return copy;
		}
	}
	#endregion

	public List<Abilities> abilities;
	public List<Enemies> enemies;

	public int rubiksSide;
	public int selectedEnemyID;

	#region RESOURCE TYPE VARIABLES
	public Sprite meteorIcon;
	public Sprite moonIcon;
	public Sprite starIcon;
	#endregion

	#region PLAYER INFO VARIABLES
	public int playerCurrentHP;
	public int playerTotalHP;
	public int playerStatusEffect;
	public bool isPlayerDefend;
	public string playerDefaultAffinity;
	public int playerStatusValidCounter;
	public List<Abilities> playerListOfAbilities = new List<Abilities>();
	#endregion

	#region ENEMY INFO VARIABLES
	public int enemyCurrentHP;
	public int enemyTotalHP;
	public int enemyStatusEffect;
	public bool isEnemyDefend;
	public string enemyDefaultAffinity;
	public int enemyStatusValidCounter;
	public List<Abilities> enemyListOfAbilities = new List<Abilities>();
	#endregion

	private Vector3 mousePos;
	private bool isCurrentTurnPlayer;

	// Use this for initialization
	void Awake ()
	{
		//PlayerPrefs.DeleteAll();
	}

	void Start () 
	{
		ShowEnemyOpeningPage(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CHEAT
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			PlayerPrefs.DeleteAll ();
		}

		//check swipe
		if (Input.GetMouseButtonDown(0))
		{
			mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (playerCurrentHP == 0)
			{
				return;
			}

			Vector3 tempMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

			//swipe left
			if (tempMousePos.x - mousePos.x < -0.1f)
			{
				Debug.Log("Swipe Left");

				//when it's enemy's turn
				if (enemyPageGameObject.activeSelf)
				{
					//go to player's page
					if (isCurrentTurnPlayer)
					{
						//increase player status valid counter by 1
						/*if (playerStatusValidCounter > 0)
						{
							playerStatusValidCounter += 1;
						}*/

						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowPlayerPage, false));
					}
					else
					{
						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowPlayerPage, true));
					}
				}
				else
				if (playerPageGameObject.activeSelf)
				{
					//go to enemy's page
					if (!isCurrentTurnPlayer)
					{
						//increase enemy status valid counter by 1
						/*if (enemyStatusValidCounter > 0)
						{
							enemyStatusValidCounter += 1;
						}*/

						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowEnemyPage, false));
					}
					else
					{
						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowEnemyPage, true));
					}
				}
			}
			else
			if (tempMousePos.x - mousePos.x > 0.1f)
			{
				Debug.Log("Swipe Right");

				//when it's enemy's turn
				if (enemyPageGameObject.activeSelf)
				{
					//go to player's page
					if (isCurrentTurnPlayer)
					{
						//increase player status valid counter by 1
						/*if (playerStatusValidCounter > 0)
						{
							playerStatusValidCounter += 1;
						}*/

						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowPlayerPage, false));
					}
					else
					{
						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowPlayerPage, true));
					}
				}
				else
				if (playerPageGameObject.activeSelf)
				{
					//go to enemy's page
					if (!isCurrentTurnPlayer)
					{
						//increase enemy status valid counter by 1
						/*if (enemyStatusValidCounter > 0)
						{
							enemyStatusValidCounter += 1;
						}*/

						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowEnemyPage, false));
					}
					else
					{
						//play fullscreen animation
						StartCoroutine(PlayEyeFullScreenTransitionAnimation(ShowEnemyPage, true));
					}
				}
			}
		}
	}

	public void EmptyVoid (bool isBool)
	{

	}

	public IEnumerator PlayerLoseEvent ()
	{
		yield return new WaitForSeconds(3f);

		//play fullscreen animation
		StartCoroutine(PlayNormalFullScreenTransitionAnimation(ShowEnemyOpeningPage, false));
	}

	public void OnPortraitButtonClicked ()
	{
		Abilities copy = new Abilities();

		playerListOfAbilities.Clear();
		enemyListOfAbilities.Clear();

		//set player's ability
		if (PlayerPrefs.HasKey("Player Ability 1") && 
			PlayerPrefs.HasKey("Player Ability 2") && 
			PlayerPrefs.HasKey("Player Ability 3") && 
			PlayerPrefs.HasKey("Player Ability 4") && 
			PlayerPrefs.HasKey("Player Ability 5"))
		{
			for (int i = 0; i < 5; i++)
			{
				int id = PlayerPrefs.GetInt("Player Ability " + (i+1));
	
				copy = abilities[id].Clone();
				playerListOfAbilities.Add(copy);
			}
		}
		else
		{
			copy = abilities[4].Clone();
			playerListOfAbilities.Add(copy);
			copy = abilities[3].Clone();
			playerListOfAbilities.Add(copy);
			copy = abilities[2].Clone();
			playerListOfAbilities.Add(copy);
			copy = abilities[0].Clone();
			playerListOfAbilities.Add(copy);
			copy = abilities[1].Clone();
			playerListOfAbilities.Add(copy);
		}

		//set enemy's abilities
		for (int i = 0; i < 5; i++)
		{
			copy = abilities[enemies[selectedEnemyID].listOfAbilities[i]].Clone();
			enemyListOfAbilities.Add(copy);
		}

		//reset enemy HP and other status
		enemyTotalHP = enemies[selectedEnemyID].enemyTotalHP;
		enemyCurrentHP = enemies[selectedEnemyID].enemyCurrentHP;
		isEnemyDefend = enemies[selectedEnemyID].isEnemyDefend;
		enemyStatusValidCounter = enemies[selectedEnemyID].enemyStatusValidCounter;
		enemyStatusEffect = enemies[selectedEnemyID].enemyStatusEffect;
		enemyDefaultAffinity = enemies[selectedEnemyID].enemyDefaultAffinity;

		//reset player HP and other status
		playerCurrentHP = playerTotalHP;
		isPlayerDefend = false;
		playerStatusValidCounter = 0;
		playerStatusEffect = -1;
		playerDefaultAffinity = "normal";		

		//play fullscreen animation
		StartCoroutine(PlayNormalFullScreenTransitionAnimation(ShowPlayerPage, false));
	}

	public IEnumerator PlayListOfFullScreenTransitionAnimations (Action<bool> functionToCall, List<Abilities> listOfAbilities, bool functionToCallBool)
	{
		for (int i = 0; i < listOfAbilities.Count; i++)
		{
			if (listOfAbilities[i].fullScreenTransitionAnimationID == 0)
			{
				if (i == listOfAbilities.Count - 1)
				{
					yield return StartCoroutine(PlayNormalFullScreenTransitionAnimation(functionToCall, false));
				}
				else
				{
					yield return StartCoroutine(PlayNormalFullScreenTransitionAnimation(EmptyVoid, false));
				}
			}
			else
			if (listOfAbilities[i].fullScreenTransitionAnimationID == 1)
			{
				if (i == listOfAbilities.Count - 1)
				{
					yield return StartCoroutine(PlayDefendFullScreenTransitionAnimation(functionToCall, false));
				}
				else
				{
					yield return StartCoroutine(PlayDefendFullScreenTransitionAnimation(EmptyVoid, false));
				}
			}
			else
			if (listOfAbilities[i].fullScreenTransitionAnimationID == 2)
			{
				if (i == listOfAbilities.Count - 1)
				{
					yield return StartCoroutine(PlayFireFullScreenTransitionAnimation(functionToCall, false));
				}
				else
				{
					yield return StartCoroutine(PlayFireFullScreenTransitionAnimation(EmptyVoid, false));
				}
			}
			else
			if (listOfAbilities[i].fullScreenTransitionAnimationID == 3)
			{
				if (i == listOfAbilities.Count - 1)
				{
					yield return StartCoroutine(PlayLightningFullScreenTransitionAnimation(functionToCall, false));
				}
				else
				{
					yield return StartCoroutine(PlayLightningFullScreenTransitionAnimation(EmptyVoid, false));
				}
			}
			else
			if (listOfAbilities[i].fullScreenTransitionAnimationID == 4)
			{
				if (i == listOfAbilities.Count - 1)
				{
					yield return StartCoroutine(PlayStarFullScreenTransitionAnimation(functionToCall, false));
				}
				else
				{
					yield return StartCoroutine(PlayStarFullScreenTransitionAnimation(EmptyVoid, false));
				}
			}
			else
			if (listOfAbilities[i].fullScreenTransitionAnimationID == 5)
			{
				if (i == listOfAbilities.Count - 1)
				{
					yield return StartCoroutine(PlayEyeFullScreenTransitionAnimation(functionToCall, false));
				}
				else
				{
					yield return StartCoroutine(PlayEyeFullScreenTransitionAnimation(EmptyVoid, false));
				}
			}
		}
	}

	public IEnumerator PlayNormalFullScreenTransitionAnimation (Action<bool> functionToCall, bool functionToCallBool)
	{
		fullScreenTransitionGameObjects[0].SetActive(true);

		fullScreenTransitionGameObjects[0].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.4f);

		functionToCall(functionToCallBool);

		fullScreenTransitionGameObjects[0].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.4f);

		fullScreenTransitionGameObjects[0].SetActive(false);
	}

	public IEnumerator PlayDefendFullScreenTransitionAnimation (Action<bool> functionToCall, bool functionToCallBool)
	{
		fullScreenTransitionGameObjects[1].SetActive(true);

		fullScreenTransitionGameObjects[1].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.4f);

		functionToCall(functionToCallBool);

		fullScreenTransitionGameObjects[1].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.2f);

		fullScreenTransitionGameObjects[1].SetActive(false);
	}

	public IEnumerator PlayFireFullScreenTransitionAnimation (Action<bool> functionToCall, bool functionToCallBool)
	{
		fullScreenTransitionGameObjects[2].SetActive(true);

		fullScreenTransitionGameObjects[2].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.5f);

		functionToCall(functionToCallBool);

		fullScreenTransitionGameObjects[2].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.4f);

		fullScreenTransitionGameObjects[2].SetActive(false);
	}

	public IEnumerator PlayLightningFullScreenTransitionAnimation (Action<bool> functionToCall, bool functionToCallBool)
	{
		fullScreenTransitionGameObjects[3].SetActive(true);

		fullScreenTransitionGameObjects[3].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.6f);

		functionToCall(functionToCallBool);

		fullScreenTransitionGameObjects[3].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.3f);

		fullScreenTransitionGameObjects[3].SetActive(false);
	}

	public IEnumerator PlayStarFullScreenTransitionAnimation (Action<bool> functionToCall, bool functionToCallBool)
	{
		fullScreenTransitionGameObjects[4].SetActive(true);

		fullScreenTransitionGameObjects[4].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.75f);

		functionToCall(functionToCallBool);

		fullScreenTransitionGameObjects[4].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.25f);

		fullScreenTransitionGameObjects[4].SetActive(false);
	}

	public IEnumerator PlayEyeFullScreenTransitionAnimation (Action<bool> functionToCall, bool functionToCallBool)
	{
		fullScreenTransitionGameObjects[5].SetActive(true);

		fullScreenTransitionGameObjects[5].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("start");

		yield return new WaitForSeconds(0.6f);

		functionToCall(functionToCallBool);

		fullScreenTransitionGameObjects[5].transform.FindChild("Animation").gameObject.GetComponent<Animator>().Play("end");

		yield return new WaitForSeconds(0.3f);

		fullScreenTransitionGameObjects[5].SetActive(false);
	}

	public void OnAbilityButtonClicked (GameObject button)
	{
		if (button.transform.parent.gameObject.transform.parent.gameObject == playerPageGameObject)
		{
			//hide defend button and show confirm and reset buttons
			playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(false);
			playerPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(true);
		}
		else
		{
			//hide defend button and show confirm and reset buttons
			enemyPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(false);
			enemyPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(true);
		}

		GameObject parentAbilitiesGameObject = button.transform.parent.gameObject;

		Sprite selectedResourceType = null;

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

					selectedResourceType = parentAbilitiesGameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite;

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
				//if it's the same button with the ones that have been chosen
				if (button.transform.FindChild("Counters").gameObject.transform.GetChild(i).gameObject.activeSelf)
				{
					button.transform.FindChild("Counters").gameObject.transform.GetChild(i).gameObject.SetActive(false);
					button.transform.FindChild("Counters").gameObject.transform.GetChild(i+1).gameObject.SetActive(true);

					break;
				}

				//if it's not the same button with the ones that have been chosen
				if (i == button.transform.FindChild("Counters").gameObject.transform.childCount - 1)
				{
					//if it's the same element with the nes that have been chosen
					if (button.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite == selectedResourceType || selectedResourceType == null)
					{
						button.transform.FindChild("Counters").gameObject.transform.GetChild(0).gameObject.SetActive(true);
					}	
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

		if (page == playerPageGameObject)
		{
			//show defend button and hide confirm and reset buttons
			playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(true);
			playerPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(false);
		}
		else
		{
			//show defend button and hide confirm and reset buttons
			enemyPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(true);
			enemyPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(false);
		}
	}

	public void OnConfirmButtonClicked (GameObject page)
	{
		//if enemy's turn
		if (page == enemyPageGameObject)
		{	
			//if status effect valid counter has not reached zero
			if (enemyStatusValidCounter > 0)
			{
				enemyStatusValidCounter -= 1;
			}

			List<Abilities> stackOfChosenAbilities = new List<Abilities>();

			for (int i = 0; i < page.transform.FindChild("Abilities").gameObject.transform.childCount; i++)
			{
				int counter = 0;

				for (int j = 0; j < page.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.childCount; j++)
				{
					if (page.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.GetChild(j).gameObject.activeSelf)
					{
						counter = j + 1;
	
						break;
					}
				}

				//add to the list
				for (int k = 0; k < counter; k++)
				{
					stackOfChosenAbilities.Add(enemyListOfAbilities[i]);
				}
			}

			//play the fullscreen animations if any
			if (stackOfChosenAbilities.Count > 0)
			{
				StartCoroutine(PlayListOfFullScreenTransitionAnimations(ShowPlayerPage, stackOfChosenAbilities, false));

				ExecuteChosenAbilities(stackOfChosenAbilities, page);
			}
		}
		else
		{
			//if status effect valid counter has not reached zero
			if (playerStatusValidCounter > 0)
			{
				playerStatusValidCounter -= 1;
			}

			List<Abilities> stackOfChosenAbilities = new List<Abilities>();

			for (int i = 0; i < page.transform.FindChild("Abilities").gameObject.transform.childCount; i++)
			{
				int counter = 0;

				for (int j = 0; j < page.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.childCount; j++)
				{
					if (page.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.GetChild(j).gameObject.activeSelf)
					{
						counter = j + 1;
	
						break;
					}
				}

				//add to the list
				for (int k = 0; k < counter; k++)
				{
					stackOfChosenAbilities.Add(playerListOfAbilities[i]);
				}
			}

			//play the fullscreen animations if any
			if (stackOfChosenAbilities.Count > 0)
			{
				StartCoroutine(PlayListOfFullScreenTransitionAnimations(ShowEnemyPage, stackOfChosenAbilities, false));

				ExecuteChosenAbilities(stackOfChosenAbilities, page);
			}
		}

		//change to false
		isEnemyDefend = false;
		isPlayerDefend = false;
	}

	public void OnOkayButtonClicked (GameObject page)
	{
		//if in enemy's page
		if (page == enemyPageGameObject)
		{
			//absorb enemy's abilities
			AbsorbEnemyAbilities();

			//play fullscreen animation
			StartCoroutine(PlayNormalFullScreenTransitionAnimation(ShowEnemyOpeningPage, false));
		}
		else
		{
			//do not absorb enemy's abilities
			Debug.Log("Do not absorb Enemy Abilities!");
			
			//play fullscreen animation
			StartCoroutine(PlayNormalFullScreenTransitionAnimation(ShowEnemyOpeningPage, false));
		}
	}

	public void OnDefendButtonClicked (GameObject page)
	{
		//if enemy's turn
		if (page == enemyPageGameObject)
		{
			//if status effect valid counter has not reached zero
			if (enemyStatusValidCounter > 0)
			{
				enemyStatusValidCounter -= 1;
			}

			//change to defend
			isEnemyDefend = true;
			
			//make is defend the opponent as false
			isPlayerDefend = false;

			//play fullscreen animation
			StartCoroutine(PlayDefendFullScreenTransitionAnimation(ShowPlayerPage, false));
		}
		else
		{
			//if status effect valid counter has not reached zero
			if (playerStatusValidCounter > 0)
			{
				playerStatusValidCounter -= 1;
			}

			//change to defend
			isPlayerDefend = true;

			//make is defend the opponent as false
			isEnemyDefend = false;

			//play fullscreen animation
			StartCoroutine(PlayDefendFullScreenTransitionAnimation(ShowEnemyPage, false));
		}
	}

	public void ShowEnemyOpeningPage (bool isWhat)
	{
		enemyOpeningPageGameObject.SetActive(true);
		playerPageGameObject.SetActive(false);
		enemyPageGameObject.SetActive(false);

		//random which Rubiks' side that player needs to use
		rubiksSide = UnityEngine.Random.Range(1, 7);

		//show Rubiks's side on the screen
		rubiksSideGameObject.transform.GetChild(rubiksSide-1).gameObject.SetActive(true);

		//show which enemy that will be shown randomly
		int randomEnemyID = UnityEngine.Random.Range(0, enemies.Count);
		selectedEnemyID = randomEnemyID;

		//put the selected enemy sprite on the screen
		enemyOpeningPageGameObject.transform.FindChild("PortraitButton").gameObject.GetComponent<Image>().sprite = enemies[selectedEnemyID].sprite;
	}

	public void ShowPlayerPage (bool isPeeking)
	{
		if (!isCurrentTurnPlayer)
		{
			//reset player page
			ResetPlayerPageElements();
		}

		if (!isPeeking)
		{
			if (playerCurrentHP < 0)
			{

			}
			else
			{
				if (!isCurrentTurnPlayer)
				{
					//always show defend button in the beginning
					playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(true);
				}
			}
		}

		if (!isPeeking)
		{
			isCurrentTurnPlayer = true;

			playerTurnCount += 1;

			/*if (playerTurnCount == 1)
			{
				//increase player status valid counter by 1
				if (playerStatusValidCounter > 0)
				{
					playerStatusValidCounter += 1;
				}
			}*/
		}

		enemyOpeningPageGameObject.SetActive(false);
		playerPageGameObject.SetActive(true);
		enemyPageGameObject.SetActive(false);

		if (playerCurrentHP > 0)
		{
			//put current HP and total HP
			playerPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + playerCurrentHP;
			playerPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + playerTotalHP;
		}
		else
		{
			playerCurrentHP = 0;

			//put current HP and total HP
			playerPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + playerCurrentHP;
			playerPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + playerTotalHP;

			StartCoroutine(PlayerLoseEvent());
		}

		//check if player has status effect or not, and change status effect image and counter text
		if (playerStatusEffect == -1)
		{
			playerPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.FindChild("CharacterIcon" + playerDefaultAffinity).gameObject.SetActive(true);
		}
		else
		{
			//reduce valid time by 1
			/*if (playerStatusValidCounter > 0)
			{
				
			}*/

			//if status effect valid counter has not reached zero
			if (playerStatusValidCounter > 0)
			{
				/*if (!isPeeking)
				{	
					playerStatusValidCounter -= 1;
				}*/

				//show character icons based on the status effect
				playerPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.FindChild("CharacterIcon" + abilities[playerStatusEffect].resourceType).gameObject.SetActive(true);
	
				//show status effect
				playerPageGameObject.transform.FindChild("StatusEffect").gameObject.SetActive(true);
	
				//put status effect image
				playerPageGameObject.transform.FindChild("StatusEffect").gameObject.transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = abilities[playerStatusEffect].sprite;
	
				//put valid counter text for status effect
				playerPageGameObject.transform.FindChild("StatusEffect").gameObject.transform.FindChild("ValidCounterText").gameObject.GetComponent<Text>().text = "" + playerStatusValidCounter;
			}
			else
			{
				if (playerStatusEffect != -1)
				{
					//reset resource count
					ResetChosenAbilities(playerStatusEffect, true);

					playerStatusEffect = -1;
				}

				playerPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.FindChild("CharacterIcon" + playerDefaultAffinity).gameObject.SetActive(true);
			}
		}

		if (isPeeking || playerCurrentHP == 0)
		{
			//activate blocker
			playerPageGameObject.transform.FindChild("Blocker").gameObject.SetActive(true);

			if (enemyCurrentHP == 0)
			{
				//show okay button
				playerPageGameObject.transform.FindChild("OkayButton").gameObject.SetActive(true);
			}
		}

		//show player abilities on the screen
		for (int i = 0; i < playerListOfAbilities.Count; i++)
		{
			//assign sprite
			playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("NameDescription").gameObject.GetComponent<Image>().sprite = playerListOfAbilities[i].sprite;
		
			//assign resource count
			playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "" + playerListOfAbilities[i].resourceCount;

			//asssign resource type
			if (playerListOfAbilities[i].resourceType == "meteor")
			{
				playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite = meteorIcon;
			}
			else
			if (playerListOfAbilities[i].resourceType == "star")
			{
				playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite = starIcon;
			}
			else
			if (playerListOfAbilities[i].resourceType == "moon")
			{
				playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite = moonIcon;
			}

			//disabled the button if resource count is equal to zero
			if (playerListOfAbilities[i].resourceCount == 0 || playerCurrentHP == 0)
			{
				playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Button").gameObject.GetComponent<Button>().enabled = false;
			}
			else
			{
				playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Button").gameObject.GetComponent<Button>().enabled = true;
			}
		}

	}

	public void ShowEnemyPage (bool isPeeking)
	{
		if (isCurrentTurnPlayer)
		{
			//reset enemy page
			ResetEnemyPageElements();
		}

		if (!isPeeking)
		{
			if (enemyCurrentHP < 0)
			{
				//show okay button
				enemyPageGameObject.transform.FindChild("OkayButton").gameObject.SetActive(true);

				//show defeated text
				enemyPageGameObject.transform.FindChild("Dialogue").gameObject.SetActive(true);
				enemyPageGameObject.transform.FindChild("Dialogue").gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>().text = enemies[selectedEnemyID].defeatedText;
			}
			else
			{
				if (isCurrentTurnPlayer)
				{
					//always show defend button in the beginning
					enemyPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(true);
				}
			}
		}

		if (!isPeeking)
		{
			isCurrentTurnPlayer = false;

			enemyTurnCount += 1;

			/*if (enemyTurnCount == 1)
			{
				//increase enemy status valid counter by 1
				if (enemyStatusValidCounter > 0)
				{
					enemyStatusValidCounter += 1;
				}
			}*/
		}

		enemyOpeningPageGameObject.SetActive(false);
		playerPageGameObject.SetActive(false);
		enemyPageGameObject.SetActive(true);

		if (enemyCurrentHP > 0)
		{
			//put current HP and total HP
			enemyPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + enemyCurrentHP;
			enemyPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + enemyTotalHP;
		}
		else
		{
			enemyCurrentHP = 0;
			
			//put current HP and total HP
			enemyPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + enemyCurrentHP;
			enemyPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + enemyTotalHP;
		}

		//check if enemy has status effect or not, and change status effect image and counter text
		if (enemyStatusEffect == -1)
		{
			enemyPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.FindChild("CharacterIcon" + enemyDefaultAffinity).gameObject.SetActive(true);
		}
		else
		{
			//reduce valid time by 1
			/*if (enemyStatusValidCounter > 0)
			{
				if (!isPeeking)
				{
					enemyStatusValidCounter -= 1;
				}
			}*/

			//if status effect valid counter has reached zero
			if (enemyStatusValidCounter > 0)
			{
				//show character icons based on the status effect
				enemyPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.FindChild("CharacterIcon" + abilities[enemyStatusEffect].resourceType).gameObject.SetActive(true);
	
				//show status effect
				enemyPageGameObject.transform.FindChild("StatusEffect").gameObject.SetActive(true);
	
				//put status effect image
				enemyPageGameObject.transform.FindChild("StatusEffect").gameObject.transform.FindChild("Image").gameObject.GetComponent<Image>().sprite = abilities[enemyStatusEffect].sprite;
	
				//put valid counter text for status effect
				enemyPageGameObject.transform.FindChild("StatusEffect").gameObject.transform.FindChild("ValidCounterText").gameObject.GetComponent<Text>().text = "" + enemyStatusValidCounter;
			}
			else
			{
				if (enemyStatusEffect != -1)
				{
					//reset resource count
					ResetChosenAbilities(enemyStatusEffect, false);

					enemyStatusEffect = -1;
				}

				enemyPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.FindChild("CharacterIcon" + enemyDefaultAffinity).gameObject.SetActive(true);
			}
		}

		if (isPeeking)
		{
			//activate blocker
			enemyPageGameObject.transform.FindChild("Blocker").gameObject.SetActive(true);
		}

		//show enemy abilities on the screen
		for (int i = 0; i < enemyListOfAbilities.Count; i++)
		{
			//assign sprite
			enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("NameDescription").gameObject.GetComponent<Image>().sprite = enemyListOfAbilities[i].sprite;
		
			//assign resource count
			enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "" + enemyListOfAbilities[i].resourceCount;

			//asssign resource type
			if (enemyListOfAbilities[i].resourceType == "meteor")
			{
				enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite = meteorIcon;
			}
			else
			if (enemyListOfAbilities[i].resourceType == "star")
			{
				enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite = starIcon;
			}
			else
			if (enemyListOfAbilities[i].resourceType == "moon")
			{
				enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Icon").gameObject.GetComponent<Image>().sprite = moonIcon;
			}

			//disabled the button if resource count is equal to zero
			if (enemyListOfAbilities[i].resourceCount == 0 || enemyCurrentHP == 0)
			{
				enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Button").gameObject.GetComponent<Button>().enabled = false;
			}
			else
			{
				enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Button").gameObject.GetComponent<Button>().enabled = true;
			}
		}
	}

	public void ResetPlayerPageElements ()
	{
		playerPageGameObject.transform.FindChild("Blocker").gameObject.SetActive(false);

		playerPageGameObject.transform.FindChild("OkayButton").gameObject.SetActive(false);

		playerPageGameObject.transform.FindChild ("Dialogue").gameObject.SetActive (false);

		playerPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(false);
		playerPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(false);

		playerPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + 0;
		playerPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + 0;

		for (int i = 0; i < playerPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.childCount; i++)
		{
			playerPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.GetChild(i).gameObject.SetActive(false);
		}

		playerPageGameObject.transform.FindChild("StatusEffect").gameObject.SetActive(false);

		//reset all selected abilities
		for (int i = 0; i < playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.childCount; i++)
		{
			for (int j = 0; j < playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.childCount; j++)
			{
				playerPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.GetChild(j).gameObject.SetActive(false);
			}
		}
	}

	public void ResetEnemyPageElements ()
	{
		enemyPageGameObject.transform.FindChild("Blocker").gameObject.SetActive(false);
	
		enemyPageGameObject.transform.FindChild("OkayButton").gameObject.SetActive(false);
	
		enemyPageGameObject.transform.FindChild ("Dialogue").gameObject.SetActive (false);

		enemyPageGameObject.transform.FindChild("DefendButton").gameObject.SetActive(false);
		enemyPageGameObject.transform.FindChild("ConfirmResetButtons").gameObject.SetActive(false);
	
		enemyPageGameObject.transform.FindChild("CurrentHPText").gameObject.GetComponent<Text>().text = "" + 0;
		enemyPageGameObject.transform.FindChild("TotalHPText").gameObject.GetComponent<Text>().text = "" + 0;
	
		for (int i = 0; i < enemyPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.childCount; i++)
		{
			enemyPageGameObject.transform.FindChild("CharacterIcons").gameObject.transform.GetChild(i).gameObject.SetActive(false);
		}
	
		enemyPageGameObject.transform.FindChild("StatusEffect").gameObject.SetActive(false);
	
		//reset all selected abilities
		for (int i = 0; i < enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.childCount; i++)
		{
			for (int j = 0; j < enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.childCount; j++)
			{
				enemyPageGameObject.transform.FindChild("Abilities").gameObject.transform.GetChild(i).gameObject.transform.FindChild("Counters").gameObject.transform.GetChild(j).gameObject.SetActive(false);
			}
		}
	}

	#region LOGIC OF ABILITIES
	public void AbsorbEnemyAbilities ()
	{
		Debug.Log("Absorb Enemy Abilities!");

		//save to player prefs
		for (int i = 0; i < enemyListOfAbilities.Count; i++)
		{
			PlayerPrefs.SetInt("Player Ability " + (i+1), enemyListOfAbilities[i].id);
		}
	}

	public void ResetChosenAbilities (int abilityID, bool isPlayer)
	{
		//if Bid (DEBUFF)
		if (abilityID == 0)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<BidAbility>().Reset(isPlayer);
		}
		else
		//if Grant (BUFF)
		if (abilityID == 1)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<GrantAbility>().Reset(isPlayer);
		}
		else
		//if Bolt (ATT)
		if (abilityID == 2)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<BoltAbility>().Reset(isPlayer);
		}
		else
		//if Ice (ATT)
		if (abilityID == 3)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<IceAbility>().Reset(isPlayer);
		}
		else
		//if Flame (ATT)
		if (abilityID == 4)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<FlameAbility>().Reset(isPlayer);
		}
		else
		//if Offer (BUFF)
		if (abilityID == 5)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<OfferAbility>().Reset(isPlayer);
		}
		else
		//if Block Meteor (DEBUFF)
		if (abilityID == 6)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<BlockMeteorAbility>().Reset(isPlayer);
		}
		else
		//if Jolt (ATT)
		if (abilityID == 7)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<JoltAbility>().Reset(isPlayer);
		}
		else
		//if Freeze (ATT)
		if (abilityID == 8)
		{
			this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<FreezeAbility>().Reset(isPlayer);
		}
	}

	public void ExecuteChosenAbilities (List<Abilities> listOfChosenAbilities, GameObject page)
	{
		for (int i = 0; i < listOfChosenAbilities.Count; i++)
		{
			Debug.Log("List of Chosen Abilities ID: " + listOfChosenAbilities[i]);

			//if Bid (DEBUFF)
			if (listOfChosenAbilities[i].name == "Bid")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<BidAbility>().Execute(page);
			}
			else
			//if Grant (BUFF)
			if (listOfChosenAbilities[i].name == "Grant")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<GrantAbility>().Execute(page);
			}
			else
			//if Bolt (ATT)
			if (listOfChosenAbilities[i].name == "Bolt")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<BoltAbility>().Execute(page);
			}
			else
			//if Ice (ATT)
			if (listOfChosenAbilities[i].name == "Ice")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<IceAbility>().Execute(page);
			}
			else
			//if Flame (ATT)
			if (listOfChosenAbilities[i].name == "Flame")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<FlameAbility>().Execute(page);
			}
			else
			//if Offer (BUFF)
			if (listOfChosenAbilities[i].name == "Offer")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<OfferAbility>().Execute(page);
			}
			else
			//if Block Meteor (DEBUFF)
			if (listOfChosenAbilities[i].name == "Block Meteor")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<BlockMeteorAbility>().Execute(page);
			}
			else
			//if Jolt (ATT)
			if (listOfChosenAbilities[i].name == "Jolt")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<JoltAbility>().Execute(page);
			}
			else
			//if Freeze (ATT)
			if (listOfChosenAbilities[i].name == "Freeze")
			{
				this.gameObject.transform.FindChild("AbilityManager").gameObject.GetComponent<FreezeAbility>().Execute(page);
			}
		}
	}
	#endregion
}
