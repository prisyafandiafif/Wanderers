using UnityEngine;
using System.Collections;

public class BlockMeteorAbility : MonoBehaviour 
{
	public int abilityID;

	public int changeValue;
	public string affinity;
	
	public string abilityType;

	private GameManager gameManagerInstance;	

	// Use this for initialization
	void Start () 
	{
		gameManagerInstance = this.gameObject.transform.parent.gameObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Reset (bool isPlayer)
	{
		Debug.Log("Reset Block Meteor DEBUFF!");

		if (isPlayer)
		{
			for (int i = 0; i < gameManagerInstance.playerListOfAbilities.Count; i++)
			{
				if (gameManagerInstance.playerListOfAbilities[i].resourceType == affinity)
				{
					gameManagerInstance.playerListOfAbilities[i].resourceCount = gameManagerInstance.abilities[gameManagerInstance.playerListOfAbilities[i].id].resourceCount;
				}
			}
		}
		else
		{
			for (int i = 0; i < gameManagerInstance.enemyListOfAbilities.Count; i++)
			{
				if (gameManagerInstance.enemyListOfAbilities[i].resourceType == affinity)
				{
					gameManagerInstance.enemyListOfAbilities[i].resourceCount = gameManagerInstance.abilities[gameManagerInstance.enemyListOfAbilities[i].id].resourceCount;
				}
			}
		}
	}

	public void Execute (GameObject page)
	{
		Debug.Log("Execute Block Meteor DEBUFF!");

		//if this is player's turn
		if (page == gameManagerInstance.playerPageGameObject)
		{
			//if debuff
			if (abilityType == "debuff")
			{
				//find all abilities that have the same affinity with this affinity's ability
				for (int i = 0; i < gameManagerInstance.enemyListOfAbilities.Count; i++)
				{
					if (gameManagerInstance.enemyListOfAbilities[i].resourceType == affinity)
					{
						if (changeValue == -100)
						{
							gameManagerInstance.playerListOfAbilities[i].resourceCount = 0; 
						}
						else
						if (gameManagerInstance.enemyListOfAbilities[i].resourceCount + changeValue >= 5 || gameManagerInstance.enemyListOfAbilities[i].resourceCount + changeValue <= 0)
						{
							//do nothing
						} 
						else
						{
							gameManagerInstance.enemyListOfAbilities[i].resourceCount += changeValue; 
						}
					}
				}

				//if the current status effect is not the same
				if (gameManagerInstance.enemyStatusEffect != abilityID)
				{
					gameManagerInstance.enemyStatusEffect = abilityID;

					/*if (gameManagerInstance.playerTurnCount == 1)
					{
						gameManagerInstance.enemyStatusValidCounter = gameManagerInstance.abilities[abilityID].validTime;
					}
					else
					{*/
					gameManagerInstance.enemyStatusValidCounter = gameManagerInstance.abilities[abilityID].validTime;
					//}
				}
				else
				{
					gameManagerInstance.enemyStatusValidCounter += gameManagerInstance.abilities[abilityID].validTime;
				}
			}
			else
			//if buff
			if (abilityType == "buff")
			{

			}
		}
		else
		{
			//if debuff
			if (abilityType == "debuff")
			{
				//find all abilities that have the same affinity with this affinity's ability
				for (int i = 0; i < gameManagerInstance.playerListOfAbilities.Count; i++)
				{
					if (gameManagerInstance.playerListOfAbilities[i].resourceType == affinity)
					{
						if (changeValue == -100)
						{
							gameManagerInstance.playerListOfAbilities[i].resourceCount = 0; 
						}
						else
						if (gameManagerInstance.playerListOfAbilities[i].resourceCount + changeValue >= 5 || gameManagerInstance.playerListOfAbilities[i].resourceCount + changeValue <= 0)
						{
							//do nothing
						} 
						else
						{
							gameManagerInstance.playerListOfAbilities[i].resourceCount += changeValue; 
						}
					}
				}

				//if the current status effect is not the same
				if (gameManagerInstance.playerStatusEffect != abilityID)
				{
					gameManagerInstance.playerStatusEffect = abilityID;

					/*if (gameManagerInstance.enemyTurnCount == 1)
					{
						gameManagerInstance.playerStatusValidCounter = gameManagerInstance.abilities[abilityID].validTime;
					}
					else
					{*/
					gameManagerInstance.playerStatusValidCounter = gameManagerInstance.abilities[abilityID].validTime;
					//}
				}
				else
				{
					gameManagerInstance.playerStatusValidCounter += gameManagerInstance.abilities[abilityID].validTime;
				}
			}
			else
			//if buff
			if (abilityType == "buff")
			{

			}
		}

		
	}
}

