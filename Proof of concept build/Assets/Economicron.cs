using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Economicron : MonoBehaviour 
{
	public enum script {one, two, three, four};
	public script canvScript;

	public Image canv1, canv2, canv3, canv4;
	public float a, b, c, d, e, g;
	public int h;
	public bool one, running;

	public float speed;
	
	// Update is called once per frame
	void Update ()
	{
		canv1.GetComponent<CanvasRenderer>().SetAlpha(a);
		canv2.GetComponent<CanvasRenderer>().SetAlpha(b);
		canv3.GetComponent<CanvasRenderer>().SetAlpha(c);
		canv4.GetComponent<CanvasRenderer>().SetAlpha(d);

		if (Input.GetMouseButtonDown(0))
		{
			running = true;
			one = !one;
		}

		if (running & one)
		{
			FadeIn();
		}

		if (running & one == false)
		{
			FadeOut();

		}
	}

	void FadeIn ()
	{
		e = Time.deltaTime * speed;

		if (one)
			g += e;

		h = Mathf.CeilToInt(g);

		switch (h)
		{
		case 1: 
			a += e;
			break;
		case 2: 
			b += e;
			break;
		case 3: 
			c += e;
			break;
		case 4: 
			d += e;
			break;
		case 5: 
			a -= e;
			break;
		case 6: 
			b -= e;
			break;
		case 7: 
			c -= e;
			break;
		case 8:
			g = 7;
			running = !running;
			break;
		}
	}

	void FadeOut ()
	{
		e = Time.deltaTime * speed;
		g -= e;

		h = Mathf.CeilToInt(g);

		switch (h)
		{
		case 0:
			g = 0;
			running = !running;
			break;
		case 1: 
			a -= e;
			break;
		case 2: 
			b -= e;
			break;
		case 3: 
			c -= e;
			break;
		case 4: 
			d -= e;
			break;
		case 5: 
			a += e;
			break;
		case 6: 
			b += e;
			break;
		case 7: 
			c += e;
			break;
		}
	}

}
