using UnityEngine;
using System.Collections;

public class GrantAbility : MonoBehaviour 
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
		Debug.Log("Reset Grant BUFF!");

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
		Debug.Log("Execute Grant BUFF!");

		//if this is enemy's turn
		if (page == gameManagerInstance.enemyPageGameObject)
		{
			//if buff
			if (abilityType == "buff")
			{
				//find all abilities that have the same affinity with this affinity's ability
				for (int i = 0; i < gameManagerInstance.enemyListOfAbilities.Count; i++)
				{
					if (gameManagerInstance.enemyListOfAbilities[i].resourceType == affinity)
					{
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

					gameManagerInstance.enemyStatusValidCounter = gameManagerInstance.abilities[abilityID].validTime + 1;
				}
				else
				{
					gameManagerInstance.enemyStatusValidCounter += gameManagerInstance.abilities[abilityID].validTime;
				}
			}
			else
			//if debuff
			if (abilityType == "debuff")
			{

			}
		}
		else
		{
			//if buff
			if (abilityType == "buff")
			{
				//find all abilities that have the same affinity with this affinity's ability
				for (int i = 0; i < gameManagerInstance.playerListOfAbilities.Count; i++)
				{
					if (gameManagerInstance.playerListOfAbilities[i].resourceType == affinity)
					{
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

					gameManagerInstance.playerStatusValidCounter = gameManagerInstance.abilities[abilityID].validTime + 1;
				}
				else
				{
					gameManagerInstance.playerStatusValidCounter += gameManagerInstance.abilities[abilityID].validTime;
				}
			}
			else
			//if debuff
			if (abilityType == "debuff")
			{

			}
		}

		
	}
}

