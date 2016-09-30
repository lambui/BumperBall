using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class ballMovement : MonoBehaviour 
{
	Rigidbody thisRigidbody;
	dash dashAbility;

	bool stun;
	float stunTimer;
	public float stunDuration;
	public float speed;
	AudioSource[] audioList;

	void Awake()
	{
		thisRigidbody = gameObject.GetComponent<Rigidbody>();
		dashAbility = GetComponent<dash>();
		stunTimer = 0f;

		//add sound effects
		audioList = GetComponents<AudioSource>();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			execute();
		}

		//update collider in case size change
		gameObject.GetComponent<SphereCollider>().radius = transform.GetChild(0).localScale.x/2;

		if(stun)
			stunTimer -= Time.deltaTime;
		if(stunTimer <= 0f)
			stun = false;

		StartCoroutine(checkDestroy());
	}

	void FixedUpdate()
	{
		if(stun == false)
			thisRigidbody.AddForce(new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0f, CrossPlatformInputManager.GetAxis("Vertical")) * speed * thisRigidbody.mass);
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "bottomMost")
		{
			Destroy(transform.parent.gameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "AI")
		{
			thisRigidbody.velocity = col.relativeVelocity * col.gameObject.GetComponent<Rigidbody>().mass;
			stun = true;
			stunTimer = stunDuration;
			audioList[0].Play();
		}
		if(col.gameObject.tag == "object")
		{
			audioList[4].Play();
		}
	}

	public float getCooldownRatio()
	{
		return dashAbility.getCooldownRatio();
	}

	public bool isCooldown()
	{
		return dashAbility.isCooldown();
	}

	public void execute()
	{
		if(stun == false)
			dashAbility.execute(new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0f, CrossPlatformInputManager.GetAxis("Vertical")));
	}

	IEnumerator checkDestroy()
	{
		float y = transform.position.y;
		if(y < -10)
		{
			//activate flash particle
			transform.parent.GetChild(2).gameObject.GetComponent<ParticleSystem>().Emit(1);

			//turn off renderer
			transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

			//play dead sound
			audioList[1].Play();

			//wait 0.05s for particle animation to finish
			yield return new WaitForSeconds(0.05f);

			//destroy object
			Destroy(transform.parent.gameObject);
		}
	}
}
