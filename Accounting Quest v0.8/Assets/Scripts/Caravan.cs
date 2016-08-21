using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class Caravan : MonoBehaviour
{

	public GameObject shops, townScreen, currentTile, time, choiceSystem, metrics;

	public LayerMask hexLayer;

	public enum tileEventList {none, ambush, treasure, hazard};
	public tileEventList tileEvent;

	public Collider[] surroundingTiles;
	public List<MeshCollider> hexes = new List<MeshCollider>();

	//inputs for Hazard events
	public GameObject ambush, hazard;
	public Text randDEF, randATK, outcomeA, resultA, outcomeH, resultH;
	public int def, newDEF, bandit, scene;
	public bool inEvent;

	//input for treasure
	public GameObject treasure;
	public Text outcomeT; 

	public float overlapR;

	public Text defT;

	public int i, a, b, d, help, danger;

	public Color yellow, lightBlue, darkGreen, ironGrey;

	//MAIN PLAYER FUNCTIONS FOR INTERACTING WITH OVEROWRLD EVENTS 
	// Use this for initialization
	void Start () 
	{
		def = 10;
		defT.text = def.ToString();

		danger = 1;

		metrics = GameObject.Find("Metrics");

		scene = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		WhereAmI ();


		if (GetComponent<NavMeshAgent> ().velocity.magnitude == 0 && scene == 1)
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
			if (c.gameObject != currentTile && time.GetComponent<WorldTime>().start == false)
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

		if (scene == 1)
		{
			if (c.name == "town")
			{
				shops.GetComponent<ShopSystem>().town = c.gameObject;
				townScreen.SetActive(true);
				shops.GetComponent<ShopSystem>().MercsLeave();
			}

			else if (c.gameObject == currentTile) 
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
				else if (i > a && i <= b && shops.GetComponent<ShopSystem> ().cargo.Count < 10) 
				{
					tileEvent = tileEventList.treasure;
					Treasure ();
				}
				else if (i > b && i <= d && shops.GetComponent<ShopSystem> ().cargo.Count > 0) 
				{
					tileEvent = tileEventList.hazard;
					Hazard();
				}
				else
				{
					tileEvent = tileEventList.none;
				}

				if (time.GetComponent<WorldTime>().camp.activeInHierarchy)
				{
					time.GetComponent<WorldTime>().camp.GetComponent<Button>().interactable = true;
				}
			}
		}

		if (scene == 2)
		{
			if (c.name == "town")
			{
				//shops.GetComponent<ShopSystem>().town = c.gameObject;
				townScreen.SetActive(true);
				def = 10;

			}

			//c.GetComponent<Tile>().canMouseOver = false;
		}
			
	}
		

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, overlapR);

	}

	public void Ambush()
	{
		ambush.SetActive(true);
		InEvent ();

		int j;

		j = Random.Range (1, 21);

		newDEF = def + j;
		randDEF.text = newDEF.ToString();

//		if (shops.GetComponent<ShopSystem> ().cargo.Count > 0)
			bandit = shops.GetComponent<ShopSystem> ().cargo.Count * 2 + currentTile.GetComponent<Tile>().distFromTown;
//		else
//			bandit = currentTile.GetComponent<Tile>().distFromTown;

		j = Random.Range (1, 21);

		bandit += j;
		randATK.text = bandit.ToString();

		int g = Mathf.CeilToInt(choiceSystem.GetComponent<MultipleChoice>().goldN * 0.1f);

		if (shops.GetComponent<ShopSystem> ().cargo.Count > 0 && newDEF < bandit) 
		{
			
			outcomeA.text = "Defeat";
			outcomeA.color = Color.red;


			j = Random.Range (0, shops.GetComponent<ShopSystem> ().cargo.Count - 1);


			resultA.text = ("Bandits stole some " +  shops.GetComponent<ShopSystem> ().cargo[j].name + " and " + g + "G");
			shops.GetComponent<ShopSystem> ().cargo.RemoveAt(j);
			choiceSystem.GetComponent<MultipleChoice>().goldN -= g;
			shops.GetComponent<ShopSystem> ().CargoTextAdd ();

			metrics.GetComponent<Metrics>().goldNeg -= g;
			metrics.GetComponent<Metrics>().banditsNeg += 1;
		} 

		else if (shops.GetComponent<ShopSystem> ().cargo.Count == 0 && newDEF < bandit)
		{
			outcomeA.text = "Defeat";
			outcomeA.color = Color.red;

			resultA.text = ("Bandits stole " + g + "G");
			choiceSystem.GetComponent<MultipleChoice>().goldN -= g;

			metrics.GetComponent<Metrics>().goldNeg -= g;
			metrics.GetComponent<Metrics>().banditsNeg += 1;

		}

		else 
		{
			
			outcomeA.text = "Victory";
			outcomeA.color = Color.green;

			resultA.text = ("Bandits were driven away");
			metrics.GetComponent<Metrics>().banditsPos += 1;
		}
	}

	public void InEvent()
	{
		inEvent = !inEvent;
	}

	public void Treasure()
	{
		treasure.SetActive(true);
		InEvent ();

		metrics.GetComponent<Metrics>().treasure += 1;

		if (currentTile.name == "water") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().fish);
			outcomeT.text = ("Found some Fish");
			outcomeT.color = lightBlue;

		}
		if (currentTile.name == "forest") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().lumber);
			outcomeT.text = ("Found some Lumber");
			outcomeT.color = darkGreen;
		}
		if (currentTile.name == "quarry") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().iron);
			outcomeT.text = ("Found some Iron");
			outcomeT.color = ironGrey;
		}
		if (currentTile.name == "plains") 
		{
			shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem>().wheat);
			outcomeT.text = ("Found some Wheat");
			outcomeT.color = yellow;
		}

		shops.GetComponent<ShopSystem> ().CargoTextAdd ();

	}
		
	public void Hazard() 
	{
		hazard.SetActive(true);
		InEvent ();

		metrics.GetComponent<Metrics>().hazards += 1;

		int j;
		if (scene == 1)
		{
			j = Random.Range (0, shops.GetComponent<ShopSystem> ().cargo.Count-1);
		}
		else 
		{
			j = 1;
		}

		{
			if (currentTile.name == "water") 
			{
				outcomeH.text = "You tried to caulk the wagon and ford the river";

			}
			else if (currentTile.name == "forest") 
			{
				outcomeH.text = "These woods are a little too dark for comfort";
			}
			else if (currentTile.name == "quarry") 
			{
				outcomeH.text = "Venturing too close to a mine shaft has its consequences";
			}
			else if (currentTile.name == "plains") 
			{
				outcomeH.text = "Turns out it's more of a marsh";
			}
			else if (currentTile.name == "road") 
			{
				outcomeH.text = "Potholes... everywhere";
			}

			string lost = shops.GetComponent<ShopSystem> ().cargo[j].name;

			if (lost == "Wheat")
			{
				resultH.color = yellow;
			}

			else if (lost == "Fish")
			{
				resultH.color = lightBlue;
			}
			else if (lost == "Lumber")
			{
				resultH.color = darkGreen;
			}
			else if (lost == "Iron")
			{
				resultH.color = ironGrey;
			}

			resultH.text = "You lost some " + lost;
			shops.GetComponent<ShopSystem> ().cargo.RemoveAt (j);
			shops.GetComponent<ShopSystem> ().CargoTextAdd ();

		}

	}


}
