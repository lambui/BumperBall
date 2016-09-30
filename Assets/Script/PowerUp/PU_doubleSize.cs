using UnityEngine;
using System.Collections;

public class PU_doubleSize : MonoBehaviour 
{
	public float duration;
	public GameObject effectTracker;
	public string name;

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			GameObject ef = searchEffectTrackerByName(name);
			if(ef == null)
				instantiateEffectTracker(col.gameObject);
			else
				ef.GetComponent<effectController>().addTime(duration);
			Destroy(transform.parent.gameObject);
		}
	}

	//this function should be a global function in some namespace but right now only this effect uses it so its a class function for now
	GameObject searchEffectTrackerByName(string nameInput) //return Effect Tracker gameObject with nameInput else return null
	{
		GameObject[] list = GameObject.FindGameObjectsWithTag("EffectTracker");
		foreach(GameObject element in list)
		{
			if(element.GetComponent<effectController>().name == nameInput)
				return element;
		}
		return null;
	}

	void instantiateEffectTracker(GameObject target)
	{
		GameObject go = Instantiate(effectTracker) as GameObject;
		effectController ef = go.GetComponent<effectController>();
		ef.init(name, duration, target, activate, deactivate);
		ef.run();
	}

	void activate(GameObject target)
	{
		if(target == null)
			return;
		target.GetComponent<Rigidbody>().mass = 2;
		target.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("player_PU_doubleSize");
		target.GetComponents<AudioSource>()[3].Play();
	}

	void deactivate(GameObject target)
	{
		if(target == null)
			return;
		target.GetComponent<Rigidbody>().mass = 1;
		target.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("player_PU_doubleSize_shrink");
	}
}
