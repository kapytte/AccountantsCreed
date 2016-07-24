using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof(SphereCollider))]
public class Town : MonoBehaviour 
{
	public float gold = 25, fish = 75, lumber = 200, iron = 100, maxIron = 200, maxLumber = 400, maxFish = 150, MaxGold = 50, t, scanRange = 2.5f;

	public int population, workers, ran, questGivers;

	public LayerMask hexLayer = 8;
	public Text[] resoruces;
	public Text tLumber, tIron, tFish, tGold; 
	public RawImage[] resourceBar;
	public RawImage bLumber, bIron, bFish, bGold;
	public GameObject can, villager, time;
	public Canvas mainCan;
	public Collider[] resourceScan;
	//public List <GameObject> forest, water, plainHill, richHill;
	public List <GameObject> resourceList = new List<GameObject>();
	//public List <GameObject> villagers = new List<GameObject>();

	//public List <ScriptableObject> resources = new List<ScriptableObject>();
	public bool makingVillagers;


	// Use this for initialization
	void Start ()
	{
		//unnessiary joke
		Transform heart = gameObject.transform;
		SphereCollider home = GetComponent<SphereCollider>();
		home.transform.position = heart.transform.position;

		ran = Random.Range(1,100);

		home.radius = 0.5f;
		home.isTrigger = true;

		time = GameObject.Find("Time");

		can = GameObject.Find("Canvas");
		mainCan = can.GetComponent<Canvas>();

		villager = Resources.Load("Villager") as GameObject;

		population = Random.Range(15,30);
		workers = Mathf.CeilToInt(population / 2); 

		makingVillagers = true;
		MakeVillagers();

//		resoruces = mainCan.GetComponentsInChildren<Text>();
//		tLumber = resoruces[0];
//		tIron = resoruces[1];
//		tFish = resoruces[2];
//		tGold = resoruces[3];

//		resourceBar = mainCan.GetComponentsInChildren<RawImage>();
//		bLumber = resourceBar[2];
//		bIron = resourceBar[4];
//		bFish = resourceBar[6];
//		bGold = resourceBar[8];


		//SortByDistance();
		//foreach ()
		//forest.(transform.position.magnitude);

		FindNearbyResoruces();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (time.GetComponent<WorldTime>().h == 7 && time.GetComponent<WorldTime>().am == true)
		{
			//print ("hey");
			MakeVillagers();
		}

		MaxAmount();
	}

//	void ResourceCount()
//	{
//		tLumber.text = "Lumber" + " " + lumber;
//		tIron.text = "Iron" + " " + iron;
//		tFish.text = "Fish" + " " + fish;
//		tGold.text = "Gold" + " " + gold;
//
//		bLumber.rectTransform.anchorMax = new Vector2(lumber / 400,0.5f);
//		bIron.rectTransform.anchorMax = new Vector2(iron / 200,0.5f);
//		bFish.rectTransform.anchorMax = new Vector2(fish / 150,0.5f);
//		bGold.rectTransform.anchorMax = new Vector2(gold / 50,0.5f);
//
//	}
		
	void MaxAmount()
	{
		if (gold > MaxGold)
		{
			gold = MaxGold;
		}
		if (lumber > maxLumber)
		{
			lumber = maxLumber;
		}
		if (fish > maxFish)
		{
			fish = maxFish;
		}
		if (iron > maxIron)
		{
			iron = maxIron;
		}

		if (questGivers > 4)
		{
			questGivers = 4;
		}
	}


	//scans for surrounding resources 
	void FindNearbyResoruces()
	{
		print(gameObject.layer);
		hexLayer = (gameObject.layer);
		resourceScan = Physics.OverlapSphere(transform.position, scanRange);
	
		foreach (Collider c in resourceScan)
		{
			if (c.gameObject.tag == "Hex")
			{
				resourceList.Add(c.gameObject);
				c.GetComponent<Tile>().dist = Vector3.Distance(transform.position, c.transform.position);
			}
		}
			
	}
		

	void MakeVillagers()
	{
		if (makingVillagers == true)
		{
			questGivers ++;

			int i = 0;
			while (i < workers)
			{
				Instantiate(villager, transform.position, Quaternion.identity);
				villager.GetComponent<Villager>().town = gameObject;
				villager.GetComponent<Villager>().townLocation = ran;
				i++;
			}
			makingVillagers = false;

			gold -= population/6;
			fish -= population/2;
			lumber -= population/3;
			iron -= population/4;

		}
	}








	/*
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

	*/

}
