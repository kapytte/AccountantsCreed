using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Questions : MonoBehaviour 
{
	public GameObject questGiver;

	public void Decision()
	{
		questGiver.GetComponent<MultipleChoice>().decision = gameObject.GetComponentInChildren<Text>().text;
	}
}
