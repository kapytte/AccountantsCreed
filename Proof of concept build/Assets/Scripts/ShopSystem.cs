f
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class townPrice
{
	public string townName;
	public float fishPrice, lumberPrice, ironPrice, wheatPrice; 
	public Text fishPriceT, lumberPriceT, ironPriceT, wheatPriceT;
}


public class ShopSystem : MonoBehaviour 
{
	public List<townPrice> outpostList;
	public List<GameObject> towns = new List<GameObject>(); 

	public GameObject wheat, lumber, fish, iron;

	public RawImage market, outpost, mercenaries;

	public Text mercCostT, weaponCost, uFish, uLumber, UIron, uGold, priceF, priceL, priceI, priceG;
	 
	public Button barracks, stalls, outpostB, buyMercs, exit;
	public Button bL, bF, bI, bG, sL, sF, sI, sG;

	public float fishPrice, lumberPrice, ironPrice, wheatPrice;
	public bool hasMercs;
	public int g, def, mercCost;

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

		StartCoroutine(townSpawn());

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
				outpost.isActiveAndEnabled == true || 
				questGiver.GetComponent<MultipleChoice>().questActive)
			{
				barracks.gameObject.SetActive(false);
				stalls.gameObject.SetActive(false);
				outpostB.gameObject.SetActive(false);
				exit.interactable = false;
			}
			else
			{
				barracks.gameObject.SetActive(true);
				stalls.gameObject.SetActive(true);
				outpostB.gameObject.SetActive(true);
				exit.interactable = true;
			}

			if (market.isActiveAndEnabled)
			{
				ButtonEnabled();
			}

			if(mercenaries.isActiveAndEnabled)
			{
				if (hasMercs)
				{
					mercCostT.text = "Already Purchaced";
					buyMercs.interactable = false;
				}
				else
				{
					MercCost();
				}
			} 
		}




		if (Input.GetMouseButtonDown(0))
		{
			for(int i = 0; i < towns.Count; i++)
			{
				

			}
			Outpost();
		}

	}

	IEnumerator townSpawn()
	{
		yield return new WaitForSeconds(2f);
		Outpost();
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

	void Outpost()
	{
		for (int i = 0; i < outpostList.Count; i++)
		{
			outpostList[i].townName = towns[i].name + " " + i;

			outpostList[i].wheatPrice = (200 / towns[i].GetComponentInChildren<Town>().wheat);
			outpostList[i].wheatPriceT.text = Mathf.CeilToInt(outpostList[i].wheatPrice).ToString();

			outpostList[i].fishPrice = (150 / towns[i].GetComponentInChildren<Town>().fish) * 2;
			outpostList[i].fishPriceT.text =Mathf.CeilToInt(outpostList[i].fishPrice).ToString();

			outpostList[i].lumberPrice = (100 / towns[i].GetComponentInChildren<Town>().lumber) * 3;
			outpostList[i].lumberPriceT.text = Mathf.CeilToInt(outpostList[i].lumberPrice).ToString();

			outpostList[i].ironPrice = (50 / towns[i].GetComponentInChildren<Town>().iron) * 4;
			outpostList[i].ironPriceT.text = Mathf.CeilToInt(outpostList[i].ironPrice).ToString();
		}
	}


	public void BuyMercs()
	{
		def = 20;
		caravan.GetComponent<Caravan>().def = def;
		caravan.GetComponent<Caravan>().defT.text = def.ToString();

		questGiver.GetComponent<MultipleChoice>().goldN -= mercCost;
		hasMercs = true;
	}

	void MercCost()
	{
		mercCost = cargo.Count * 2 + 3;
		mercCostT.text = mercCost.ToString();

		if (questGiver.GetComponent<MultipleChoice>().goldN > mercCost && cargo.Count > 0)
		{
			buyMercs.interactable = true;
		}
		else
		{
			buyMercs.interactable = false;
		}
	}



	void Price()
	{
		fishPrice = (150 / town.GetComponent<Town>().fish) * 2;
		lumberPrice = (100 / town.GetComponent<Town>().lumber) * 3;
		ironPrice = (50 / town.GetComponent<Town>().iron) * 4;
		wheatPrice = (200 / town.GetComponent<Town>().wheat);

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
		MercsLeave();
	}
	public void BuyFish()
	{
		town.GetComponent<Town>().fish -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (fishPrice);
		cargo.Add(fish);
		CargoTextAdd ();
		MercsLeave();
	}
	public void BuyIron()
	{
		town.GetComponent<Town>().iron -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (ironPrice);
		cargo.Add(iron);
		CargoTextAdd ();
		MercsLeave();
	}
	public void BuyGold()
	{
		town.GetComponent<Town>().wheat -= 1;
		questGiver.GetComponent<MultipleChoice>().goldN -= Mathf.CeilToInt (wheatPrice);
		cargo.Add(wheat);
		CargoTextAdd ();
		MercsLeave();
	}

	public void SellLumber()
	{
		cargo.Remove (lumber);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (lumberPrice);
		town.GetComponent<Town> ().lumber += 1;
		CargoTextAdd ();
		MercsLeave();
		
	}
	public void SellFish()
	{
		cargo.Remove (fish);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (fishPrice);
		town.GetComponent<Town> ().fish += 1;
		CargoTextAdd ();
		MercsLeave();
	}
	public void SellIron()
	{
		cargo.Remove (iron);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (ironPrice);
		town.GetComponent<Town> ().iron += 1;
		CargoTextAdd ();
		MercsLeave();
	}

	public void SellGold()
	{
		cargo.Remove (wheat);
		questGiver.GetComponent<MultipleChoice> ().goldN += Mathf.CeilToInt (wheatPrice);
		town.GetComponent<Town> ().wheat += 1;
		CargoTextAdd ();
		MercsLeave();
	}

	public void MercsLeave()
	{
		def = 10;
		caravan.GetComponent<Caravan>().def = def;
		caravan.GetComponent<Caravan>().defT.text = def.ToString();


		hasMercs = false;
		MercCost();
	}
}



