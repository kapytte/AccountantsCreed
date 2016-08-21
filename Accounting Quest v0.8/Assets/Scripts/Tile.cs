using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Analytics;

[RequireComponent (typeof(Material), typeof(MeshCollider), typeof(Rigidbody))]
public class Tile : MonoBehaviour 
{

	public string type;
	public int resource; 
	public float dist;
	public int travelTime, ambushBase, treasureBase, hazardBase;
	public int ambushMod, treasureMod, hazardMod, distFromTown;
	public int ambushChance, treasureChance, hazardChance, none;

	public Collider[] surroundingTiles;
	public List<GameObject> hexes = new List<GameObject>();

	public GameObject caravan, clock, canvas, prev, shopSystem, house, parent, goHere, goHerePrefab;

	public List<MeshRenderer> edges = new List<MeshRenderer>();

	public Material plains, lake, quarry, forest, roadS, roadC, roadT, town;

	public bool available, canMouseOver, currentTile;




	public int c;

	//finds objects of interest on level load
	void Awake()
	{
		canvas = GameObject.Find ("TownScreen");
		caravan = GameObject.Find ("Caravan");
		clock = GameObject.Find ("Time");
		prev = GameObject.Find("PreviewText");
		shopSystem = GameObject.Find("ShopSystem");



	}

	//establishes position and type of hex at start
	void Start () 
	{
		ambushBase = 15;
		treasureBase = 10;
		hazardBase = 5;

		DistFromTown();

		available = true;

		if(gameObject.transform.parent.name != null)
		{
			ColorChange();
		}

		if (canvas.activeInHierarchy != false) 
		{
			canvas.SetActive (false);
		}

		surroundingTiles = Physics.OverlapSphere(transform.position, 1.4f);

		for(int i = 0; i < surroundingTiles.Length; i++)
		{
			if(surroundingTiles[i].tag == "Hex" && !hexes.Contains(surroundingTiles[i].gameObject) && surroundingTiles[i].gameObject != gameObject)
			{
				hexes.Add(surroundingTiles[i].gameObject);
			}
		}

		Shuffle();

		AssignRisk ();

	

	}
		



	//gets distance from nearest town and adjusts base event chance
	void DistFromTown()
	{
		distFromTown = 100;
		for(int i = 0; i < shopSystem.GetComponent<ShopSystem>().towns.Count; i++)
		{
			int b = Mathf.CeilToInt (Vector3.Distance(transform.position, shopSystem.GetComponent<ShopSystem>().towns[i].transform.position));

			if (b < distFromTown)
			{
				distFromTown = b;
			} 
		}


	}

	//decides what hex this gameobject will be, either manually with towns and roads, or randomly with other hexes
	void ColorChange()
	{
		if (parent.name == "Town")
		{
			GetComponent<Renderer>().material = town;
			gameObject.name = "town";
			gameObject.AddComponent<Town>();
			ambushBase *= 0;
			treasureBase *= 0;
			hazardBase *= 0;
			Instantiate(house, transform.position, Quaternion.identity);


		}

		else if (parent.name == "Road")
		{
			GetComponent<Renderer>().material= roadS;
			gameObject.name = "road";
			travelTime = 1;
			ambushBase *= 1;
			treasureBase *= 0;
			hazardBase *= 1;
		}
			
		else
		{
			c =	Random.Range(0,100);

			if (c >=0 &&  c < 15)
			{
				GetComponent<Renderer>().material = lake;
				gameObject.name = "water";
				type = "fish";
				resource = Random.Range (100, 200);
				travelTime = 3;
				ambushBase *= 0;
				treasureBase *= 2;
				hazardBase *= 3;
			}

			if (c >=15 &&  c < 25)
			{
				GetComponent<Renderer>().material = quarry;
				gameObject.name = "quarry";
				type = "iron";
				resource = Random.Range (50,100);
				travelTime = 2;
				ambushBase *= 2;
				treasureBase /= 2;
				hazardBase *= 2;
			}

			if (c >=25 &&  c < 55)
			{
				GetComponent<Renderer>().material = forest;
				gameObject.name  = "forest";
				type = "lumber";
				resource = Random.Range (150,300);
				travelTime = 3;
				ambushBase *= 2;
				treasureBase *= 1;
				hazardBase *= 2;

				int i = Random.Range(2, 6);

			
			}

			if (c >=55 && c < 100)
			{
				GetComponent<Renderer>().material = plains;
				gameObject.name = "plains";
				type = "wheat";
				resource = Random.Range (200, 400);
				travelTime = 1;
				ambushBase *= 2;
				treasureBase *= 2;
				hazardBase *= 2;
			}
			ambushBase += distFromTown;
			treasureBase += distFromTown;
			hazardBase += distFromTown;
		}


	}


