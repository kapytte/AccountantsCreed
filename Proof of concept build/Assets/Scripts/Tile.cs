using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Material))]
public class Tile : MonoBehaviour {

	public string type;
	public int resource; 
	public float dist;
	public GameObject caravan;

	public bool available, canMouseOver;



	public int c;

	// Use this for initialization
	void Start () 
	{
		caravan = GameObject.Find ("Caravan");

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
			
		
		else
		{
			c =	Random.Range(0,100);

			if (c >=0 &&  c < 20)
			{
				c = 1;
			}

			if (c >=20 &&  c < 45)
			{
				c = 2;
			}

			if (c >=45 &&  c < 85)
			{
				c = 3;
			}

			if (c >=85 &&  c < 100)
			{
				c = 4;
			}

			switch (c)
			{
			case 1:
				GetComponent<Renderer>().material.color = Color.blue;
				gameObject.name = "water";
				gameObject.tag = "water";
				type = "fish";
				resource = Random.Range (50,100);
				break;

			case 2:
				GetComponent<Renderer>().material.color = Color.grey;
				gameObject.name = "plain hill";
				gameObject.tag = "plainHill";
				type = "iron";
				resource = Random.Range (100,200);
				break;

			case 3:
				GetComponent<Renderer>().material.color = new Color(0,.43f, 0);
				gameObject.name  = "forest";
				gameObject.tag = "forest";
				type = "lumber";
				resource = Random.Range (200,400);
				break;

			case 4:
				GetComponent<Renderer>().material.color = Color.yellow;
				gameObject.name = "rich hill";
				gameObject.tag = "richHill";
				type = "gold";
				resource = Random.Range (25, 50);	
				break;
			}
		}

	}

	void OnMouseOver()
	{

		if (canMouseOver == true) 
		{
			print (gameObject.name);

			if (Input.GetMouseButton (0))
			{


				caravan.GetComponent<NavMeshAgent> ().SetDestination (transform.position);
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
