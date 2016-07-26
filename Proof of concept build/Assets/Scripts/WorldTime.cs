using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WorldTime : MonoBehaviour
{
	public GameObject sun, caravan, townScreen;
	public Canvas mainC;
	public Text hour, phase;

	public Image clockBG;

	public int addtime, h;
	public float rotateClock, rotateSpeed, timePassing;
	public bool am, start;

	public Quaternion clockFromPos, clockToPos;

	public List<GameObject> roster = new List<GameObject>();


	// Use this for initialization
	void Start () 
	{

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


		clockBG.transform.rotation = Quaternion.Lerp(clockFromPos, clockToPos, timePassing);
		sun.transform.rotation = clockBG.transform.rotation;
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
			}
			else if (h == 6 && am == false)
			{
				sun.GetComponentInChildren<Light>().enabled = false;
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


	
}
