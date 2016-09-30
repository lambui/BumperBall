using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cameraControl : MonoBehaviour 
{
	GameObject[] players;
	public float min_y;
	public float max_y;
	public float xMaxDistance;
	public float zMaxDistance;

	public float min_z_back;
	public float max_z_back;

	void FixedUpdate()
	{
		GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] AI = GameObject.FindGameObjectsWithTag("AI");
		players = new GameObject[playerArray.Length + AI.Length];
		playerArray.CopyTo(players, 0);
		AI.CopyTo(players, playerArray.Length);

		if(players.Length == 0)
			return;

		float x, z; x = 0; z = 0;
		float max_x, min_x; max_x = min_x = players[0].transform.position.x;
		float max_z, min_z; max_z = min_z = players[0].transform.position.z;
		foreach( GameObject element in players)
		{
			float xPos = element.transform.position.x;
			float zPos = element.transform.position.z;

			x += xPos;
			z += zPos;

			if(max_x < xPos)
				max_x = xPos;
			if(min_x > xPos)
				min_x = xPos;

			if(max_z < zPos)
				max_z = zPos;
			if(min_z > zPos)
				min_z = zPos;
		}
		x /= players.Length;
		z /= players.Length;

		//determine the height of camera to capture every players
		float xDistance = max_x - min_x;
		float zDistance = max_z - min_z;
		float y; y = min_y;
		if(zDistance == 0)
		{
			float b = min_y;
			float a = (max_y - b)/xMaxDistance;
			y = a*xDistance + b;
		}
		else
		{
			if(xDistance/zDistance < Camera.main.aspect)
			{
				float b = min_y;
				float a = (max_y - b)/zMaxDistance;
				y = a*zDistance + b;
			}
			else
			{
				float b = min_y;
				float a = (max_y - b)/xMaxDistance;
				y = a*xDistance + b;
			}
		}

		//determine z_back
		float zDif = min_z_back - max_z_back;
		float yDif = min_y - max_y;
		float A = zDif/yDif;
		float B = min_z_back - A;
		float z_back = A*y + B;
		z += z_back;

		//determine the appropriate camera angle
		float xRotAngle = Mathf.Atan(y/Mathf.Abs(z_back));

		//move camera
		transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), 0.1f);
		Quaternion target = Quaternion.Euler(xRotAngle*Mathf.Rad2Deg, 0, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.1f);
	}
}
