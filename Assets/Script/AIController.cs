using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour 
{
	public GameObject target;
	Rigidbody thisRigidbody;
	bool nearEdge;
	bool targetDanger;
	bool targetNear;
	public float nearRadius;
	public float speed;
	GameObject[] effectTrackers;


	bool dashUp;
	bool nearDash;
	public float dashDistance;
	dash dashAbility;

	void Start()
	{
		thisRigidbody = GetComponent<Rigidbody>();
		dashAbility = GetComponent<dash>();
	}

	void FixedUpdate()
	{
		//return if object is destroyed
		if(thisRigidbody == null)
			return;

		if(target == null)
			return;

		dashUp = !dashAbility.isCooldown();

		//see if player is currently under good effect.
		//if is then mark target as danger and maybe consider running away from.
		targetDanger = false;
		effectTrackers = GameObject.FindGameObjectsWithTag("EffectTracker");
		foreach(GameObject element in effectTrackers)
		{
			if(element.GetComponent<effectController>().name == "doubleSize")
			{
				targetDanger = true;
				break;
			}
		}

		float distance = Vector3.Distance(transform.position, target.transform.position);
		if(distance < nearRadius)
			targetNear = true;
		else
			targetNear = false;

		if(distance < dashDistance)
			nearDash = true;
		else
			nearDash = false;

		decisionMaking();
	}

	void decisionMaking()
	{
		if(GetComponent<AI_ballMovement>().isStunned())
			return;

		Vector3 dir = Vector3.zero;
		if(nearEdge) //if is too close to edge
		{
			dir = Vector3.zero - transform.position; //run toward middle if near edge
			if(dashUp)
			{
				dashAbility.execute(dir);
				return;
			}
		}
		else
		{
			if(targetDanger)
			{
				if(targetNear)
				{
					dir = transform.position - target.transform.position; //run away if target is danger and is too close to target
					if(dashUp && nearDash) //if dash up and is absolutely too close
					{
						dashAbility.execute(dir);
						return;
					}
				}
				else
				{
					if(Random.Range(0f, 10f) < 1f) //10% chance if will flee
						dir = transform.position - target.transform.position;
					else
						dir = Vector3.zero;
				}
			}
			else
			{
				if(targetNear)	//if target is too close
				{
					if(dashUp) //if dash up
					{
						if(nearDash)
							dir = transform.position - target.transform.position; //move away first
						else
						{
							dir = target.transform.position - transform.position;
							dashAbility.execute(dir);
							return;
						}
					}
					else //if not
						dir = target.transform.position - transform.position; //run at target
				}
				else
				{
					if(dashUp) //use dash
					{
						if(Random.Range(0f, 10f) < 1f) //10% chance if will use dash
						{
							dir = target.transform.position - transform.position;
							dashAbility.execute(dir);
							return;
						}
						else
							dir = target.transform.position - transform.position; //run at target
					}
					else
						dir = target.transform.position - transform.position; //run at target
				}
			}
		}
		thisRigidbody.AddForce(dir.normalized*speed*thisRigidbody.mass);
	}

	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag == "platformEdge")
		{
			nearEdge = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "platformEdge")
		{
			nearEdge = false;
		}
	}

	
}
