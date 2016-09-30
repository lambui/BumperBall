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

	public GameObject canvasOverlay = null;
	public GameObject pauseOverlay = null;

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
				delayTimer -= Time.deltaTime;
				if(delayTimer <= 0f)
					overlay.fillAmount += 2f*Time.deltaTime;
			}
			if(overlay.fillAmount >= 1f)
			{
				startOverlay = false;
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
		if(canvasOverlay == null || pauseOverlay == null)
			return;
		Time.timeScale = 0;
		canvasOverlay.SetActive(false); //hide all control buttons
		pauseOverlay.SetActive(true); //turn all the pause overlay
	}

	public void unpauseGame()
	{
		if(canvasOverlay == null || pauseOverlay == null)
			return;
		Time.timeScale = 1;
		canvasOverlay.SetActive(true);
		pauseOverlay.SetActive(false);
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
