using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour 
{
	public GameObject normalFullScreenTransitionGameObject;

	public GameObject playerPageGameObject;

	// Use this for initialization
	void Start () 
	{
	
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

		normalFullScreenTransitionGameObject.SetActive(false);
	}

	public void ShowPlayerPage ()
	{
		playerPageGameObject.SetActive(true);
	}
}
