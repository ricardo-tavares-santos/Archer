using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//???
using EasyMobile;
//???
public class rwdesenv : MonoBehaviour {
	
	//???
    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }	
	
	public void somStop() {
		AudioListener.pause = true;
    }
	public void somStart() {
		AudioListener.pause = false;
    }		
	
	
    public void OpenStoreBlack()
    {
		Application.OpenURL("https://play.google.com/store/apps/dev?id=8661706558284396298");		
    }
    public void OpenStoreWhite()
    {
		Application.OpenURL("https://play.google.com/store/apps/dev?id=7003053749192026050");		
    }
	
    public void LikeFacebookPage()
    {
	    Application.OpenURL("https://fb.me/rwdesenvgames");
    }	


    public void Rate()
    {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.RWdesenv.ArcherShooter");		
    }	
	
	
	string subject = "Archer Shooter";
	string body = "Archer Shooter\n\nhttps://play.google.com/store/apps/details?id=com.RWdesenv.ArcherShooter";	 
	public void shareText(){
		//execute the below lines if being run on a Android device
		#if UNITY_ANDROID
			  //Refernece of AndroidJavaClass class for intent
			  AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			  //Refernece of AndroidJavaObject class for intent
			  AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			  //call setAction method of the Intent object created
			  intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			  //set the type of sharing that is happening
			  intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			  //add data to be passed to the other activity i.e., the data to be sent
			  intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
			  intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
			  //get the current activity
			  AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			  AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			  //start the activity by sending the intent data
			  currentActivity.Call ("startActivity", intentObject);
		#endif
	}	

		
	
}
