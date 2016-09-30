using UnityEngine;
using System.Collections;

public class wrapperController : MonoBehaviour 
{
	void FixedUpdate()
	{
		float scale = transform.parent.GetChild(0).GetChild(0).localScale.x;
		transform.position = new Vector3(transform.parent.GetChild(0).position.x, transform.parent.GetChild(0).position.y*scale + 0.8f, transform.parent.GetChild(0).position.z + 0.3f);
	}
}
