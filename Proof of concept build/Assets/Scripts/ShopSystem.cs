﻿
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopSystem : MonoBehaviour 
{
	public GameObject wheat, lumber, fish, iron;

	public RawImage market, blackSmith, mercenaries;

	public Text mercCost, weaponCost, uFish, uLumber, UIron, uGold, priceF, priceL, priceI, priceG;

	public Button barracks, stalls, smith;
	public Button bL, bF, bI, bG, sL, sF, sI, sG;

	public float fishPrice, lumberPrice, ironPrice, wheatPrice;

	public int g;

	public GameObject town, caravan, questGiver;

	public List <GameObject> cargo = new List<GameObject>(); 
	public List <Text> inv = new List<Text>(); 

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
		if (town != null)
		{
			if (market)
			{ 
				Price();
			}

			g = cargo.Count;

			if (market.isActiveAndEnabled == true || 
				mercenaries.isActiveAndEnabled == true || 
				blackSmith.isActiveAndEnabled == true || 
				questGiver.GetComponent<MultipleChoice>().questActive)
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

	}

	public void CargoTextAdd()
	{
		int i = 0;

		while (i < inv.Count)
		{
			if (i < cargo.Count)
			{
				inv[i].text= cargo[i].name[0].ToString();
				i++;
			}
			else
			{
				inv[i].text = null;
				i++;
			}
		}	
	}

	void ButtonEnabled()
	{
//		print (cargo.Count);
		if (cargo.Count == 0)
		{
			sL.interactable = false;
			sF.interactable = false;
			sI.interactable = false;
			sG.interactable = false;
		}

		if (cargo.Count > 0)
		{
			
			if (cargo.Contains (lumber))
				sL.interactable = true;
			else
				sL.interactable = false;

			if (cargo.Contains(fish))
				sF.interactable = true;
			else
				sF.interactable = false;

			if (cargo.Contains(iron))
				sI.interactable = true;
			else
				sI.interactable = false;
			
			if (cargo.Contains(wheat))
				sG.interactable = true;
			else
				sG.interactable = false;
			
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

		if (town.GetComponent<Town>().wheat > 0 && cargo.Count < 10 && questGiver.GetComponent<MultipleChoice>().goldN >= Mathf.CeilToInt (wheatPrice))
			bG.interactable = true;
		else
			bG.interactable = false;
	}



	void Price()
	{
		fishPrice = fishPrice = (town.GetComponent<Town>().fish / 300) * 2;
		lumberPrice = town.GetComponent<Town>().lumber / 200 * 1;
		ironPrice = (town.GetComponent<Town>().iron / 100) * 4;
		wheatPrice = (town.GetComponent<Town>().wheat / 400) * 3;

		uFish.text =  town.GetComponent<Town>().fish.ToString();
		uLumber.text = town.GetComponent<Town>().lumber.ToString();
		UIron.text = town.GetComponent<Town>().iron.ToString();
		uGold.text = town.GetComponent<Town>().wheat.ToString();

		priceF.text = Mathf.CeilToInt (fishPrice).ToString();
		priceL.text = Mathf.CeilToInt (lumberPrice).ToString();
		priceI.text = Mathf.CeilToInt (ironPrice).ToString();
		priceG.text = Mathf.CeilToInt (wheatPrice).ToString();
	}

	public void BuyLumber()
	{
		town.GetComponent<Town>().lumber -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (lumberPrice);
		cargo.Add(lumber);
		CargoTextAdd ();
	}
	public void BuyFish()
	{
		town.GetComponent<Town>().fish -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (fishPrice);
		cargo.Add(fish);
		CargoTextAdd ();
	}
	public void BuyIron()
	{
		town.GetComponent<Town>().iron -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (ironPrice);
		cargo.Add(iron);
		CargoTextAdd ();
	}
	public void BuyGold()
	{
		town.GetComponent<Town>().wheat -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (wheatPrice);
		cargo.Add(wheat);
		CargoTextAdd ();
	}

	public void SellLumber()
	{
		cargo.Remove (lumber);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (lumberPrice);
		town.GetComponent<Town> ().lumber += 1;
		CargoTextAdd ();
		
	}
	public void SellFish()
	{
		cargo.Remove (fish);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (fishPrice);
		town.GetComponent<Town> ().fish += 1;
		CargoTextAdd ();
	}
	public void SellIron()
	{
		cargo.Remove (iron);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (ironPrice);
		town.GetComponent<Town> ().iron += 1;
		CargoTextAdd ();
	}

	public void SellGold()
	{
		cargo.Remove (wheat);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (wheatPrice);
		town.GetComponent<Town> ().wheat += 1;
		CargoTextAdd ();

	}
}



