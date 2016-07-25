
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopSystem : MonoBehaviour 
{
	public RawImage market, blackSmith, mercenaries;

	public Text mercCost, weaponCost, uFish, uLumber, UIron, uGold, priceF, priceL, priceI, priceG, inv;

	public Button barracks, stalls, smith;
	public Button bL, bF, bI, bG, sL, sF, sI, sG;

	public float fishPrice, lumberPrice, ironPrice, goldPrice;

	public int g;

	public GameObject town, caravan, questGiver;

	public List <GameObject> cargo = new List<GameObject>(); 

	void Start()
	{
		bL.interactable = false;
		bF.interactable = false;
		bG.interactable = false;
		bI.interactable = false;
		sL.interactable = false;
		sF.interactable = false;
		sG.interactable = false;
		sI.interactable = false;
	}

	void Update()
	{
		if (town != null && market)
		{ 
			Price();


		}

		foreach(GameObject c in cargo)
		{
			inv.text = c.name;
		}


		g = cargo.Count;

		if (market.isActiveAndEnabled == true || mercenaries.isActiveAndEnabled == true || blackSmith.isActiveAndEnabled == true || questGiver.GetComponent<MultipleChoice>().questActive)
		{
			barracks.gameObject.SetActive(false);
			stalls.gameObject.SetActive(false);
			smith.gameObject.SetActive(false);
		}
		else
		{
			barracks.gameObject.SetActive(true);
			stalls.gameObject.SetActive(true);
			smith.gameObject.SetActive(true);
		}

		if (market.isActiveAndEnabled == true)
		{
			ButtonEnabled();
		}


	}




	void ButtonEnabled()
	{
		if (cargo.Count == 0)
		{
			bL.interactable = false;
			bF.interactable = false;
			bI.interactable = false;
			bG.interactable = false;
		}

		if (cargo.Count >= 0)
		{
			foreach (GameObject c in cargo)
			{
				print("hey");
				if (c.name == "Lumber")
					sL.interactable = true;
				else
					sL.interactable = false;

				if (c.name == "Fish")
					sF.interactable = true;
				else
					sF.interactable = false;

				if (c.name == "Iron")
					sI.interactable = true;
				else
					sI.interactable = false;

				if (c.name == "Gold")
					sG.interactable = true;
				else
					sG.interactable = false;
			}
		}



		if (town.GetComponent<Town>().lumber > 0 && cargo.Count < 10 && questGiver.GetComponent<MultipleChoice>().goldN >= Mathf.CeilToInt (lumberPrice))
			bL.interactable = true;
		else
			bL.interactable = false;

		if (town.GetComponent<Town>().fish > 0 && cargo.Count < 10 && questGiver.GetComponent<MultipleChoice>().goldN >= Mathf.CeilToInt (fishPrice))
			bF.interactable = true;
		else
			bF.interactable = false;

		if (town.GetComponent<Town>().iron > 0 && cargo.Count < 10 && questGiver.GetComponent<MultipleChoice>().goldN >= Mathf.CeilToInt (ironPrice))
			bI.interactable = true;
		else
			bI.interactable = false;

		if (town.GetComponent<Town>().wheat > 0 && cargo.Count < 10 && questGiver.GetComponent<MultipleChoice>().goldN >= Mathf.CeilToInt (goldPrice))
			bG.interactable = true;
		else
			bG.interactable = false;


	}



	void Price()
	{
		fishPrice = fishPrice = town.GetComponent<Town>().fish / 150 * 10;
		lumberPrice = town.GetComponent<Town>().lumber / 400 * 5;
		ironPrice = (town.GetComponent<Town>().iron / 200) * 15;
		goldPrice = (town.GetComponent<Town>().wheat / 50) * 25;

		uFish.text =  town.GetComponent<Town>().fish.ToString();
		uLumber.text = town.GetComponent<Town>().lumber.ToString();
		UIron.text = town.GetComponent<Town>().iron.ToString();
		uGold.text = town.GetComponent<Town>().wheat.ToString();

		priceF.text = Mathf.CeilToInt (fishPrice).ToString();
		priceL.text = Mathf.CeilToInt (lumberPrice).ToString();
		priceI.text = Mathf.CeilToInt (ironPrice).ToString();
		priceG.text = Mathf.CeilToInt (goldPrice).ToString();
	}

	public void BuyLumber()
	{
		town.GetComponent<Town>().lumber -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (lumberPrice);
		cargo.Add(new GameObject ("Lumber"));
	}
	public void BuyFish()
	{
		town.GetComponent<Town>().fish -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (fishPrice);
		cargo.Add(new GameObject ("Fish"));
	}
	public void BuyIron()
	{
		town.GetComponent<Town>().iron -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (ironPrice);
		cargo.Add(new GameObject ("Iron"));
	}
	public void BuyGold()
	{
		town.GetComponent<Town>().wheat -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (goldPrice);
		cargo.Add(new GameObject ("Gold"));
	}

	public void SellLumber()
	{
		town.GetComponent<Town>().lumber += 1;
		questGiver.GetComponent<MultipleChoice>().goldN += Mathf.CeilToInt (lumberPrice);
		foreach(GameObject c in cargo)
		{
			if (c.name == "Lumber")
			{
				cargo.Remove(c);
				break;
			}
		}
		
	}
	public void SellFish()
	{
		town.GetComponent<Town>().fish += 1;
		questGiver.GetComponent<MultipleChoice>().goldN += Mathf.CeilToInt (fishPrice);
		foreach(GameObject c in cargo)
		{
			if (c.name == "Fish")
			{
				cargo.Remove(c);
				break;
			}
		}
	}
	public void SellIron()
	{
		town.GetComponent<Town>().iron += 1;
		questGiver.GetComponent<MultipleChoice>().goldN += Mathf.CeilToInt (ironPrice);
		foreach(GameObject c in cargo)
		{
			if (c.name == "Iron")
			{
				cargo.Remove(c);
				break;
			}
		}
	}
	public void SellGold()
	{
		town.GetComponent<Town>().wheat += 1;
		questGiver.GetComponent<MultipleChoice>().goldN += Mathf.CeilToInt (goldPrice);
		foreach(GameObject c in cargo)
		{
			if (c.name == "Gold")
			{
				cargo.Remove(c);
				break;
			}
		}
	}


}
