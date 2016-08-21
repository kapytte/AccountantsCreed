using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour 
{
	public GameObject mouseCanvas, worldCanvas, caravan, childCanvas, nextCanvas, nextTile, narrator;
	public enum eventList {none, hazard, loot, ambush, town};
	public eventList eventType;
	public bool mouseOver;

	// TUTOIRAL SPECIFIC SCRITED EVENTS

	public void Start()
	{
		if (eventType == eventList.ambush)
		{
			mouseOver = true;
		}
	}

	public void TurnOnTile()
	{
		GetComponentInChildren<Tile>().canMouseOver = true;

	}

	public void Update()
	{
		if (GetComponentInChildren<Tile>() != null)
		{
			if (GetComponentInChildren<Tile>().canMouseOver == true && mouseOver == true)
			{
				MouseOverChild();
			}
		}
	}

	void MouseOverChild()
	{
		RaycastHit h; 

		Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(mouse, out h);

		if (h.collider == GetComponentInChildren<MeshCollider>())
		{
			GetComponentInChildren<Tile>().canMouseOver = false;

			mouseCanvas.SetActive(true);
			worldCanvas.SetActive(false);

			mouseOver = false;

			narrator.SetActive(true);
		}
	}
		

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == caravan)
		{

			if (eventType == eventList.hazard)
			{
				c.GetComponent<Caravan>().Hazard();
			}

			if (eventType == eventList.loot)
			{
				c.GetComponent<Caravan>().Treasure();
			}

			if (eventType == eventList.ambush)
			{
				c.GetComponent<Caravan>().Ambush();
			}

			if (eventType == eventList.town)
			{
				mouseCanvas.GetComponent<ShopSystem>().outpostNum += 1;
			}

			if (childCanvas != null)
			{
				childCanvas.SetActive(false);
			}

			if (nextCanvas != null)
			{
				nextCanvas.SetActive(true);
			}

			if (narrator != null)
			{
				narrator.SetActive(true);
			}

			if (nextTile != null)
			{
				nextTile.GetComponent<Tutorial>().TurnOnTile();
			}

			GetComponentInChildren<Tile>().canMouseOver = false;
		}
	}




}
