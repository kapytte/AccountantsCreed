using UnityEngine;
using System.Collections;
//using System;

//[RequireComponent (typeof(BoxCollider))]
//[RequireComponent (typeof(Rigidbody))]

public class Hex : MonoBehaviour
{
	public GameObject tile, newTile;
	public float vert, hori;
	 
	public Vector3 center;

	//INSTANTIATES HEX TILES AT THE START OF THE GAME
	// Use this for initialization
	void Awake () 
	{
		center = new Vector3(transform.position.x, transform.position.y / 2, transform.position.z);
	}
	
	// Update is called once per frame
	void Start () 
	{
		newTile = Instantiate(tile, transform.position, Quaternion.identity) as GameObject;
		newTile.transform.parent = gameObject.transform;
		newTile.GetComponentInChildren<Tile>().parent = gameObject;
		newTile.GetComponentInChildren<Tile>().gameObject.layer = 8;

		RaycastHit h;

		Physics.Raycast(center, transform.up, out h, 1f);
	}
		


}
