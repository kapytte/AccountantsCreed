using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//metric behavor
public class Metrics : MonoBehaviour 
{

	public List <AudioClip> tunes = new List<AudioClip>();
	public AudioSource speakers;
	
	public int goldPos, goldNeg, goldCurrent, days, buyGoods, sellGoods, treasure, hazards, banditsPos, banditsNeg, questionsRight, questionsWrong, repeats; 
	public string gameState;

	public float time, i, t, a, fadeDuration;

	public bool starting;

	public RawImage fade;

	public GameObject choiceSystem;

	//prevents destruction on load and fades through intro scene
	void Start()
	{
		DontDestroyOnLoad(gameObject);

//		if (SceneManager.GetActiveScene().buildIndex == 0)
//		{
			StartCoroutine(WaitWhileFading());
			starting = true;
//		}


	}


	//keeps track of time and fades through intro scene
	void Update()
	{
		if (fade == null)
		{
			fade = GameObject.Find ("Fade").GetComponentInChildren<RawImage> ();
		}

		a = Time.realtimeSinceStartup - time;

		if (starting)
		{
			FadeToBlack();
		}

		if (a >= 300)
		{
			TimeStats();
			time = Time.realtimeSinceStartup;
		}
	}

	//starts new game
	public void PreviewScreen()
	{
		SceneManager.LoadScene(1);
	}
		
	//finds quest giver when loaded
	void OnLevelWasLoaded()
	{
		choiceSystem = GameObject.Find("QuestGiver");
	}

	//gets game stats on quit
	void OnApplicationQuit()
	{

		GameStats();


	}

	IEnumerator music()
	{
		while (true)
		{
			yield return new WaitWhile(() => speakers.isPlaying == true);

			int i = Random.Range(0, tunes.Count);

			speakers.PlayOneShot(tunes[i]);
		}
			
	}

	//controls fading image
	void FadeToBlack()
	{
		i += t * Time.deltaTime;

		fade.color = new Color(0, 0, 0, i);
	}

	//auto-fading function
	IEnumerator WaitWhileFading()
	{
		yield return new WaitWhile(() => i > - fadeDuration);	
		t = -t;

		yield return new WaitWhile(() => i < fadeDuration);
		t = -t;

		starting = false;

		StartCoroutine (music());

		SceneManager.LoadScene(3);
	}

	//gets stats every 5 minutes in case of web build
	void TimeStats()
	{
		Analytics.CustomEvent("5 Min Update", new Dictionary<string, object>
			{
				{"Questions Correct", questionsRight},
				{"Questions Wrong", questionsWrong},
				{"Gold Gained", goldPos},
				{"Gold Lost", goldNeg}, 
				{"Days passed", days},
				{"Cargo Bought", buyGoods},
				{"Cargo Sold", sellGoods},
				{"Playtime", Time.realtimeSinceStartup},
			});

		TreasureStats();
	}

	//gets more stats every 5 min
	void TreasureStats()
	{Analytics.CustomEvent("5 Min Update Cont'", new Dictionary<string, object>
		{
			{"Treasure Found", treasure},
			{"Hazards", hazards},
			{"Ambush Won", banditsPos},
			{"Ambush Lost", banditsNeg}
		});
		
	}

	//gets game-specific stats
	void GameStats()
	{
		Analytics.CustomEvent("Game Stats", new Dictionary<string, object>
			{
				{"Questions Correct", questionsRight},
				{"Questions Wrong", questionsWrong},
				{"PlaySessions", repeats},
				{"Gold", choiceSystem.GetComponent<MultipleChoice>().goldN},
				{"Playtime", Time.realtimeSinceStartup},
			});
		gameState = "Quit";

		LevelStats();
	}

	//gets level-specific stats 
	public void LevelStats()
	{
		Analytics.CustomEvent("Level Stats", new Dictionary<string, object>
			{
				{"Game State", gameState},
				{"Gold Gained", goldPos},
				{"Gold Lost", goldNeg}, 
				{"Days passed", days},
				{"Cargo Bought", buyGoods},
				{"Cargo Sold", sellGoods},
				{"Treasure Found", treasure},
				{"Hazards", hazards},
				{"Ambush Won", banditsPos},
				{"Ambush Lost", banditsNeg}
			});

		ClearStats();
	}

	//clears level stats at the end of level
	void ClearStats()
	{
		gameState = "";
		goldPos = 0;
		goldNeg = 0;
		days = 0;
		buyGoods = 0;
		sellGoods = 0;
		treasure = 0;
		hazards = 0;
		banditsPos = 0;
		banditsNeg = 0;

	}
}
