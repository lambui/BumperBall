using UnityEngine;
using System.Collections;

public class AI_ballMovement : MonoBehaviour 
{
	bool stun;
	float stunTimer;
	public float stunDuration;

	AudioSource[] audioList;

	void Awake()
	{
		audioList = GetComponents<AudioSource>();
	}

	void Update()
	{
		if(stun)
			stunTimer -= Time.deltaTime;
		if(stunTimer <= 0f)
			stun = false;

		StartCoroutine(checkDestroy());
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			gameObject.GetComponent<Rigidbody>().velocity = col.relativeVelocity * col.gameObject.GetComponent<Rigidbody>().mass;
			audioList[0].Play();
			stun = true;
			stunTimer = stunDuration;
		}
		if(col.gameObject.tag == "object")
		{
			audioList[4].Play();
		}
	}

	public bool isStunned()
	{
		return stun;
	}

	IEnumerator checkDestroy()
	{
		float y = transform.position.y;
		if(y < -10)
		{
			//activate flash particle
			transform.parent.GetChild(2).gameObject.GetComponent<ParticleSystem>().Emit(1);

			//turn off renderer
			GetComponent<Renderer>().enabled = false;

			audioList[1].Play();

			//wait 0.05s for particle animation to finish
			yield return new WaitForSeconds(0.05f);

			//destroy object
			Destroy(transform.parent.gameObject);
		}
	}
}
