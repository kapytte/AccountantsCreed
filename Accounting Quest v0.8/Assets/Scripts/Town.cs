using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


//REPRESENTS THE QUESTS AND TRADE GOODS FOUND IN EACH TOWN
[RequireComponent (typeof(SphereCollider))]
public class Town : MonoBehaviour 
{
	public float wheat, fish, lumber, iron, maxIron = 50, maxLumber = 100, maxFish = 150, maxWheat= 200, t, scanRange = 2.5f;

	public int population, workers, ran, questGivers;

	public LayerMask hexLayer = 8;
	public Text[] resoruces;
	public Text tLumber, tIron, tFish, tWheat; 
	public RawImage[] resourceBar;
	public RawImage bLumber, bIron, bFish, bWheat;
	public GameObject can, villager, time, choiceSystem, world;
	public Canvas mainCan;
	public Collider[] resourceScan;
	//public List <GameObject> forest, water, plainHill, richHill;
	public List <GameObject> resourceList = new List<GameObject>();
	//public List <GameObject> villagers = new List<GameObject>();
	public List <int> randomDraw = new List<int>();



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

		home.isTrigger = true;

		choiceSystem = GameObject.Find("QuestGiver");

		time = GameObject.Find("Time");

		can = GameObject.Find("Canvas");
		mainCan = can.GetComponent<Canvas>();

		world = GameObject.Find("World");

		villager = Resources.Load("Villager") as GameObject;
		population = Random.Range(15,30);
		workers = Mathf.CeilToInt(population / 2); 

		makingVillagers = true;
		MakeVillagers();

		GenerateQuest();

		wheat = Mathf.CeilToInt (Random.Range((maxWheat * .25f), (maxWheat * .75f)));
		fish = Mathf.CeilToInt(Random.Range((maxFish * .25f), (maxFish * .75f)));
		lumber = Mathf.CeilToInt(Random.Range((maxLumber * .25f), (maxLumber * .75f)));
		iron = Mathf.CeilToInt(Random.Range((maxIron * .25f), (maxIron * .75f)));

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
		if (wheat > maxWheat)
		{
			wheat = maxWheat;
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
				GameObject v;
				v = Instantiate(villager, transform.position, Quaternion.identity) as GameObject;
				v.GetComponent<Villager>().town = gameObject;
				v.GetComponent<Villager>().townLocation = ran;
				v.transform.parent = world.transform;
				i++;
			}

			makingVillagers = false;

			GenerateQuest();

			wheat -= population/2;
			lumber -= population/4;
			fish -= population/6;
			iron -= population/8;

		}
	}


	//generates a quest from the random generator
	public void GenerateQuest()
	{

		//clears the random draw list of current values
		randomDraw.Clear();

		//generates 4 random numbers from range provided and puts them in a list, ensuring no duplicate numbers. 
		int c = 0;
		while(c < questGivers)
		{
			int i =	Random.Range(0, choiceSystem.GetComponent<MultipleChoice>().lvl1Quest.Count);

			if(!randomDraw.Contains(i)) 
			{
				randomDraw.Add(i);

				c++;
			}
		}
		choiceSystem.GetComponent<MultipleChoice>().randomDraw = randomDraw;
	}


	void OnTriggerEnter(Collider c)
	{
		if (c.name == "Caravan")
		{
			choiceSystem.GetComponent<MultipleChoice>().randomDraw = randomDraw;
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
