using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Save : MonoBehaviour 
{
	public GameObject caravan, choice, shops, time;

	public void NewGame()
	{
		SceneManager.LoadScene (0);
	}


	public void SaveGame()
	{
		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetInt ("PlayerGold", choice.GetComponent<MultipleChoice> ().goldN);
		PlayerPrefs.SetInt ("PlayerRep", choice.GetComponent<MultipleChoice> ().repN);

		PlayerPrefs.SetFloat ("PlayerPosX", caravan.GetComponent<Caravan> ().transform.position.x);
		PlayerPrefs.SetFloat ("PlayerPosY", caravan.GetComponent<Caravan> ().transform.position.y);
		PlayerPrefs.SetFloat ("PlayerPosZ", caravan.GetComponent<Caravan> ().transform.position.z);

		for (int i = 0; i < shops.GetComponent<ShopSystem> ().cargo.Count; i++)
		{
			
			string cargo = shops.GetComponent<ShopSystem> ().cargo [i].name;
			string cargoPos = "CargoPos" + i.ToString ();
			PlayerPrefs.SetString (cargoPos, cargo);
			Debug.Log (cargoPos + " " + cargo);
		}
	}

	public void Tutorial()
	{
		SceneManager.LoadScene(2);
	}

	public void LoadGame()
	{
		choice.GetComponent<MultipleChoice> ().goldN = PlayerPrefs.GetInt ("PlayerGold");
		choice.GetComponent<MultipleChoice> ().repN = PlayerPrefs.GetInt ("PlayerRep");

		caravan.GetComponent<Transform> ().position = new Vector3(PlayerPrefs.GetFloat ("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));

		for (int i = 0; i < 10; i++)
		{
			string cargoPos = "CargoPos" + i.ToString ();
			string cargo = PlayerPrefs.GetString (cargoPos);
			Debug.Log (cargoPos + " " + cargo);

			if (cargo == "Wheat")
			{
				shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem> ().wheat);
			}

			if (cargo == "Fish")
			{
				shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem> ().fish);
			}

			if (cargo == "Lumber")
			{
				shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem> ().lumber);
			}
		
			if (cargo == "Iron")
			{
				shops.GetComponent<ShopSystem> ().cargo.Add (shops.GetComponent<ShopSystem> ().iron);
			}
		}
		shops.GetComponent<ShopSystem> ().CargoTextAdd ();

	}

	public void Quit()
	{
		Application.Quit();
	}
}
