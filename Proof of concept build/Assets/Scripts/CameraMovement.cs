using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour 
{

	public int smooth;
	public Transform mainCam, caravan, canvas;

	public float leftB, rightB, frontB, backB;

	public bool LockedCamera, tutorial;

	public Vector3 startPos, startRot;

	public Text cameraLock;

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
			if (LockedCamera && !tutorial)
			{
				FreeMovement ();
				AutoAdjust();
			}
			else
			{
				Follow ();
			}

			CameraSwitch ();

		}
	}

	public void Tutorial()
	{
		tutorial = !tutorial;
	}

	void CameraSwitch()
	{
		if (Input.GetKeyDown (KeyCode.F) && LockedCamera) 
		{
			cameraLock.text = "Camera Locked [F]";
			LockedCamera = !LockedCamera;
			smooth = 3;
		}

		else if (Input.GetKeyDown (KeyCode.F) && !LockedCamera) 
		{
			cameraLock.text = "Camera Unlocked [F]";
			LockedCamera = !LockedCamera;
			smooth = 3;
		}
	}

	void AutoAdjust()
	{
		if (transform.position.x < leftB)
		{
			transform.position = new Vector3(leftB, transform.position.y, transform.position.z);
		}

		if (transform.position.x > rightB)
		{
			transform.position = new Vector3(rightB, transform.position.y, transform.position.z);
		}

		if (transform.position.z > frontB)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, frontB);
		}

		if (transform.position.z < backB)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, backB);
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
		
		if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < (Screen.width * 0.1f))
		{
			mainCam.Translate(Vector3.left * smooth * Time.deltaTime, Space.World);
		}

		if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > (Screen.width * 0.9f))
		{
			mainCam.Translate(Vector3.right * smooth * Time.deltaTime, Space.World);
		}

		if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > (Screen.height * 0.9f))
		{
			mainCam.Translate(Vector3.forward * smooth * Time.deltaTime, Space.World);
		}

		if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < (Screen.height * 0.1f))
		{
			mainCam.Translate(Vector3.back * smooth * Time.deltaTime, Space.World);
		}


		if(Input.mouseScrollDelta.y > 0 && mainCam.transform.position.y > 1.5f)
		{
			mainCam.Translate(Vector3.down * (smooth * 3) * Time.deltaTime, Space.World);
		}

		if(Input.mouseScrollDelta.y < 0 && mainCam.transform.position.y < 7.5f)
		{
			mainCam.Translate(Vector3.up * (smooth * 3) * Time.deltaTime, Space.World);
		}
	
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			smooth *= 2;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)) 
		{
			smooth /= 2;
		}


	}

	void Follow()
	{
		transform.position = Vector3.Slerp (transform.position, new Vector3 (caravan.position.x, caravan.position.y + 3.2f, caravan.position.z - 1.5f), f);

	}


}
