using UnityEngine;
using System.Collections;

public class borderscript : MonoBehaviour
{
	public Camera mainCamera;
	public Vector3 direciton;
	public GameObject canvas;
	public float smooth;

	// Use this for initialization
	void Start () 
	{
		mainCamera = Camera.main;
		smooth = mainCamera.GetComponent<CameraMovement>().smooth;
	}

	void OnMouseOver()
	{
		if (mainCamera.GetComponent <CameraMovement>().LockedCamera == false && canvas.gameObject.activeInHierarchy == false)
		{
			mainCamera.transform.Translate(direciton * smooth * Time.deltaTime, Space.World);
		}
	}
}
