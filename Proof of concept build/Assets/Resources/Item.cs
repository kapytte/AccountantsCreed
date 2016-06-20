using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class Item 
{

	[XmlAttribute("name")]
	public string name;

	[XmlAttribute("Damage")]
	public float damage;

	[XmlAttribute("Durability")]
	public float durability;


}
