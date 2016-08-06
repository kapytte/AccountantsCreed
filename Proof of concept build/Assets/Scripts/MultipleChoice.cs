
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;



//Questions in this section pulled from the Basic accounting test located at www.myaccountingcourse.com/accounting-basic/multiple-choice
[System.Serializable]
public class EasyQuestions 
{
	//Question input variables
	public string preview;
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
	public Text question, buttonA, buttonB, buttonC, buttonD, correct;
	public int goldN, repN, maxQ, choiceQ, prevN, questGiver;
	public bool questActive;
	public RawImage questionArea;
	public string decision;


	public GameObject shops, town;

	//random number and quest lists
	public List <EasyQuestions> lvl1Quest;
	public List <int> randomDraw = new List<int>();

	void Start ()
	{
		//spawns quests at start
		GenerateQuest();
	}

	void Update()
	{
		town = shops.GetComponent<ShopSystem>().town;

		//checks for mouse over quest icons
	
		Preview();

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

		CanQuest();
	}

	//begins quiz
	public void StartQuest()
	{
		//ensures preview window is null
		preview.text = "";

		questActive  = true;

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

		//checks answer against template, if true..
		if (decision == lvl1Quest[randomDraw[choiceQ-1]].answer)
		{
			//show green text and add gold & reputation 
			question.color = Color.green;
			question.text = "Correct";
			repN += lvl1Quest[randomDraw[choiceQ-1]].reputation;
		}

		//if false..
		else
		{
			//show red text and remove reputation
			question.color = Color.red;
			question.text = "Incorrect";
			repN -= lvl1Quest[randomDraw[choiceQ-1]].reputation;
		}

		//enable continue button
		cont.gameObject.SetActive(true);

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



	//calls if hovering over quest icons
	void Preview()
	{
		//checks value thrown to this script by button
		switch(prevN)
		{
		//if zero, show nothing. Otherwise show corrosponding preview

		case 1:
			preview.text = lvl1Quest[randomDraw[0]].preview;
			break;

		case 2:
			preview.text = lvl1Quest[randomDraw[1]].preview;
			break;

		case 3:
			preview.text = lvl1Quest[randomDraw[2]].preview;
			break;

		case 4:
			preview.text = lvl1Quest[randomDraw[3]].preview;
			break;
		}
	}

	//generates a quest from the random generator
	public void GenerateQuest()
	{
		town.GetComponent<Town>().questGivers -= 1;

		//disables quest window and continue button
		questionArea.gameObject.SetActive(false);
		cont.gameObject.SetActive(false);

		questActive  = false;

		town.GetComponent<Town>().GenerateQuest();

	}

}
