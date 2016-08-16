using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour 
{
	
	public GameObject shopSystem, town1;

	// Use this for initialization
	void Start ()
	{

	}

	public void EnterTown()
	{
		shopSystem.GetComponent<ShopSystem>().towns[0].GetComponentInChildren<Tile>().canMouseOver = true;
	}

}
