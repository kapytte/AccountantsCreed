﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


//QUESTION SYSTEM NESSISARY FOR WIN CONDITION, GETS RANDOM QUESTIONS FROM LIST
[System.Serializable]
public class EasyQuestions 
{
	//Question input variables
	public string topic;

	public string question;
	public string optionA;
	public string optionB;
	public string optionC;
	public string optionD;
	public string answer;
	public int PageNum;
	public int reputation;
}
	

public class MultipleChoice : MonoBehaviour 
{
	
	public Button q1, q2, q3, q4, cont;
	public Text preview, goldS, repS;
	public Text question, buttonA, buttonB, buttonC, buttonD;
	public int goldN, repN, maxQ, choiceQ, prevN, questGiver, repReward;
	public bool questActive, startTimer;
	public RawImage questionArea;
	public string decision;
	public float timerW;

	public GameObject shops, town, clock, metrics, winScreen, ecnonom;

	//random number and quest lists
	public List <EasyQuestions> lvl1Quest;
	public List <int> randomDraw = new List<int>();

	void Start ()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			GenerateQuest();

			metrics = GameObject.Find("Metrics");
		}//spawns quests at start

	}

	void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			MainLevel();
		}

		if (SceneManager.GetActiveScene().buildIndex == 2)
		{
			TutorialScene();
		}

	}

	void MainLevel()
	{
		town = shops.GetComponent<ShopSystem>().town;

		if (metrics == null)
		{
			metrics = GameObject.Find("Metrics");
		}

		//checks for mouse over quest icons

		if (prevN != 0)
		{
			Preview();
		}

		//ensures reputation does not go below 0
		if (repN <=0)
		{
			repN = 0;
		}

		//converts reputation and gold to UI element
		goldS.text = goldN.ToString();
		repS.text = repN.ToString();


		if (town != null)
		{
			QuestAmount();
		}

		if (startTimer)
		{
			WindowTimer();
		}

		CanQuest();
	}

	void TutorialScene()
	{
		goldS.text = goldN.ToString();
		repS.text = repN.ToString();

		if (prevN != 0)
		{
			Preview();
		}

		//ensures reputation does not go below 0
		if (repN <=0)
		{
			repN = 0;
		}
	}


	void WindowTimer()
	{
		timerW += Time.deltaTime;
	}

	//begins quiz
	public void StartQuest()
	{
		//ensures preview window is null
		preview.text = "";

		//creates temporary reward value
		repReward = lvl1Quest[randomDraw[choiceQ-1]].reputation;

		startTimer = true;
		questActive = true;

		//opens question window
		questionArea.gameObject.SetActive(true);

		//enables multiple choice buttons
		buttonA.enabled = true;
		buttonB.enabled = true;
		buttonC.enabled = true;
		buttonD.enabled = true;
		buttonA.GetComponentInParent<Button>().interactable = true;
		buttonB.GetComponentInParent<Button>().interactable = true;
		buttonC.GetComponentInParent<Button>().interactable = true;
		buttonD.GetComponentInParent<Button>().interactable = true;

		//resets question text to white and pulls quest info from list of questons
		question.color = Color.white;
		question.text = lvl1Quest[randomDraw[choiceQ-1]].question;
		buttonA.text = lvl1Quest[randomDraw[choiceQ-1]].optionA;
		buttonB.text = lvl1Quest[randomDraw[choiceQ-1]].optionB;
		buttonC.text = lvl1Quest[randomDraw[choiceQ-1]].optionC;
		buttonD.text = lvl1Quest[randomDraw[choiceQ-1]].optionD;

		//disables quest markers


	}
		
	public void Hint()
	{
		repReward = 0;

		int i = lvl1Quest [randomDraw [choiceQ - 1]].PageNum;

		int j = Mathf.CeilToInt (i / 2);

		ecnonom.GetComponent<Economicron> ().currentPage = j; 

	}



	void CanQuest()
	{
		if (shops.GetComponent<ShopSystem>().market.isActiveAndEnabled == true || 
			shops.GetComponent<ShopSystem>().mercenaries.isActiveAndEnabled == true ||
			shops.GetComponent<ShopSystem>().outpost.isActiveAndEnabled == true || questActive)
		{
			q1.gameObject.SetActive(false); 
			q2.gameObject.SetActive(false); 
			q3.gameObject.SetActive(false); 
			q4.gameObject.SetActive(false); 
		}
		else
		{
			q1.gameObject.SetActive(true); 
			q2.gameObject.SetActive(true); 
			q3.gameObject.SetActive(true); 
			q4.gameObject.SetActive(true); 
		}
	}

	//called when player makes a selection in quest window
	public void Choice()
	{
		//removes interaction from multiple-choice buttons
		buttonA.GetComponentInParent<Button>().interactable = false;
		buttonB.GetComponentInParent<Button>().interactable = false;
		buttonC.GetComponentInParent<Button>().interactable = false;
		buttonD.GetComponentInParent<Button>().interactable = false;

		startTimer = false;

		//checks answer against template, if true..
		if (decision == lvl1Quest[randomDraw[choiceQ-1]].answer)
		{
			//show green text and add reputation 
			GoodAnswer();
		}

		//if false..
		else
		{
			//show red text and remove reputation
			BadAnswer();
		}

		//enable continue button
		cont.gameObject.SetActive(true);

	}

	public void GoodAnswer()
	{
		question.color = Color.green;
		question.text = "Good Advice";
		repN += repReward;
		metrics.GetComponent<Metrics>().questionsRight += 1;
	}

	public void BadAnswer()
	{
		question.color = Color.red;
		question.text = "Bad Advice";
		repN -= lvl1Quest[randomDraw[choiceQ-1]].reputation;
		metrics.GetComponent<Metrics>().questionsWrong += 1;
	}

	void QuestAmount()
	{
		questGiver = town.GetComponent<Town>().questGivers;

		switch  (questGiver)
		{
		case 0:
			q1.image.enabled = false;
			q2.image.enabled = false;
			q3.image.enabled = false;
			q4.image.enabled = false;
			break;

		case 1: 
			q1.image.enabled = true;
			q2.image.enabled = false;
			q3.image.enabled = false;
			q4.image.enabled = false;
			break;
		
		case 2: 
			q1.image.enabled = true;
			q2.image.enabled = true;
			q3.image.enabled = false;
			q4.image.enabled = false;
			break;
		case 3: 
			q1.image.enabled = true;
			q2.image.enabled = true;
			q3.image.enabled = true;
			q4.image.enabled = false;
			break;
		case 4: 
			q1.image.enabled = true;
			q2.image.enabled = true;
			q3.image.enabled = true;
			q4.image.enabled = true;
			break;
		}
	}


	void DidIWin()
	{
		if (repN >= 100)
		{
			winScreen.SetActive(true);
			metrics.GetComponent<Metrics>().gameState = "Won Game";
			metrics.GetComponent<Metrics>().LevelStats();
		}
	}


	//calls if hovering over quest icons
	void Preview()
	{
		preview.text = 
			lvl1Quest[randomDraw[prevN-1]].topic + "\n" + "\n" + "Book Page " + 
			lvl1Quest[randomDraw[prevN-1]].PageNum + "\n" + "\n" + "Integrity " + 
			lvl1Quest[randomDraw[prevN-1]].reputation;

//		//checks value thrown to this script by button
//		switch(prevN)
//		{
//		//if zero, show nothing. Otherwise show corrosponding preview
//
//		case 1:
//			preview.text = lvl1Quest[randomDraw[0]].topic;
//			break;
//
//		case 2:
//			preview.text = lvl1Quest[randomDraw[1]].topic;
//			break;
//
//		case 3:
//			preview.text = lvl1Quest[randomDraw[2]].topic;
//			break;
//
//		case 4:
//			preview.text = lvl1Quest[randomDraw[3]].topic;
//			break;
//		}
	}

	//generates a quest from the random generator
	public void GenerateQuest()
	{
		Analytics.CustomEvent("Question", new Dictionary<string, object>
			{
				{"Day", clock.GetComponent<WorldTime>().days},
				{"Week", clock.GetComponent<WorldTime>().week},
				{"Integrity", repN},
				{"Question", lvl1Quest[randomDraw[choiceQ-1]].question},
				{"Answer", lvl1Quest[randomDraw[choiceQ-1]].answer},
				{"Result", decision},
				{"WindowTime", timerW}
			});

		timerW = 0;

		town.GetComponent<Town>().questGivers -= 1;

		//disables quest window and continue button
		questionArea.gameObject.SetActive(false);
		cont.gameObject.SetActive(false);

		questActive = false;

		town.GetComponent<Town>().GenerateQuest();

	}

}
