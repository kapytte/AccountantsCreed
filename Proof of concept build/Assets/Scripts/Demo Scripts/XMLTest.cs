using UnityEngine;
using System.Collections;


using System.IO;
using System.Xml;

public class XMLTest : MonoBehaviour 
{

	string playerName = "SeniorPete";
	string sessionStartTime = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString();

	//declare file name
	string filename = "XMLTest.xml";

	//assign new xml document file
	XmlDocument xml = new XmlDocument ();

	// Use this for initialization
	void Start () {
		CreateXML ();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ReadXML();
		}
	}

	private void CreateXML()
	{



		//catch to check if xml file already exists
		if (File.Exists(Application.persistentDataPath + "/" + filename))
		{
			//Load in XMl
			xml.Load(Application.persistentDataPath + "/" + filename);

		}
			
		else 
		{
			//create XML files
			XmlElement root = xml.CreateElement("GameSessions");
			XmlElement id = xml.CreateElement ("ID");

			//load data pertaining to device type
			id.InnerXml = SystemInfo.deviceUniqueIdentifier;

			//assign hierarchy of files
			root.AppendChild (id);
			xml.AppendChild (root);

		}

		//create session-specific file
		XmlElement session = xml.CreateElement ("Session");

		//player start time log
		XmlElement timeStamp = xml.CreateElement ("TimeStamp");
		timeStamp.InnerText = sessionStartTime; 
		session.AppendChild (timeStamp);


		//player name log
		XmlElement currentPlayer = xml.CreateElement ("playerName");
		currentPlayer.InnerText = playerName;
		session.AppendChild (currentPlayer);

		//access document itself (not root)
		xml.DocumentElement.AppendChild (session);

		xml.Save (Application.persistentDataPath + "/" + filename);


	}

	void ReadXML()
	{
		xml.Load(Application.persistentDataPath + "/" + filename);


	}
}
