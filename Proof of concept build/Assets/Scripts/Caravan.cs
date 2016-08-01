using UnityEngine;
using System.Collections;
using System.Collections.Generic;





public class Caravan : MonoBehaviour
{

	public GameObject shops, townScreen, currentTile;

	public LayerMask hexLayer;

	public enum tileEventList {none, ambush, treasure, hazard};
	public tileEventList tileEvent;

	public Collider[] surroundingTiles;

	public float overlapR;

	public int i, a, b, d;

	// Use this for initialization
	void Start () 
	{
		CheckTiles ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		WhereAmI ();

		if (GetComponent<NavMeshAgent> ().velocity.magnitude == 0)
		{
			CheckTiles ();
		}

		else
		{
//			Cursor.lockState = CursorLockMode.Locked;
//			Cursor.visible = false;
			CannotSelect ();
		}

	}

	void WhereAmI()
	{

		//fire raycast downward and create reference to current Hex
		RaycastHit h;

		Physics.Raycast (transform.position, -transform.up, out h);

		currentTile = h.collider.gameObject;
	}

	void CheckTiles()
	{
		surroundingTiles = Physics.OverlapSphere (transform.position, overlapR, hexLayer);

		foreach (Collider c in surroundingTiles)
		{
			if (c.gameObject != currentTile)
				c.GetComponent<Tile> ().canMouseOver = true;

		}

	}

	public void CannotSelect()
	{
		foreach (Collider c in surroundingTiles)
		{
			c.GetComponent<Tile> ().canMouseOver = false;
		}
	}
		

	void OnTriggerEnter(Collider c)
	{
		
		if (c.gameObject == currentTile) 
		{
			i = Random.Range (1, 101);

			a = currentTile.GetComponent<Tile>().ambushChance;
			b = currentTile.GetComponent<Tile>().treasureChance + a;
			d = currentTile.GetComponent<Tile>().hazardChance + b;

			if (i <= a) 
			{
				tileEvent = tileEventList.ambush;
			}
			else if (i > a && i <= b) 
			{
				tileEvent = tileEventList.treasure;
			}
			else if (i > b && i <= d) 
			{
				tileEvent = tileEventList.hazard;
			}
			else
			{
				tileEvent = tileEventList.none;
			}
		}

		if (c.name == "town")
		{
			shops.GetComponent<ShopSystem>().town = c.gameObject;
			townScreen.SetActive(true);
//			GetComponent<NavMeshAgent>().enabled = false;
//			GetComponent<Caravan>().enabled = false;
		}
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, overlapR);

	}

	//void 


}
