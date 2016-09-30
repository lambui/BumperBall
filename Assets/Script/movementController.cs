using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour 
{
	Camera mainCam;
	Vector3 controlLocation;
	Vector3 directionToMouse;
	public GameObject movingCircle;
	float outsideRadius;
	float insideRadius;

	GameObject controlledPlayer;
	Vector3 distanceFromCenter;

	public float updateInterval;
	float updateTimer;
	bool move;
	bool mouseDrag;

	void Start() 
	{
		mainCam = Camera.main;
		outsideRadius = gameObject.GetComponent<RectTransform>().rect.width/2;
		insideRadius = gameObject.transform.GetChild(0).GetComponent<RectTransform>().rect.width/2;
		controlLocation = mainCam.WorldToScreenPoint(transform.position);
		controlLocation = new Vector3(controlLocation.x, controlLocation.y, 0f);

		directionToMouse = Vector3.zero;

		controlledPlayer = GameObject.FindWithTag("Player");
		
		move = false;
		mouseDrag = false;

		updateTimer = 0;
	}

	// Update is called once per frame
	void Update() 
	{
		if(Input.GetMouseButtonDown(0))
		{
			directionToMouse = Input.mousePosition - controlLocation;
			if(directionToMouse.magnitude <= insideRadius)
				mouseDrag = true;
			else
				directionToMouse = Vector3.zero;
		}
		if(Input.GetMouseButtonUp(0))
		{
			directionToMouse = Vector3.zero;
			mouseDrag = false;
		}

		if(mouseDrag)
		{
			directionToMouse = Input.mousePosition - controlLocation;
		}

		if(Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				directionToMouse = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f) - controlLocation;
				if(directionToMouse.magnitude <= insideRadius)
					move = true;
				else
					directionToMouse = Vector3.zero;
			}
			else 
			{
				if(Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					if(move == true)
						directionToMouse = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f) - controlLocation;
					else
						directionToMouse = Vector3.zero;
				}					
				if(Input.GetTouch(0).phase == TouchPhase.Stationary)
				{
					if(move == true)
						directionToMouse = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f) - controlLocation;
				}
			}
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				directionToMouse = Vector3.zero;
				move = false;
			}
		}

		if(directionToMouse.magnitude > outsideRadius)
		{
			directionToMouse.Normalize();
			movingCircle.GetComponent<RectTransform>().localPosition = directionToMouse*outsideRadius;
		}
		else
		{
			//if(directionToMouse.magnitude > insideRadius)
				movingCircle.GetComponent<RectTransform>().localPosition = directionToMouse;
			//else
			//	movingCircle.GetComponent<RectTransform>().localPosition = Vector3.zero;
		}
	}
}
