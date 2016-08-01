using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class EconPages
{
	public string topic;
	public Texture fade1, fade2, fade3, text;
}



public class Economicron : MonoBehaviour 
{
	public List<EconPages> bookPages;
	public enum syllabus {one, two, three, four};
	public syllabus topic;


	public Button goLeft, goRight, closeButton;
	public RawImage canv1, canv2, canv3, canv4, leftPage, rightPage;
	public GameObject content;
	public float a, b, c, d, e, g, i, openSpeed;
	public int h, page;
	public bool one, running, open, close;

	public float speed;

	void Start()
	{
		leftPage.rectTransform.localScale = new Vector2(0,13.5f);
		rightPage.rectTransform.localScale = new Vector2(0,13.5f);

		page = 0;

		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		canv1.GetComponent<RawImage>().color = new Color(1, 1, 1, a);
		canv2.GetComponent<RawImage>().color = new Color(1, 1, 1, b);
		canv3.GetComponent<RawImage>().color = new Color(1, 1, 1, c);
		canv4.GetComponent<RawImage>().color = new Color(1, 1, 1, d);


		if (running & one)
		{
			FadeIn();
		}

		if (running & one == false)
		{
			FadeOut();
		}

		if (open)
		{
			OpenBook();
		}

		if (close)
		{
			CloseBook();
		}
	}




	public void OpenBook()
	{
		page = 0;
		goLeft.interactable = false;
		goRight.interactable = true;

		canv1.texture = bookPages[page].fade1;
		canv2.texture = bookPages[page].fade2;
		canv3.texture = bookPages[page].fade3;
		canv4.texture = bookPages[page].text;

		i += openSpeed * Time.deltaTime;

		if (i < 20)
		{
			Cursor.visible = false;
			open = true;
			leftPage.rectTransform.localScale = new Vector2(i,13.5f);
			rightPage.rectTransform.localScale = new Vector2(i,13.5f);
		}
		else 
		{
			closeButton.enabled = true;

			running = true;
			Cursor.visible = true;
			open = false;
		}

	}

	public void CloseBook()
	{


		i -= openSpeed * Time.deltaTime;

		if (i > 0)
		{
			Cursor.visible = false;
			close = true;
			leftPage.rectTransform.localScale = new Vector2(i,13.5f);
			rightPage.rectTransform.localScale = new Vector2(i,13.5f);
		}
		else 
		{
			Cursor.visible = true;
			close = false;
			gameObject.SetActive(false);
		}

	}

	public void MoveLeft()
	{
		
		page -= 1;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		running = true;
		StartCoroutine(Waiting());

	}

	public void MoveRight()
	{

		page += 1;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		running = true;
		StartCoroutine(Waiting());

	}
	IEnumerator Waiting()
	{
		while (running == true)
		{
			yield return null;
		}
		canv1.texture = bookPages[page].fade1 as Texture;
		canv2.texture = bookPages[page].fade2 as Texture;
		canv3.texture = bookPages[page].fade3 as Texture;
		canv4.texture = bookPages[page].text as Texture;
		running = true;

		while (running == true)
		{
			yield return null;
		}
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		if (page == 0)
		{
			goLeft.interactable = false;
		}
		else 
		{
			goLeft.interactable = true;
		}

		if (page == bookPages.Count)
		{
			goRight.interactable = false;
		}
		else
		{
			goRight.interactable = true;
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
			a = 1;
			break;
		case 3: 
			c += e;
			b = 1;
			break;
		case 4: 
			d += e;
			c = 1;
			break;
		case 5: 
			a -= e;
			d = 1;
			break;
		case 6: 
			b -= e;
			a = 0;
			break;
		case 7: 
			c -= e;
			b = 0;
			break;
		case 8:
			g = 7;
			c = 0;
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
			a = 0;
			running = !running;
			break;
		case 1: 
			a -= e;
			b = 0;
			break;
		case 2: 
			b -= e;
			c = 0;
			break;
		case 3: 
			c -= e;
			d = 0;
			break;
		case 4: 
			d -= e;
			a = 1;
			break;
		case 5: 
			a += e;
			b = 1;
			break;
		case 6: 
			b += e;
			c = 1;
			break;
		case 7: 
			c += e;
			break;
		}
	}

}
