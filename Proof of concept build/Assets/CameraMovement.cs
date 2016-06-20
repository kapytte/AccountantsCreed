using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{

	public int smooth;
	public Transform mainCam;

	public Vector3 startPos, startRot;

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
		Movement();
	}

	//sets camera to starting location on load
	void StartLocation()
	{
		mainCam.position = (startPos);
		mainCam.rotation = (Quaternion.Euler(startRot));
	}

	void Movement()
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
}