	//clears preview box
	void OnMouseExit()
	{
		if (canMouseOver == true)
		{
			prev.GetComponent<Text> ().text = null;
		}
		if (goHerePrefab != null)
		{
			Destroy(goHerePrefab);
		}
	}

	//controls preview when mousing over hexes, and allows transport by clicking on 
	void OnMouseOver()
	{
		if (canMouseOver && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !caravan.GetComponent<Caravan>().inEvent) 
		{
			
			if (gameObject.name == "town")
			{
				prev.GetComponent<Text> ().text = gameObject.name;

				if (clock.GetComponent<WorldTime>().day)
				{
					prev.GetComponent<Text> ().text = gameObject.name + "\n" + "\n" + "[Open For Business]";
					if (goHerePrefab == null)
					{
						goHerePrefab = Instantiate(goHere, new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z), Quaternion.identity) as GameObject;
					}
				}
				else
				{
					prev.GetComponent<Text> ().text = gameObject.name + "\n" + "\n" + "[Closed At Night]";

				}
			}
			else
			{
				int d = caravan.GetComponent<Caravan>().danger;
				prev.GetComponent<Text> ().text = gameObject.name + "\n" + "\n" + "Ambush: " + ambushMod + "\n" + "Item: " + treasureMod + "\n" + "Hazard: " + hazardMod;
				if (goHerePrefab == null)
				{
					goHerePrefab = Instantiate(goHere,new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z), Quaternion.identity) as GameObject;
				}
			}

			if (Input.GetMouseButton (0))
			{
				if (gameObject.name == "town" && !clock.GetComponent<WorldTime>().day)
				{
					
				}
				else
				{
					caravan.GetComponent<NavMeshAgent> ().SetDestination (transform.position);
					clock.GetComponent<WorldTime>().AddTime();
					prev.GetComponent<Text> ().text = null;

					if (clock.GetComponent<WorldTime>().camp.activeInHierarchy)
					{
						clock.GetComponent<WorldTime> ().camp.GetComponent<Button> ().interactable = false;
					}

					if (goHerePrefab != null)
					{
					Destroy(goHerePrefab);
					}
				}
			}
		}
	}

	//shuffles event chance when caravan enters
	public void OnTriggerEnter(Collider c)
	{
		if (c.name == "Caravan") 
		{
			AssignRisk ();

			ShuffleAll();

			prev.GetComponent<Text> ().text = null;
		}
	}
	public void ShuffleAll()
	{
		for(int i = 0; i < hexes.Count; i++)
		{
			hexes[i].GetComponent<Tile>().Shuffle();
		}
	}

	public void Shuffle()
	{
		ambushMod = Random.Range(ambushBase  / 2, ambushBase) * caravan.GetComponent<Caravan>().danger;
		treasureMod = Random.Range(treasureBase  / 2, treasureBase);
		hazardMod = Random.Range(hazardBase  / 2, hazardBase) * caravan.GetComponent<Caravan>().danger;

	}
		
	//ensures chances do not ovelap
	void AssignRisk() 
	{
		ambushChance = Random.Range (ambushMod / 2, ambushMod);
		treasureChance = Random.Range (treasureMod / 2, treasureMod);
		hazardChance = Random.Range (hazardMod / 2, hazardMod);
		none = 100 - ambushChance - treasureChance - hazardChance;
	}
		
	void OnTriggerStay(Collider c)
	{
		if (c.gameObject.name == "Villager")
			available = false;

		else 
			available = true;
	}
}
