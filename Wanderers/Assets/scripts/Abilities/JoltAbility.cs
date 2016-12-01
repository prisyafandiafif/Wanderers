using UnityEngine;
using System.Collections;

public class JoltAbility : MonoBehaviour 
{
	public int defaultDamage;
	public int affinityInNumber;
	
	public float affinityDamage;

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
		Debug.Log("Reset Jolt ATT!");
	}

	public void Execute (GameObject page)
	{
		Debug.Log("Execute Jolt ATT!");

		int playerAffinityInNumber = 0;
		int enemyAffinityInNumber = 0;
		
		//get affinity of player and enemy
		if (gameManagerInstance.playerStatusEffect == -1)
		{
			if (gameManagerInstance.playerDefaultAffinity == "moon")
			{
				playerAffinityInNumber = 3;
			}
			else
			if (gameManagerInstance.playerDefaultAffinity == "meteor")
			{
				playerAffinityInNumber = 2;
			}
			else
			if (gameManagerInstance.playerDefaultAffinity == "star")
			{
				playerAffinityInNumber = 1;
			}
		}
		else
		{
			if (gameManagerInstance.abilities[gameManagerInstance.playerStatusEffect].resourceType == "moon")
			{
				playerAffinityInNumber = 3;
			}
			else
			if (gameManagerInstance.abilities[gameManagerInstance.playerStatusEffect].resourceType == "meteor")
			{
				playerAffinityInNumber = 2;
			}
			else
			if (gameManagerInstance.abilities[gameManagerInstance.playerStatusEffect].resourceType == "star")
			{
				playerAffinityInNumber = 1;
			}
		}
		
		if (gameManagerInstance.enemyStatusEffect == -1)
		{
			if (gameManagerInstance.enemyDefaultAffinity == "moon")
			{
				enemyAffinityInNumber = 3;
			}
			else
			if (gameManagerInstance.enemyDefaultAffinity == "meteor")
			{
				enemyAffinityInNumber = 2;
			}
			else
			if (gameManagerInstance.enemyDefaultAffinity == "star")
			{
				enemyAffinityInNumber = 1;
			}
		}
		else
		{
			if (gameManagerInstance.abilities[gameManagerInstance.enemyStatusEffect].resourceType == "moon")
			{
				enemyAffinityInNumber = 3;
			}
			else
			if (gameManagerInstance.abilities[gameManagerInstance.enemyStatusEffect].resourceType == "meteor")
			{
				enemyAffinityInNumber = 2;
			}
			else
			if (gameManagerInstance.abilities[gameManagerInstance.enemyStatusEffect].resourceType == "star")
			{
				enemyAffinityInNumber = 1;
			}
		}

		//if this is enemy's turn
		if (page == gameManagerInstance.enemyPageGameObject)
		{
			//moon > meteor > star > moon
			//if player is weaker
			if (((playerAffinityInNumber == 3 && affinityInNumber == 1) || playerAffinityInNumber < affinityInNumber) && (playerAffinityInNumber != 0 && affinityInNumber != 0))
			{
				if (gameManagerInstance.isPlayerDefend)
				{
					gameManagerInstance.playerCurrentHP -= defaultDamage;
				}
				else
				{
					gameManagerInstance.playerCurrentHP = gameManagerInstance.playerCurrentHP - (defaultDamage + Mathf.FloorToInt(defaultDamage * affinityDamage));
				}
			}
			else
			//if player is stronger
			if (((playerAffinityInNumber == 1 && affinityInNumber == 3) || playerAffinityInNumber > affinityInNumber) && (playerAffinityInNumber != 0 && affinityInNumber != 0))
			{
				if (gameManagerInstance.isPlayerDefend)
				{
					gameManagerInstance.playerCurrentHP = gameManagerInstance.playerCurrentHP - (Mathf.FloorToInt(defaultDamage * 0.5f) - Mathf.FloorToInt(defaultDamage * 0.2f));
				}
				else
				{
					gameManagerInstance.playerCurrentHP = gameManagerInstance.playerCurrentHP - (defaultDamage - Mathf.FloorToInt(defaultDamage * affinityDamage));
				}
			}
			else
			//if player is equal
			if (playerAffinityInNumber == affinityInNumber || playerAffinityInNumber == 0 || affinityInNumber == 0)
			{
				if (gameManagerInstance.isPlayerDefend)
				{
					gameManagerInstance.playerCurrentHP = gameManagerInstance.playerCurrentHP - Mathf.FloorToInt(defaultDamage * 0.5f);
				}
				else
				{
					gameManagerInstance.playerCurrentHP = gameManagerInstance.playerCurrentHP - defaultDamage;
				}
			}
		}
		else
		{
			//moon > meteor > star > moon
			//if enemy is stronger
			if (((affinityInNumber == 3 && enemyAffinityInNumber == 1) || affinityInNumber < enemyAffinityInNumber) && (affinityInNumber != 0 && enemyAffinityInNumber != 0))
			{
				if (gameManagerInstance.isEnemyDefend)
				{
					gameManagerInstance.enemyCurrentHP = gameManagerInstance.enemyCurrentHP;
				}
				else
				{
					gameManagerInstance.enemyCurrentHP = gameManagerInstance.enemyCurrentHP - (defaultDamage - Mathf.FloorToInt(defaultDamage * affinityDamage));
				}
			}
			else
			//if enemy is weaker
			if (((affinityInNumber == 1 && enemyAffinityInNumber == 3) || affinityInNumber > enemyAffinityInNumber) && (affinityInNumber != 0 && enemyAffinityInNumber != 0))
			{
				if (gameManagerInstance.isEnemyDefend)
				{
					gameManagerInstance.enemyCurrentHP -= defaultDamage;
				}
				else
				{
					gameManagerInstance.enemyCurrentHP = gameManagerInstance.enemyCurrentHP - (defaultDamage + Mathf.FloorToInt(defaultDamage * affinityDamage));
				}
			}
			else
			//if enemy is equal
			if (affinityInNumber == enemyAffinityInNumber || affinityInNumber == 0 || enemyAffinityInNumber == 0)
			{
				if (gameManagerInstance.isEnemyDefend)
				{
					gameManagerInstance.enemyCurrentHP = gameManagerInstance.enemyCurrentHP;
				}
				else
				{
					gameManagerInstance.enemyCurrentHP = gameManagerInstance.enemyCurrentHP - defaultDamage;
				}
			}
		}
	}
}

