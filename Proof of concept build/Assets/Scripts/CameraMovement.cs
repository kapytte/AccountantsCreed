using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{

	public int smooth;
	public Transform mainCam, caravan, canvas;

	public bool LockedCamera;

	public Vector3 startPos, startRot;

	public float f;

	void Awake()
	{
		mainCam = gameObject.transform;

		StartLocation();
	}

	// Use this for initialization
	void Start ()
	{
		GetComponent<Camera>().backgroundColor = Color.grey;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canvas.gameObject.activeInHierarchy == false)
		{
			if (LockedCamera)
				FreeMovement ();
			else
				Follow ();

			if (Input.GetKeyDown (KeyCode.F)) 
			{
				LockedCamera = !LockedCamera;
			}

		}
	}

	//sets camera to starting location on load
	void StartLocation()
	{
		mainCam.position = (startPos);
		mainCam.rotation = (Quaternion.Euler(startRot));
	}

	void FreeMovement()
	{
		if (Input.GetKey(KeyCode.A))
		{
			mainCam.Translate(Vector3.left * smooth * Time.deltaTime, Space.World);
		}

		if (Input.GetKey(KeyCode.W))
		{
			mainCam.Translate(Vector3.forward * smooth * Time.deltaTime, Space.World);
		}

		if (Input.GetKey(KeyCode.D))
		{
			mainCam.Translate(Vector3.right * smooth * Time.deltaTime, Space.World);
		}

		if (Input.GetKey(KeyCode.S))
		{
			mainCam.Translate(Vector3.back * smooth * Time.deltaTime, Space.World);
		}
	}

	void Follow()
	{
		transform.position = Vector3.Slerp (transform.position, new Vector3 (caravan.position.x, caravan.position.y + 3.2f, caravan.position.z - 1.5f), f);

	}
}
