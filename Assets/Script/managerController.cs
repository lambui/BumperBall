using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class managerController : MonoBehaviour 
{
	public bool playing = false;
	bool startOverlay;
	bool overlayDone;
	public Image overlay = null;
	bool isWon;

	public float sceneChangeDelayDuration = 0f;
	float delayTimer;

	GameObject[] players;
	GameObject[] AI;

	void Start()
	{
		overlayDone = false;
		startOverlay = false;
		isWon = false;
		delayTimer = sceneChangeDelayDuration;
	}

	void Update()
	{
		if(playing)
		{
			players = GameObject.FindGameObjectsWithTag("Player");
			AI = GameObject.FindGameObjectsWithTag("AI");

			if(AI.Length == 0)
			{
				stopPlay(true);
				return;
			}

			if(players.Length == 0)
			{
				stopPlay(false);
				return;
			}
		}

		if(overlay != null)
		{
			if(startOverlay)
			{
				overlay.fillAmount += 2f*Time.deltaTime;
			}
			if(overlay.fillAmount >= 1f)
			{
				startOverlay = false;
				delayTimer -= Time.deltaTime;
				if(delayTimer <= 0f)
					overlayDone = true;
			}
			if(overlayDone)
			{
				changeEndScene();
			}
		}
	}

	public void stopPlay(bool playerWin)
	{
		playing = false;
		startOverlay = true;
		overlay.gameObject.SetActive(true);
		isWon = playerWin;
	}

	public void changeEndScene()
	{
		if(isWon)
		{
			//popup congratulation window
			Application.LoadLevel(2);
		}
		else
		{
			//popup sorry window
			Application.LoadLevel(3);
		}
	}

	public void pauseGame()
	{
		Time.timeScale = 0;
	}

	public void unpauseGame()
	{
		Time.timeScale = 1;
	}

	public void playDemo()
	{
		Application.LoadLevel(1); //scene 1
	}

	public void startScreen()
	{
		Application.LoadLevel(0); //scene Intro
	}
}
