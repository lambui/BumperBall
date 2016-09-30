using UnityEngine;
using System.Collections;

public class introBallController : MonoBehaviour 
{
	public GameObject animationObject;
	Animator animator;

	public float duration;
	float timer;
	int index;

	public string[] excludedAnimationClips;

	void Start()
	{
		animator = animationObject.GetComponent<Animator>();
		timer = duration;
		index = 0;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if(timer <= 0f)
		{
			timer = duration;
			index = Random.Range(0, animator.runtimeAnimatorController.animationClips.Length);

			string name = animator.runtimeAnimatorController.animationClips[index].name;
			
			bool skip = false;
			foreach(string s in excludedAnimationClips)
			{
				if(s == name)
				{
					skip = true;
					break;
				}
			}
			if(skip)
			{
				animator.Play("player_idle");
				return;
			}

			animator.Play(name);
		}
	}
}
