using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


public class Town : MonoBehaviour 
{
	public float gold = 25, fish = 75, lumber = 200, iron = 100, maxIron, maxLumber, maxFish, MaxGold, t;

	public Text[] resoruces;
	public Text tLumber, tIron, tFish, tGold; 
	public RawImage[] resourceBar;
	public RawImage bLumber, bIron, bFish, bGold;
	public GameObject can;
	public Canvas mainCan;
	public GameObject[] scouter;
	public List <GameObject> forest, water, plainHill, richHill;



	// Use this for initialization
	void Start ()
	{
		can = GameObject.Find("Canvas");
		mainCan = can.GetComponent<Canvas>();

		resoruces = mainCan.GetComponentsInChildren<Text>();
		tLumber = resoruces[0];
		tIron = resoruces[1];
		tFish = resoruces[2];
		tGold = resoruces[3];

		resourceBar = mainCan.GetComponentsInChildren<RawImage>();
		bLumber = resourceBar[2];
		bIron = resourceBar[4];
		bFish = resourceBar[6];
		bGold = resourceBar[8];


		SortByDistance();
		//foreach ()
		//forest.(transform.position.magnitude);



	}
	
	// Update is called once per frame
	void Update () 
	{
		ResourceCount();
		Drain();



		
	}

	void ResourceCount()
	{
		tLumber.text = "Lumber" + " " + lumber;
		tIron.text = "Iron" + " " + iron;
		tFish.text = "Fish" + " " + fish;
		tGold.text = "Gold" + " " + gold;

		bLumber.rectTransform.anchorMax = new Vector2(lumber / 400,0.5f);
		bIron.rectTransform.anchorMax = new Vector2(iron / 200,0.5f);
		bFish.rectTransform.anchorMax = new Vector2(fish / 150,0.5f);
		bGold.rectTransform.anchorMax = new Vector2(gold / 50,0.5f);

	}

	void Drain()
	{

		t += Time.deltaTime;


		if (t > 10)
		{
			lumber -= 4;
			iron -= 2;
			fish -= 2;
			gold -= 1;

			t = 0;

		}
	}

	void SortByDistance()
	{

	
		//var newList = new List <GameObject>();

		GameObject town = gameObject;

		//LUMBER

		scouter = GameObject.FindGameObjectsWithTag("forest");

		forest = scouter.ToList<GameObject>();
		foreach (GameObject c in forest)
		{
			c.GetComponent<Tile>().dist = Vector3.Distance(transform.position, c.transform.position);

		}
		forest = forest.OrderBy(c => c.GetComponent<Tile>().dist).ToList();

		//WATER
		scouter = GameObject.FindGameObjectsWithTag("water");
		water = scouter.ToList<GameObject>();
		foreach (GameObject c in water)
		{
			c.GetComponent<Tile>().dist = Vector3.Distance(transform.position, c.transform.position);
		}
		water = water.OrderBy(c => c.GetComponent<Tile>().dist).ToList();

		//IRON
		scouter = GameObject.FindGameObjectsWithTag("plainHill");
		plainHill = scouter.ToList<GameObject>();
		foreach (GameObject c in plainHill)
		{
			c.GetComponent<Tile>().dist = Vector3.Distance(transform.position, c.transform.position);
		}
		plainHill = plainHill.OrderBy(c => c.GetComponent<Tile>().dist).ToList();

		//GOLD
		scouter = GameObject.FindGameObjectsWithTag("richHill");
		richHill = scouter.ToList<GameObject>();
		foreach (GameObject c in richHill)
		{
			c.GetComponent<Tile>().dist = Vector3.Distance(transform.position, c.transform.position);
		}
		richHill = richHill.OrderBy(c => c.GetComponent<Tile>().dist).ToList();
	
	}



}
