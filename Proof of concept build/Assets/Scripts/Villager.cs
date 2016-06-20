using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour
{
	public string Job;
	public float fish, gold, lumber, iron;
	public NavMeshAgent nav;
	public GameObject town;
	public bool hasResources;


	public List <GameObject> resourceSpots;

	// Use this for initialization
	void Start ()
	{
		town = GameObject.Find("town");
		nav = GetComponent<NavMeshAgent>();

		JobProspects();
	}
	
	// Update is called once per frame
	void Update () 
	{
		town = GameObject.Find("town");
		//JobProspects();
	}

	void JobProspects()
	{
		
		int c = Random.Range(1,5);

		switch (c)
		{
		case 1:
			Job = "Woodcutter";
			resourceSpots = town.GetComponent<Town>().forest;
			break;

		case 2:
			Job = "Miner";
			resourceSpots = town.GetComponent<Town>().plainHill;
			break;

		case 3:
			Job = "Fisher";
			resourceSpots = town.GetComponent<Town>().water;
			break;

		case 4:
			Job = "Prospector";
			resourceSpots = town.GetComponent<Town>().richHill;
			break;
		}

	}

	void FindWork()
	{
		if (hasResources == false )
		{
			foreach (GameObject c in resourceSpots)
			{
				if (c.GetComponent<Tile>().available == true)
				{
					nav.destination = c.transform.position;
					break;
				}
			}
		}
	}
}
