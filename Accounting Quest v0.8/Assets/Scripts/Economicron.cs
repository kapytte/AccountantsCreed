﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//bank of pages in econonomicron
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

	public enum bookStage {fadeIn, fadeOut, flipBook, fadeText, none}
	public bookStage bookSequence;

	public Light bookLight; 
	public Camera mainCam, bookCam;
	public Button goLeft, goRight, closeButton;
	public RawImage canv1, canv2, canv3, canv4, fadeScreen1, fadeScreen2;
	public GameObject content, bookHinge, mainCanvas;
	public float a, b, c, d, e, g, i, openSpeed, fadeSpeed;
	public int h, currentPage, page;
	public bool one, running, open, close, closing;
	public Quaternion pageOpenPos, pageClosedPos;


	public float speed;

	//sets page 1 of book and start transform of pages
	void Start()
	{
		bookLight.enabled = false;

		pageOpenPos = new Quaternion(-0.4f, -0.5f, -0.5f, 0.4f);
		pageClosedPos = new Quaternion(-0.6f, 0.4f, 0.5f, 0.6f);

		bookHinge.transform.rotation = pageClosedPos;

		currentPage = 0;

		page = bookPages.Count - 1;

		canv1.texture = bookPages[currentPage].fade1;
		canv2.texture = bookPages[currentPage].fade2;
		canv3.texture = bookPages[currentPage].fade3;
		canv4.texture = bookPages[currentPage].text;

		gameObject.SetActive(false);


	}

	//constantly updates apha of pages, and controls page-truning and book open/close
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
			OpeningProcedure();
		}

		if (close)
		{
			ClosingProcedure();
		}

	}



	//opens the book by expanding the pages and fading in page 1
	public void OpenBook()
	{
		i = 0;
		StartCoroutine (OpeningBookControl());

	}

	void OpeningProcedure()
	{
		

		if (bookSequence == bookStage.fadeOut)
		{
			i += fadeSpeed * Time.deltaTime;
			fadeScreen1.color = new Color(0,0,0,i);
		}

		else if (bookSequence == bookStage.fadeIn)
		{
			i -= fadeSpeed * Time.deltaTime;
			fadeScreen2.color = new Color(0,0,0,i);
		}

		else if (bookSequence == bookStage.flipBook)
		{
			i += openSpeed * Time.deltaTime;
			bookHinge.transform.rotation = Quaternion.Lerp(pageClosedPos, pageOpenPos, i);
		}

	}

	IEnumerator OpeningBookControl()
	{
		goLeft.interactable = false;
		goRight.interactable = true;

		//fade out main screen
		Cursor.visible = false;
		open = true;
		bookSequence = bookStage.fadeOut;
		yield return new WaitWhile(() => i < 1);
		bookLight.enabled = true;
		//posess book camrera & fade in book screen
		bookCam.enabled = true;
		mainCam.enabled = false;
		mainCanvas.SetActive(false);
		bookSequence = bookStage.fadeIn;
		yield return new WaitWhile(() => i > 0);
		//open book
		bookSequence = bookStage.flipBook;
		yield return new WaitForSeconds(1);
		open = false;
		bookSequence = bookStage.none;
		//fade in text
		canv1.texture = bookPages[currentPage].fade1;
		canv2.texture = bookPages[currentPage].fade2;
		canv3.texture = bookPages[currentPage].fade3;
		canv4.texture = bookPages[currentPage].text;


		//end
		closeButton.enabled = true;

		running = true;
		yield return new WaitWhile(() => running == true);

		Cursor.visible = true;
		open = false;
	}

	void ClosingProcedure()
	{


		if (bookSequence == bookStage.fadeOut)
		{
			i += fadeSpeed * Time.deltaTime;
			fadeScreen2.color = new Color(0,0,0,i);
		}

		else if (bookSequence == bookStage.fadeIn)
		{
			i -= fadeSpeed * Time.deltaTime;
			fadeScreen1.color = new Color(0,0,0,i);
		}

		else if (bookSequence == bookStage.flipBook)
		{
			i += openSpeed * Time.deltaTime;
			bookHinge.transform.rotation = Quaternion.Lerp(pageOpenPos, pageClosedPos, i);
		}

	}

	IEnumerator ClosingBookControl()
	{
		i = 0;
		//fade out main screen
		Cursor.visible = false;
		running = true;

		yield return new WaitWhile(() => running == true);

		close = true;
		bookSequence = bookStage.flipBook;
	
		yield return new WaitForSeconds(1);
		bookSequence = bookStage.fadeOut;

		yield return new WaitWhile(() => i < 1);
		//posess book camrera & fade in book screen

		bookLight.enabled = false;
		bookCam.enabled = false;
		mainCam.enabled = true;
		mainCanvas.SetActive(true);

		bookSequence = bookStage.fadeIn;
		yield return new WaitWhile(() => i > 0);
		//open book
		
		close = false;
		bookSequence = bookStage.none;
		currentPage = 0;

		Cursor.visible = true;
	}

	//closes the book by fading out the page and redcing page size
	void CloseBook()
	{


		i -= openSpeed * Time.deltaTime;

		if (i > 0)
		{

			Cursor.visible = false;
			close = true;


//			leftPage.rectTransform.localScale = new Vector2(i,13.5f);
//			rightPage.rectTransform.localScale = new Vector2(i,13.5f);
		}
		else 
		{
			Cursor.visible = true;
			close = false;
			gameObject.SetActive(false);
			currentPage = 0;

		}

	}


	//moves pages left and right by calling from page list
	public void MoveLeft()
	{
		
		currentPage -= 1;
		running = true;
		StartCoroutine(Waiting());

	}
	public void MoveRight()
	{

		currentPage += 1;
		running = true;
		StartCoroutine(Waiting());

	}

	//waits till page has faded, then closes the book
	public void BeginClose()
	{
		StartCoroutine(ClosingBookControl());

	}

	//waits for page to fade in & out when turning pages
	IEnumerator Waiting()
	{
		goLeft.interactable = false;
		goRight.interactable = false;
		closeButton.interactable = false;

		yield return new WaitWhile(() => running == true);
		Cursor.visible = false;

		canv1.texture = bookPages[currentPage].fade1 as Texture;
		canv2.texture = bookPages[currentPage].fade2 as Texture;
		canv3.texture = bookPages[currentPage].fade3 as Texture;
		canv4.texture = bookPages[currentPage].text as Texture;
		running = true;

		StartCoroutine (KeepWaiting());
	}
	IEnumerator KeepWaiting()
	{
		yield return new WaitWhile(() => running == true);

		Cursor.visible = true;

		if (currentPage == 0)
		{
			goLeft.interactable = false;
		}
		else 
		{
			goLeft.interactable = true;
		}

		if (currentPage == page)
		{
			goRight.interactable = false;
		}
		else
		{
			goRight.interactable = true;
		}

		closeButton.interactable = true;
	}

	//uses simple timer function to adjust page alphas over time
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
			one = false;
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
			one = true;
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
