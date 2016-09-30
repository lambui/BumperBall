using System;
using UnityEngine;
using System.Collections;

public class effectController : MonoBehaviour 
{
	public string name = "";
	float duration;
	public float timer;
	bool isActive;
	bool isInit;
	GameObject affectedTarget;
	Action<GameObject> activate;
	Action<GameObject> deactivate;

	void Awake()
	{
		isInit = false;
		isActive = false;
	}

	void Update()
	{
		if(!isInit)
			return;

		if(isActive)
		{
			timer -= Time.deltaTime;
		}

		if(timer <= 0f && isActive)
		{
			deactivate(affectedTarget);
			Destroy(gameObject);
		}
	}

	public void run()
	{
		if(!isInit)
			return;
		if(affectedTarget == null)
			return;
		activate(affectedTarget);
		isActive = true;
	}

	public void init(string nameInput, float durationInput, GameObject targetInput, Action<GameObject> activateFunction, Action<GameObject> deactivateFunction)
	{
		setName(nameInput);
		setDuration(durationInput);
		setTarget(targetInput);
		setActivate(activateFunction);
		setDeactivate(deactivateFunction);
		isInit = true;
	}


	public void addTime(float input)
	{
		if(!isInit)
			return;
		timer += input;

		//play corressponding buff sound
		switch(name)
		{
			case "doubleSize": affectedTarget.GetComponents<AudioSource>()[3].Play(); break;
		}
	}

	public void setDuration(float input)
	{
		duration = input;
		timer = duration;
	}

	public void setTarget(GameObject input)
	{
		affectedTarget = input;
	}

	public void setName(string input)
	{
		name = input;
	}

	public void setActivate(Action<GameObject> input)
	{
		activate = input;
	}

	public void setDeactivate(Action<GameObject> input)
	{
		deactivate = input;
	}
}
