using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Material), (typeof(MeshCollider)), (typeof(Rigidbody)))]
public class Tile : MonoBehaviour 
{

	public string type;
	public int resource; 
	public float dist;
	public GameObject caravan, clock;

	public bool available, canMouseOver;


	public int c;

	// Use this for initialization
	void Start () 
	{
		caravan = GameObject.Find ("Caravan");
		clock = GameObject.Find ("Time");

		available = true;



		if(gameObject.transform.parent.name != null)
		{
			ColorChange();
		}

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
			}

			if (c >=30 &&  c < 50)
			{
				GetComponent<Renderer>().material.color = Color.grey;
				gameObject.name = "plain hill";
				type = "iron";
				resource = Random.Range (100,200);
			}

			if (c >=50 &&  c < 90)
			{
				GetComponent<Renderer>().material.color = new Color(0,.43f, 0);
				gameObject.name  = "forest";
				type = "lumber";
				resource = Random.Range (200,400);
			}

			if (c >=90 &&  c < 100)
			{
				GetComponent<Renderer>().material.color = Color.yellow;
				gameObject.name = "rich hill";
				type = "gold";
				resource = Random.Range (25, 50);	
			}
		}

	}

	void OnMouseOver()
	{

		if (canMouseOver == true) 
		{
			//print (gameObject.name);

			if (Input.GetMouseButton (0))
			{
				caravan.GetComponent<NavMeshAgent> ().SetDestination (transform.position);
				clock.GetComponent<WorldTime>().AddTime();
			}

		}

	}


	void OnTriggerStay(Collider c)
	{
		if (c.gameObject.name == "Villager")
			available = false;

		else 
			available = true;
	}
}
