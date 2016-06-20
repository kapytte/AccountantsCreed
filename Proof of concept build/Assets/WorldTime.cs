using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WorldTime : MonoBehaviour
{
	public GameObject minute, hour;
	public Light Sun;
	public Canvas mainC;
	public Text[] words;
	public Text actualT, myT;

	public float m, h;

	void Awake()
	{
		minute = GameObject.Find("Minute");
		hour = GameObject.Find("Hour");

		Sun = Light.FindObjectOfType<Light>();

		words = mainC.GetComponentsInChildren<Text>();
		actualT = words[0];
		myT = words [1];
	}


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Clock();
	}

	void Clock()
	{
		

		if (m > 60)
		{
			m = 0;
		}

		m += Time.deltaTime;

	
		float tConv = m * 6;	

		myT.text = tConv.ToString();

		h += Time.deltaTime/2;

		float hConv = h;

		actualT.text = hConv.ToString();

		minute.transform.rotation = Quaternion.Euler(0, 180, tConv);
		hour.transform.rotation = Quaternion.Euler(0, 180, hConv);

		Sun.transform.rotation = Quaternion.Euler(tConv + 60, 0, 0);
	}


	
}
