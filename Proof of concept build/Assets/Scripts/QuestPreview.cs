using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class QuestPreview : MonoBehaviour
{
	public int num;
	public GameObject questFunctions;

	public void EnterButton()
	{
		questFunctions.GetComponent<MultipleChoice>().prevN = num;
	}

	public void LeaveButton()
	{
		questFunctions.GetComponent<MultipleChoice>().preview.text = "";
		questFunctions.GetComponent<MultipleChoice>().prevN = 0;
	}

	public void SelectButton()
	{
		questFunctions.GetComponent<MultipleChoice>().choiceQ = num;
	}
}
