using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class skillButtonController : MonoBehaviour 
{
	public GameObject player;
	public Button activateButton;
	public Image coolDown;
	public Color cooldownColor;
	Vector3 coolDownLocalScale;

	void Start()
	{
		coolDownLocalScale = coolDown.GetComponent<RectTransform>().localScale;
		activateButton.onClick.AddListener
		(
			delegate
			{
				if(player != null)
				{
					ballMovement playerComponent = player.GetComponent<ballMovement>();
					playerComponent.execute();
				}
			}
		);
	}

	void Update()
	{
		if(player == null)
			return;

		//visualize cooldown
		ballMovement playerComponent = player.GetComponent<ballMovement>();
		coolDown.fillAmount = 1 - playerComponent.getCooldownRatio();

		//disable button if on cooldown 
		activateButton.interactable = !playerComponent.isCooldown();
		if(playerComponent.isCooldown())
		{
			coolDown.gameObject.GetComponent<Animator>().enabled = false;
			coolDown.GetComponent<RectTransform>().localScale = coolDownLocalScale;
			coolDown.color = cooldownColor; //set color
			activateButton.gameObject.GetComponentInChildren<Text>().text = "Cooling Down...";
		}
		else
		{
			coolDown.gameObject.GetComponent<Animator>().enabled = true;
			activateButton.gameObject.GetComponentInChildren<Text>().text = "Activating Dash";
		}
	}

	public void setPlayer(GameObject input)
	{
		player = input;
	}
}
