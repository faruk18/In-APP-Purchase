using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class Purchaser : MonoBehaviour,IStoreListener {


    IStoreController controller;
    // for three product add money paid
    string[] Products = new string[]{"coin_100","coin_500","remove_ads"};

	void Start()
	{// Create a builder, first passing in a suite of Unity provided stores.
		var Module = StandardPurchasingModule.Instance ();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(Module);

       foreach(string s in Products)
            {

			if(s.Contains("ads"))
                {
                 builder.AddProduct(s,ProductType.NonConsumable);
                }
            else
                {
                 builder.AddProduct(s,ProductType.Consumable);
                }
           

            }


        UnityPurchasing.Initialize(this,builder);

	}

	public void BuyProduct(string ProductId)
        {

		if (ProductId.Contains ("ads"))
		{
			Product product0 = controller.products.WithID (ProductId);

			if(product0.hasReceipt)
				{

                GetComponent<moneySystem>().Ads.SetActive(false);
                }

            else
                {
                 buyProduct(ProductId);
                }
		}
        else
           {
              buyProduct(ProductId);
           }
        
       }
    

    void buyProduct(string ProductId)
        {

        Product product = controller.products.WithID(ProductId);


        if(product != null && product.availableToPurchase)
            {
            Debug.Log("On a purchase");

        controller.InitiatePurchase(product);
            }

        else
            {
            Debug.Log("On a purchase in product");
            }
       }
	public void OnInitialized(IStoreController controller, IExtensionProvider provider)
	{

        this.controller =controller;
		Debug.Log ("System Open");
        Product product0 = controller.products.WithID ("remove_ads");

			if(product0.hasReceipt)
				{

                GetComponent<moneySystem>().Ads.SetActive(false);
                }

            else
                {
			
			GetComponent<moneySystem>().Ads.SetActive(true);
                }
	}

  public void OnInitializeFailed(InitializationFailureReason error)
	{



    // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
     Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{

		if (String.Equals (args.purchasedProduct.definition.id, Products [0], StringComparison.Ordinal))
        {
            //If the consumable item has been successfully purchased, add 100 coins 
            GetComponent<moneySystem>().AddMoney(100);

		}

        else if(String.Equals (args.purchasedProduct.definition.id, Products [1], StringComparison.Ordinal))
            {
             //If the consumable item has been successfully purchased, add 500 coins 
            GetComponent<moneySystem>().AddMoney(500);
            }

        else if(String.Equals (args.purchasedProduct.definition.id, Products [2], StringComparison.Ordinal))
            {
			GetComponent<moneySystem> ().Ads.SetActive (false);
			//PlayerPrefs.SetInt ("ads", 1);

            Debug.Log("Remove Ads");
            }
		return PurchaseProcessingResult.Complete;
	}
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",product.definition.storeSpecificId, failureReason));
		//Debug.Log ("product purchase:"+product.ToString());

	}
}
