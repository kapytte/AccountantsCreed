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

	// Use this for initialization
	void Awake () 
	{
		center = new Vector3(transform.position.x, transform.position.y / 2, transform.position.z);
	}
	
	// Update is called once per frame
	void Start () 
	{
		

		newTile = Instantiate(tile, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
		newTile.transform.parent = gameObject.transform;

		RaycastHit h;

		Physics.Raycast(center, transform.up, out h, 1f);

	
	}
		


}
