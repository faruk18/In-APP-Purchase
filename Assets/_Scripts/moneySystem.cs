using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneySystem : MonoBehaviour {

	/// <summary>
	/// The mainmoney.
	/// </summary>
	public static int Mainmoney;
    public GameObject Ads;
	public Text MoneyText;
	// Use this for initialization
	void Start () {
		//for money value add and save
		Mainmoney = PlayerPrefs.GetInt ("money");
		MoneyText.text ="$"+Mainmoney.ToString ();
/*
        if(PlayerPrefs.GetInt("ads")==1)
            {
			Ads.SetActive(false);
            }
        else
            {
			Ads.SetActive(false);
             }
         */
	}
	
	public void AddMoney(int Value)
	{
		Mainmoney += Value;
		PlayerPrefs.SetInt ("money", Mainmoney);

		MoneyText.text ="$"+Mainmoney.ToString ();
	}
}
