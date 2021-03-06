﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WorldTime : MonoBehaviour
{
	public GameObject sun, caravan, townScreen, choice, camp, shops, loseScreen, startScreen, metrics;
	public Canvas mainC;
	public Text hour, phase, debtT, daysT;

	public Image clockBG;

	public int addtime, h, debt, days, week;
	public float rotateClock, rotateSpeed, timePassing;
	public bool am, start, day;

	public Quaternion clockFromPos, clockToPos;

	public List<GameObject> roster = new List<GameObject>();


	//sets the time and debt to default values
	void Start () 
	{
		
		day = true;

		days = 10;
		daysT.text = days.ToString();

		debt = 20;
		debtT.text = debt.ToString ();

		metrics = GameObject.Find("Metrics");
		metrics.GetComponent<Metrics>().repeats += 1;
	}
	
	//simulates time passing
	void Update ()
	{
		Clock();


		timePassing = Time.time - rotateSpeed;
		if(start)
		{
			SimulateDayCycle();
		}

		ClosingTime();
//		WakeUp();
	}

	//rotates clock based on amount of time passing
	void SimulateDayCycle()
	{
		if(timePassing < 1)
		{
			clockBG.transform.rotation = Quaternion.Lerp(clockFromPos, clockToPos, timePassing);
			sun.transform.rotation = clockBG.transform.rotation;
		}
		else
		{
			start = false;
		}
	}

	//cycles between am & pm
	void Clock()
	{

		if (h > 11)
		{
			h = 0;
			am = !am;
		}

		int realhour = h + 1;
		hour.text = realhour.ToString();

		if (am)
		{
			phase.text = "am";
		}
		else
		{
			phase.text = "pm";
		}



		//Sun.transform.rotation = Quaternion.Euler(tConv + 60, 0, 0);
	}

	//adds time and checks for time-specific functions
	public void AddTime()
	{
		
		if (timePassing > 1)
		{
			//time on clock
			h += addtime;

			rotateClock = (15 * addtime);
			rotateSpeed = Time.time;

			clockFromPos = clockBG.transform.rotation;
		
			Vector3 tempVec = clockBG.transform.rotation.eulerAngles;
			clockToPos = Quaternion.Euler(0,0, tempVec.z - rotateClock);

			if (h == 5 && am)
			{
				sun.GetComponentInChildren<Light>().enabled = true;
				caravan.GetComponent<Caravan>().danger = 1;
				if (SceneManager.GetActiveScene().buildIndex == 1)
				{
					shops.GetComponent<ShopSystem> ().Outpost ();
				}
				day = true;
				if (camp.activeInHierarchy)
				{
					camp.SetActive(false);
				}
			}
			else if (h == 6 && am == false)
			{
				sun.GetComponentInChildren<Light>().enabled = false;
				caravan.GetComponent<Caravan>().danger = 2;
				day = false;
				if (SceneManager.GetActiveScene().buildIndex == 1)
				{
					camp.SetActive(true);
				}
			}

			if (h == 11 && am == false) 
			{
				days -= 1;
				metrics.GetComponent<Metrics>().days += 1;
				daysT.text = days.ToString ();

				if (days == 0) 
				{
					Day0();

				}
			}



			if (roster.Capacity > 0)
			{		
				foreach(GameObject v in roster)
				{
					if (v.GetComponent<Villager>().target !=  null)
					{
						v.GetComponent<Villager>().SomethingNeedDoing();
					}
				}
			}
			start = true;
		}

	

	}

	public void LeaveTown()
	{
//		caravan.GetComponent<Caravan>().enabled = true;
//		caravan.GetComponent<NavMeshAgent>().enabled = true;
		townScreen.SetActive(false);
	}

	//sends villagers home after 5pm
	void ClosingTime()
	{
		if(h == 4 && am == false)
		{
			foreach(GameObject v in roster)
			{
				v.GetComponent<Villager>().GoHome();

				//roster.Remove(v);
			}
		}
	}


	//speeds time up till 6am and runs relevant time functions
	public void Camp()
	{
		if (h < 11 && am == false) 
		{
			days -= 1;
			daysT.text = days.ToString ();

			metrics.GetComponent<Metrics>().days += 1;
		}

		h = 5;
		am = true;
		day = true;

		rotateSpeed = Time.time;

		clockFromPos = clockBG.transform.rotation;

		clockToPos = new Quaternion(0, 0, 0.7f, 0.7f);

		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			shops.GetComponent<ShopSystem> ().Outpost ();
		}

		sun.GetComponentInChildren<Light>().enabled = true;
		caravan.GetComponent<Caravan>().danger = 1;
		if (camp.activeInHierarchy)
		{
			camp.SetActive(false);
		}

		caravan.GetComponent<Caravan>().currentTile.GetComponent<Tile>().ShuffleAll();

		if (days == 0) 
		{
			Day0();
		}

		start = true;
	}

	//calls when debt is due, possible lose state
	void Day0()
	{
		choice.GetComponent<MultipleChoice> ().goldN -= debt;

		if (choice.GetComponent<MultipleChoice> ().goldN > 0) 
		{
			debt += 10;
			debtT.text = debt.ToString ();
			days = 10;
			daysT.text = days.ToString ();
		} 
		else 
		{
			loseScreen.SetActive (true);
			metrics.GetComponent<Metrics>().gameState = "Lost Game";
			metrics.GetComponent<Metrics>().LevelStats();
		}

		week += 1;
	}
}
