using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour
{
	public string jobType;
	public int fish, gold, lumber, iron;
	public NavMeshAgent nav;
	public GameObject town, target, nextHex, time;
	public bool goingHome;

	public int townLocation;

	public enum job {miner, woodcutter, fisher, prospector};
	public job profession;

	// Use this for initialization
	void Start ()
	{
		gameObject.name = "Villager";

		time = GameObject.Find("Time");
		nav = GetComponent<NavMeshAgent>();

		Education();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (town == null)
		{
			town = GameObject.Find("town");
		}

		if (target == null)
		{
			Education();
		}

	//	Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), target.transform.position - transform.position, Color.green, 10);
	}

	public	void JobProspects()
	{
		
		switch (profession)
		{
		case job.woodcutter:
			jobType = "Woodcutter";
			foreach(GameObject c in town.GetComponent<Town>().resourceList)
			{
				if (c.name == "forest")
				{
					target = c.gameObject;
					town.GetComponent<Town>().resourceList.Remove(c);
					time.GetComponent<WorldTime>().roster.Add(gameObject);
					SomethingNeedDoing();
					break;
				}
			}
			break;

		case job.miner:
			jobType = "Miner";
			foreach(GameObject c in town.GetComponent<Town>().resourceList)
			{
				if (c.name == "plain hill")
				{
					target = c.gameObject;
					town.GetComponent<Town>().resourceList.Remove(c);
					time.GetComponent<WorldTime>().roster.Add(gameObject);
					SomethingNeedDoing();
					break;
				}
			}
			break;

		case job.fisher:
			jobType = "Fisher";
			foreach(GameObject c in town.GetComponent<Town>().resourceList)
			{
				if (c.name == "water")
				{
					target = c.gameObject;
					town.GetComponent<Town>().resourceList.Remove(c);
					time.GetComponent<WorldTime>().roster.Add(gameObject);
					SomethingNeedDoing();
					break;
				}

			}

			break;

		case job.prospector:
			jobType = "Prospector";
			foreach(GameObject c in town.GetComponent<Town>().resourceList)
			{
				if (c.name == "rich hill")
				{
					target = c.gameObject;
					town.GetComponent<Town>().resourceList.Remove(c);
					time.GetComponent<WorldTime>().roster.Add(gameObject);
					SomethingNeedDoing();
					break;
				}

			}
			break;
		}
	}
	public void Education()
	{
		int i = Random.Range(1, 5);

		switch(i)
		{
		case 1:
			profession = job.woodcutter;
			break;
		case 2:
			profession = job.miner;
			break;
		case 3:
			profession = job.fisher;
			break;
		case 4:
			profession = job.prospector;
			break;
		}
		JobProspects();

	}
		

	public void SomethingNeedDoing()
	{
		if (target != null)
		{
			Vector3 sight = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			RaycastHit[] hexes = Physics.SphereCastAll(transform.position, 0.2f, target.transform.position - transform.position, Vector3.Distance(transform.position, target.transform.position));

			foreach(RaycastHit c in hexes)
			{
				
				if (Vector3.Distance(transform.position, c.transform.position) < 2 && c.collider.tag == "Hex" && c.collider.gameObject != nextHex)
				{
					
					nextHex = c.collider.gameObject;
					break;
				}

			}

			nav.destination = nextHex.transform.position;

			//nav.destination = target.transform.position;

			WorkWork();
		}

	}

	void WorkWork()
	{
		if (Vector3.Distance(transform.position, target.transform.position) < .5f && target != town)
		{
			int i = Random.Range(1, 101);

		
			switch(target.name)
			{
			case "forest":
				if (i > 50)
					lumber ++;
				break;
			case "water":
				if (i > 30)
					fish ++;
				break;
			case "plain hill":
				if (i > 60)
					iron ++;
				break;
			case "rich hill":
				if (i > 70)
					gold ++;
				break;
			}
		}
	}

	public void GoHome()
	{
		if (goingHome == false)
		{
			town.GetComponent<Town>().resourceList.Add(target);
			town.GetComponent<Town>().makingVillagers = true;

			target = town;
			goingHome = true;
		}

	//	time.GetComponent<WorldTime>().roster.Remove(gameObject);
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == town && target == town)
		{
			time.GetComponent<WorldTime>().roster.Remove(gameObject);
			goingHome = false;
			switch (profession)
			{
			case job.fisher:
				town.GetComponent<Town>().fish += fish;
				Destroy(gameObject);
				break;
			case job.miner:
				town.GetComponent<Town>().iron += iron;
				Destroy(gameObject);
				break;
			case job.prospector:
				town.GetComponent<Town>().gold += gold;
				Destroy(gameObject);
				break;
			case job.woodcutter:
				town.GetComponent<Town>().gold += gold;
				Destroy(gameObject);
				break;
			}
		}
	}


}
