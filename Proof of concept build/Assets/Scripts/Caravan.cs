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


	public int i, a, b, d, def, bandit, help;

	// Use this for initialization
	void Start () 
	{
		CheckTiles ();

		def = 10;
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
			c.GetComponent<Tile>().canMouseOver = false;
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
				Ambush ();
			}
			else if (i > a && i <= b) 
			{
				tileEvent = tileEventList.treasure;
				Treasure ();
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

	void Ambush()
	{
		if (shops.GetComponent<ShopSystem> ().cargo.Count > 0) 
		{
			int j;

			j = Random.Range (1, 21);

			def += j;
			def += help;


			bandit = shops.GetComponent<ShopSystem> ().cargo.Count * 2;

			j = Random.Range (1, 21);

			bandit += j;

			if (def >= bandit) 
			{
				j = Random.Range (0, shops.GetComponent<ShopSystem> ().cargo.Count - 1);

				print ("Bandits stole some " +  shops.GetComponent<ShopSystem> ().cargo[j].name);
				shops.GetComponent<ShopSystem> ().cargo.RemoveAt (j);
				shops.GetComponent<ShopSystem> ().CargoTextAdd ();
			}
			else 
			{
				print ("Safe");
			}
		} 

		else 
		{
			print ("Nope");
		}
	}

	void Treasure()
	{
		if (currentTile.name == "water") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().fish);
			print ("Found some Fish");
		}
		if (currentTile.name == "forest") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().lumber);
			print ("Found some Lumber");
		}
		if (currentTile.name == "plain hill") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().iron);
			print ("Found some Iron");
		}
		if (currentTile.name == "plains") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().wheat);
			print ("Found some Wheat");
		}

		shops.GetComponent<ShopSystem> ().CargoTextAdd ();

	}
		
	void Hazard() 
	{
		int j;

		j = Random.Range (0, shops.GetComponent<ShopSystem> ().cargo.Count);
		shops.GetComponent<ShopSystem> ().cargo.RemoveAt (j);
		shops.GetComponent<ShopSystem> ().CargoTextAdd ();
		print ("Safe");
	}


}
