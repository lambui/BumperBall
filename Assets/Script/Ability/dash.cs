using UnityEngine;
using System.Collections;

public class dash : MonoBehaviour 
{
	Rigidbody thisRigidbody;
	public float dashCooldown;
	float dashTimer;
	public float dashForce;

	AudioSource[] audioList;

	void Awake()
	{
		thisRigidbody = GetComponent<Rigidbody>();
		audioList = GetComponents<AudioSource>();
	}

	void Update()
	{
		if(dashTimer > 0f)
			dashTimer -= Time.deltaTime;
	}

	public void execute(Vector3 dir)
	{
		if(dashTimer <= 0f)
		{
			thisRigidbody.AddForce(dir.normalized * dashForce * thisRigidbody.mass);
			dashTimer = dashCooldown;

			//activate sphere burst particle
			transform.parent.GetChild(1).gameObject.GetComponent<ParticleSystem>().Emit(100);
			audioList[2].Play();
		}
	}

	public float getCooldownRatio()
	{
		return dashTimer/dashCooldown;
	}

	public bool isCooldown() //false = ability ready to use
	{
		return dashTimer <= 0f ? false : true;
	}
}
