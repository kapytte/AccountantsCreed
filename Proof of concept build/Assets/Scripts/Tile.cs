using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Material), typeof(MeshCollider), typeof(Rigidbody))]
public class Tile : MonoBehaviour 
{

	public string type;
	public int resource; 
	public float dist;
	public int travelTime, ambushBase, treasureBase, hazardBase;
	public int ambushChance, treasureChance, hazardChance, none;



	public GameObject caravan, clock, canvas, prev;



	public bool available, canMouseOver, currentTile;


	public int c;

	void Awake()
	{
		canvas = GameObject.Find ("TownScreen");
		caravan = GameObject.Find ("Caravan");
		clock = GameObject.Find ("Time");
		prev = GameObject.Find("PreviewText");

	}

	// Use this for initialization
	void Start () 
	{
		ambushBase = 15;
		treasureBase = 10;
		hazardBase = 5;

		available = true;

		if(gameObject.transform.parent.name != null)
		{
			ColorChange();
		}
			
		AssignRisk ();

		if (canvas.activeInHierarchy != false) 
		{
			canvas.SetActive (false);
		}
	}


	void ColorChange()
	{
		if (gameObject.transform.parent.name == "Town")
		{
			GetComponent<Renderer>().material.color = Color.red;
			gameObject.name = "town";
			gameObject.AddComponent<Town>();

		}

		else if (gameObject.transform.parent.name == "Road")
		{
			GetComponent<Renderer>().material.color = new Color(.58f, .27f, .07f);
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
				GetComponent<Renderer>().material.color = Color.blue;
				gameObject.name = "water";
				type = "fish";
				resource = Random.Range (100, 200);
				travelTime = 3;
				ambushBase *= 0;
				treasureBase *= 3;
				hazardBase *= 3;
			}

			if (c >=15 &&  c < 25)
			{
				GetComponent<Renderer>().material.color = Color.grey;
				gameObject.name = "plain hill";
				type = "iron";
				resource = Random.Range (50,100);
				travelTime = 2;
				ambushBase *= 2;
				treasureBase *= 2;
				hazardBase *= 2;
			}

			if (c >=25 &&  c < 55)
			{
				GetComponent<Renderer>().material.color = new Color(0,.43f, 0);
				gameObject.name  = "forest";
				type = "lumber";
				resource = Random.Range (150,300);
				travelTime = 3;
				ambushBase *= 2;
				treasureBase *= 3;
				hazardBase *= 2;
			}

			if (c >=55 && c < 100)
			{
				GetComponent<Renderer>().material.color = Color.green;
				gameObject.name = "plains";
				type = "wheat";
				resource = Random.Range (200, 400);
				travelTime = 1;
				ambushBase *= 2;
				treasureBase *= 1;
				hazardBase *= 2;
			}
		}

	}
	void OnMouseEnter()
	{
		if (canMouseOver == true && canvas.activeInHierarchy == false) 
		{
//			string  = gameObject.name + "" + 
			if (gameObject.name == "town")
			{
				prev.GetComponent<Text> ().text = gameObject.name;
			}
			else
			{
				prev.GetComponent<Text> ().text = gameObject.name + "\n" + "\n" + "Ambush: " + ambushBase + "\n" + "Item Find: " + treasureBase + "\n" + "Hazard: " + hazardBase;
			}
		}
	}

	void OnMouseExit()
	{

		prev.GetComponent<Text> ().text = null;
	}


	void OnMouseOver()
	{

		if (canMouseOver == true && canvas.activeInHierarchy == false) 
		{
			if (Input.GetMouseButton (0))
			{
				caravan.GetComponent<NavMeshAgent> ().SetDestination (transform.position);
				clock.GetComponent<WorldTime>().AddTime();
				prev.GetComponent<Text> ().text = null;
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.name == "Caravan") 
		{
			AssignRisk ();
		}
	}


	void AssignRisk() 
	{
		ambushChance = Random.Range (ambushBase / 2, ambushBase);
		treasureChance = Random.Range (treasureBase / 2, treasureBase);
		hazardChance = Random.Range (hazardBase / 2, hazardBase);
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
