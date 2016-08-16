using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;



public class Metrics : MonoBehaviour 
{
	public int goldPos, goldNeg, goldCurrent, days, buyGoods, sellGoods, treasure, hazards, banditsPos, banditsNeg, questionsRight, questionsWrong, repeats; 
	public string gameState;

	public GameObject choiceSystem;
	void Start()
	{
		DontDestroyOnLoad(gameObject);

	}

	public void PreviewScreen()
	{
		SceneManager.LoadScene(1);
	}
		
	void OnLevelWasLoaded()
	{
		choiceSystem = GameObject.Find("QuestGiver");
	}


	void OnApplicationQuit()
	{

		GameStats();


	}


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
