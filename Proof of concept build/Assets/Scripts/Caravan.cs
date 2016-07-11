using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//OBJECTIVES
//check which hex player is on
//check surrounding hexes and enable selection
//move to new hex when player selects it


public class Caravan : MonoBehaviour
{
	public Collider currentTile;

	public LayerMask hexLayer;

	public Collider[] surroundingTiles;

	public float overlapR;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (GetComponent<NavMeshAgent> ().velocity.magnitude == 0) 
		{
			CheckTiles ();
		} 
		else
		{
			CannotSelect ();
		}

	}

	void CheckTiles()
	{
		//fire raycast downward and create reference to current Hex
		RaycastHit h;

		Physics.Raycast (transform.position, -transform.up, out h);

		currentTile = h.collider;



		surroundingTiles = Physics.OverlapSphere (transform.position, overlapR, hexLayer);

		foreach (Collider c in surroundingTiles)
		{
			if (c != currentTile)
				c.GetComponent<Tile> ().canMouseOver = true;

		}

	}

	public void CannotSelect()
	{
		foreach (Collider c in surroundingTiles)
		{
			c.GetComponent<Tile> ().canMouseOver = false;
		}
	}




	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, overlapR);

	}


}
