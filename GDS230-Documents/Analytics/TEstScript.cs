using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

using System.IO;
using System.Xml;

public class TEstScript : MonoBehaviour 
{
	//IDictionary<string, object> startData = new Dictionary<string, object>();
	Dictionary<string, object> gameStart = new Dictionary<string, object>(); 

	// Use this for initialization
	void Start ()
	{
		gameStart.Add ("DeviceID", SystemInfo.deviceType);
		Analytics.CustomEvent ("Device Type", gameStart);


	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
