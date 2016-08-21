using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StartScreen : MonoBehaviour 
{
	public RawImage fade2, fade3, image;

	public enum imageNo {two, three, four, none};

	public imageNo currentImage;

	public float t,  b, c, d;

	void Update()
	{
		Fading();

	}

	//fading buttons using alpha
	void Fading()
	{
		if (currentImage == imageNo.two)
		{
			b += t * Time.deltaTime; 

			fade2.color = new Color(1, 1, 1, b); 
		}

		else if (currentImage == imageNo.three)
		{
			c += t * Time.deltaTime; 

			fade3.color = new Color(1, 1, 1, c); 
		}

		else if (currentImage == imageNo.four)
		{
			d += t * Time.deltaTime; 

			image.color = new Color(1, 1, 1, d); 
		}
	}

	//fades button text out
	IEnumerator FadeOut()
	{
		fade2.enabled = true;
		fade3.enabled = true;

		t = -10;
		currentImage = imageNo.four;
		yield return new WaitWhile(() => d > 0);

		currentImage = imageNo.three;
		yield return new WaitWhile(() => c > 0);

		currentImage = imageNo.two;
		yield return new WaitWhile(() => b > 0);


		currentImage = imageNo.none;
	}

	//fades button text in
	IEnumerator FadeIn()
	{	
		t = 10;

		currentImage = imageNo.two;
		yield return new WaitWhile(() => b < 1);

		currentImage = imageNo.three;
		yield return new WaitWhile(() => c < 1);

		currentImage = imageNo.four;
		yield return new WaitWhile(() => d < 1);

	
		fade2.enabled = false;
		fade3.enabled = false;

		currentImage = imageNo.none;
	}

	//public functions called when start menyu buttons are moused over
	public void EnterButton()
	{
		StopAllCoroutines();
		StartCoroutine(FadeIn());
	}

	public void ExitButton()
	{
		StopAllCoroutines();
		StartCoroutine(FadeOut());
	}

}
