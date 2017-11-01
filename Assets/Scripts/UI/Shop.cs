using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
//using UnityEngine.Purchasing;
using System;

public class Shop : MonoBehaviour/*, IStoreListener*/ {

	public Image shopPanel;
	public Text coinsCount;
	public Button bootsAD;
    public Button coinsAD;

    //public static string kProductIDConsumable = "consumable";

    //static IStoreController storeController;
    //static IExtensionProvider storeExtentionProvider;

    void Start () {
        //if (storeController == null) {
        //    InitializePurchasing ();
        //}
    }

    void UpdateCoins () {
        coinsCount.text = "Coins: " + DataPreservation.data._coins;
    }

    public void PurchaseBootsWithCoins (int numberOfBoots)
    {
        int cost = 75;
        if (numberOfBoots > 1)
        {
            cost *= numberOfBoots;
            cost -= 50;
        }
        DataPreservation.data._boots += numberOfBoots;
        DataPreservation.data._coins -= cost;
        DataPreservation.data.SaveData();
        UpdateCoins();
    }

    #region IAP Code
    //public void InitializePurchasing () {
    //    if (IsInitialized ()) {
    //        return;
    //    }

    //    ConfigurationBuilder builder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance ());

    //    builder.AddProduct (kProductIDConsumable, ProductType.Consumable);

    //    UnityPurchasing.Initialize (this, builder);
    //}

    //bool IsInitialized () {
    //    return storeController != null && storeExtentionProvider != null;
    //}

    //public void BuyConsumable () {
    //    BuyProductID (kProductIDConsumable);
    //}

    //void BuyProductID (string productId) {
    //    if (IsInitialized ()) {
    //        Product product = storeController.products.WithID (productId);

    //        if (product != null && product.availableToPurchase) {
    //            print (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
    //            storeController.InitiatePurchase (product);
    //        } else {
    //            print ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
    //        }
    //    } else {
    //        print ("BuyProductID FAIL. Not initialized.");
    //    }
    //}

    //#region Apple Restore Purchases Currently Disabled
    //// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    //// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    ////public void RestorePurchases () {
    ////    // If Purchasing has not yet been set up ...
    ////    if (!IsInitialized ()) {
    ////        // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
    ////        Debug.Log ("RestorePurchases FAIL. Not initialized.");
    ////        return;
    ////    }

    ////    // If we are running on an Apple device ... 
    ////    if (Application.platform == RuntimePlatform.IPhonePlayer ||
    ////        Application.platform == RuntimePlatform.OSXPlayer) {
    ////        // ... begin restoring purchases
    ////        Debug.Log ("RestorePurchases started ...");

    ////        // Fetch the Apple store-specific subsystem.
    ////        var apple = storeExtentionProvider.GetExtension<IAppleExtensions> ();
    ////        // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
    ////        // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
    ////        apple.RestoreTransactions ((result) => {
    ////            // The first phase of restoration. If no more responses are received on ProcessPurchase then 
    ////            // no purchases are available to be restored.
    ////            Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
    ////        });
    ////    }
    ////    // Otherwise ...
    ////    else {
    ////        // We are not running on an Apple device. No work is necessary to restore purchases.
    ////        Debug.Log ("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    ////    }
    ////}
    //#endregion


    //public void OnInitialized (IStoreController controller, IExtensionProvider extensions) {
    //    print ("OnInitialized: PASS");

    //    storeController = controller;
    //    storeExtentionProvider = extensions;
    //}

    //public void OnInitializeFailed (InitializationFailureReason error) {
    //    // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
    //    print ("OnInitializeFailed InitializationFailureReason:" + error);
    //}

    //public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args) {
    //    // A consumable product has been purchased by this user.
    //    if (String.Equals (args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal)) {
    //        print (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
    //        // The consumable item has been successfully purchased, add 75 coins to the player's in-game score.
    //        DataPreservation.data._coins += 75;
    //        DataPreservation.data.SaveData ();
    //        UpdateCoins ();
    //    } else {
    //        print (string.Format ("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
    //    }

    //    return PurchaseProcessingResult.Complete;
    //}

    //public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason) {
    //    // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
    //    // this reason with the user to guide their troubleshooting actions.
    //    print (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    //}
#endregion

    #region Advertisement Code
 //   public void ShowBootsAD () {
	//	if (Advertisement.IsReady ("rewardedVideo")) {
	//		ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
	//		Advertisement.Show ("rewardedVideo", options);
 //           DataPreservation.data._watchedBootsAD = true;
 //           DataPreservation.data.SaveData ();
	//	}
	//}

	//void HandleShowResult (ShowResult result) {
	//	switch (result) {
	//	case ShowResult.Finished:
 //           DataPreservation.data._boots += 1;
 //           DataPreservation.data.SaveData ();
 //           bootsAD.gameObject.SetActive (false);
 //           print (DataPreservation.data._boots + " boots");
	//		print ("FINSIHED");
	//		break;
	//	case ShowResult.Skipped:
	//		print ("SKIPPED");
	//		break;
	//	case ShowResult.Failed:
	//		print ("FAILED");
	//		break;
	//	}
	//}

 //   public void ShowCoinsAD () {
 //       if (Advertisement.IsReady ("rewardedVideo")) {
 //           ShowOptions options = new ShowOptions { resultCallback = ShowResultHandle };
 //           Advertisement.Show ("rewardedVideo", options);
 //           DataPreservation.data._watchedCoinAD = true;
 //           DataPreservation.data.SaveData ();
 //       }
 //   }

 //   void ShowResultHandle (ShowResult result) {
 //       switch (result) {
 //           case ShowResult.Finished:
 //               DataPreservation.data._coins += 50;
 //               DataPreservation.data.SaveData ();
 //               UpdateCoins ();
 //               coinsAD.gameObject.SetActive (false);
 //               print (DataPreservation.data._coins + " coins");
 //               print ("FINSIHED");
 //               break;
 //           case ShowResult.Skipped:
 //               print ("SKIPPED");
 //               break;
 //           case ShowResult.Failed:
 //               print ("FAILED");
 //               break;
 //       }
 //   }
    #endregion

    #region Open/Close Shop
    public void OpenShop () {
		if (DataPreservation.data._watchedBootsAD == true) {
            bootsAD.gameObject.SetActive (false);
		}
        if (DataPreservation.data._watchedCoinAD == true) {
            coinsAD.gameObject.SetActive (false);
        }
        UpdateCoins ();
        shopPanel.gameObject.SetActive (true);
	}

	public void CloseShop () {
		shopPanel.gameObject.SetActive (false);
	}
    #endregion
}
