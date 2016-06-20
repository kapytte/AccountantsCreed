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

	
//		vert = box.size.y / 2;
//		hori = box.size.x *.75f;

//		CheckNearby();
//		Instantiate(box, new Vector3(gameObject.transform.position.x - hori, gameObject.transform.position.y , gameObject.transform.position.z - vert), Quaternion.Euler(-90,0,0));


	}
		
	void Update()
	{
		
	}

	void CheckNearby()
	{

		Debug.DrawRay(center, transform.up, Color.green);
		Debug.DrawRay(center, new Vector3(0.8f, 0f, 0.5f), Color.red);
		Debug.DrawRay(center, new Vector3(0.8f, 0f,- 0.5f), Color.yellow);
		Debug.DrawRay(center, -transform.up, Color.blue);
		Debug.DrawRay(center, new Vector3(-0.8f, 0f, -0.5f), Color.cyan);
		Debug.DrawRay(center, new Vector3(-0.8f, 0f, 0.5f), Color.white);



	}




}
