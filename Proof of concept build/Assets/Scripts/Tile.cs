using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Material), typeof(MeshCollider), typeof(Rigidbody))]
public class Tile : MonoBehaviour 
{

	public string type;
	public int resource; 
	public float dist;
	public int travelTime, ambushBase, treasureBase, hazardBase;
	public int ambushChance, treasureChance, hazardChance, none;



	public GameObject caravan, clock, canvas;

	public bool available, canMouseOver, currentTile;


	public int c;

	// Use this for initialization
	void Start () 
	{
		ambushBase = 15;
		treasureBase = 10;
		hazardBase = 5;


		canvas = GameObject.Find ("TownScreen");
		caravan = GameObject.Find ("Caravan");
		clock = GameObject.Find ("Time");

		available = true;


		if(gameObject.transform.parent.name != null)
		{
			ColorChange();
		}
			
		AssignRisk ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	


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

			if (c >=0 &&  c < 30)
			{
				GetComponent<Renderer>().material.color = Color.blue;
				gameObject.name = "water";
				type = "fish";
				resource = Random.Range (250, 500);
				travelTime = 3;
				ambushBase *= 0;
				treasureBase *= 3;
				hazardBase *= 3;
			}

			if (c >=30 &&  c < 50)
			{
				GetComponent<Renderer>().material.color = Color.grey;
				gameObject.name = "plain hill";
				type = "iron";
				resource = Random.Range (100,200);
				travelTime = 2;
				ambushBase *= 2;
				treasureBase *= 2;
				hazardBase *= 2;
			}

			if (c >=50 &&  c < 90)
			{
				GetComponent<Renderer>().material.color = new Color(0,.43f, 0);
				gameObject.name  = "forest";
				type = "lumber";
				resource = Random.Range (200,400);
				travelTime = 3;
				ambushBase *= 2;
				treasureBase *= 3;
				hazardBase *= 2;
			}

			if (c >=90 && c < 100)
			{
				GetComponent<Renderer>().material.color = Color.green;
				gameObject.name = "plains";
				type = "wheat";
				resource = Random.Range (25, 50);
				travelTime = 1;
				ambushBase *= 2;
				treasureBase *= 1;
				hazardBase *= 2;
			}
		}

	}

	void OnMouseOver()
	{

		if (canMouseOver == true && canvas.activeInHierarchy == false) 
		{

			if (Input.GetMouseButton (0))
			{
				caravan.GetComponent<NavMeshAgent> ().SetDestination (transform.position);
				clock.GetComponent<WorldTime>().AddTime();
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
