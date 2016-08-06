using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WorldTime : MonoBehaviour
{
	public GameObject sun, caravan, townScreen, choice, camp;
	public Canvas mainC;
	public Text hour, phase, debtT, daysT;

	public Image clockBG;

	public int addtime, h, debt, days;
	public float rotateClock, rotateSpeed, timePassing;
	public bool am, start, day;

	public Quaternion clockFromPos, clockToPos;

	public List<GameObject> roster = new List<GameObject>();


	// Use this for initialization
	void Start () 
	{
		day = true;

		days = 30;
		daysT.text = days.ToString();

		debt = 20;
		debtT.text = debt.ToString ();
	}
	
	// Update is called once per frame
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

	public void AddTime()
	{
		//Mathf.Round(clockBG.transform.rotation.eulerAngles.z);
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
				camp.SetActive(true);
			}

			if (h == 11 && am == false) 
			{
				days -= 1;
				daysT.text = days.ToString ();
			}

			if (days == 0) 
			{
				choice.GetComponent<MultipleChoice> ().goldN -= debt;

				debt += 10;
				debtT.text = debt.ToString ();
				days = 30;
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

//	void WakeUp()
//	{
//		if(h == 7 && am == true)
//		{
//			print("hey");
//			foreach(GameObject v in roster)
//			{
//				v.gameObject.SetActive(true);
//				//v.GetComponent<Villager>().JobProspects();
//
//				//roster.Remove(v);
//			}
//		}
//	}

	public void Camp()
	{
		if (h < 11 && am == false) 
		{
			days -= 1;
			daysT.text = days.ToString ();
		}

		h = 5;
		am = true;
		day = true;

		rotateSpeed = Time.time;

		clockFromPos = clockBG.transform.rotation;

		clockToPos = new Quaternion(0, 0, 0.7f, 0.7f);

		sun.GetComponentInChildren<Light>().enabled = true;
		caravan.GetComponent<Caravan>().danger = 1;
		if (camp.activeInHierarchy)
		{
			camp.SetActive(false);
		}

		caravan.GetComponent<Caravan>().currentTile.GetComponent<Tile>().ShuffleAll();

		start = true;
	}

	
}
