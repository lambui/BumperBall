using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour 
{
	void FixedUpdate()
	{
		transform.position = new Vector3(transform.parent.GetChild(0).position.x, transform.parent.GetChild(0).position.y, transform.parent.GetChild(0).position.z);
	}
}
