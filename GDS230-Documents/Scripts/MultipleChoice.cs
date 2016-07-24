using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;



public class MultipleChoice : MonoBehaviour 
{
	public Text Question; 
 	public Button a, b, c, d;

	public string textName; 


	// Use this for initialization
	void Start ()
	{
		StreamReader txtReader = StreamReader (Application.persistentDataPath + "/" + textName);
		availableQuestions = TextReader.
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void ReadText()
	{


	}

	void OptionA()
	{
		
	}

	void OptionB()
	{

	}

	void OptionC()
	{

	}

	void OptionD()
	{

	}


}
