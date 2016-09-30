using UnityEngine;
using System.Collections;

public class ballMovement2 : MonoBehaviour 
{
	void Start()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			gameObject.GetComponent<Rigidbody>().velocity = col.relativeVelocity * col.gameObject.GetComponent<Rigidbody>().mass;
		}
	}
}
