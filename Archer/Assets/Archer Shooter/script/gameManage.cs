using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;
//???
using EasyMobile;

public class gameManage : MonoBehaviour {
	public GameObject[] platform;
	public GameObject[] enemyObj;
	public GameObject[] missionObj;
	public Transform player;
	public Transform platformLast;
	public float glHeart;

	public GameObject gameOverUI;
	public Text gameOverText;

	public GameObject reliveUI;
	public GameObject btnReliveObj;
	public GameObject btnNextObj;
	public Image reliveTime;
	private int score;


	public string[] levelMission;//
	private int level;
	public Text missionText;
	private int missionCount;
	private string[] sArray;

	private bool reliveTimeDO;
	private bool gameOverDO;
	private bool firstCheckDO;
	private int scoreTemp;
	
	//???
    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }	
	
	
	// Use this for initialization
	void Start () {
		
		if (!RuntimeManager.IsInitialized())
					RuntimeManager.Init();
		
		btnReliveObj.SetActive (false);
		//???
		if(btnReliveObj)btnReliveObj.SetActive(Advertising.IsRewardedAdReady()); 
		
		levelMissionCount ();
		level = PlayerPrefs.GetInt ("level");
		if (level == 0) {
			PlayerPrefs.SetInt ("level",1);
			level = 1;
		}
		Debug.Log ("level"+level);
		sArray = levelMission [level].Split ('_');
		missionCount = int.Parse (sArray [1]);
		missionCheck (int.Parse(sArray [0]));

		scoreTemp = score;
		create ();
	}
	void levelMissionCount(){
		levelMission = new string[101];
		levelMission[0]="1_1";
		levelMission[1]="1_2";
		levelMission[2]="1_3";
		levelMission[3]="3_2";
		levelMission[4]="4_2";
		levelMission[5]="7_2";
		levelMission[6]="3_3";
		levelMission[7]="4_3";
		levelMission[8]="5_3";
		levelMission[9]="9_2";
		levelMission[10]="6_1";
		levelMission[11]="1_5";
		levelMission[12]="3_4";
		levelMission[13]="4_4";
		levelMission[14]="10_2";
		levelMission[15]="5_4";
		levelMission[16]="2_1";
		levelMission[17]="3_5";
		levelMission[18]="7_4";
		levelMission[19]="11_1";
		levelMission[20]="1_7";
		levelMission[21]="9_3";
		levelMission[22]="6_2";
		levelMission[23]="5_5";
		levelMission[24]="7_5";
		levelMission[25]="12_1";
		levelMission[26]="2_2";
		levelMission[27]="4_6";
		levelMission[28]="5_6";
		levelMission[29]="11_2";
		levelMission[30]="1_8";
		levelMission[31]="3_7";
		levelMission[32]="4_8";
		levelMission[33]="6_3";
		levelMission[34]="7_8";
		levelMission[35]="5_9";
		levelMission[36]="10_7";
		levelMission[37]="9_8";
		levelMission[38]="8_1";
		levelMission[39]="4_9";
		levelMission[40]="1_10";
		levelMission[41]="3_10";
		levelMission[42]="7_9";
		levelMission[43]="2_3";
		levelMission[44]="11_3";
		levelMission[45]="4_10";
		levelMission[46]="6_4";
		levelMission[47]="10_8";
		levelMission[48]="12_2";
		levelMission[49]="1_13";
		levelMission[50]="3_11";
		levelMission[51]="4_12";
		levelMission[52]="7_12";
		levelMission[53]="8_2";
		levelMission[54]="4_13";
		levelMission[55]="5_13";
		levelMission[56]="9_12";
		levelMission[57]="6_5";
		levelMission[58]="1_15";
		levelMission[59]="3_14";
		levelMission[60]="4_14";
		levelMission[61]="10_12";
		levelMission[62]="5_14";
		levelMission[63]="2_4";
		levelMission[64]="12_3";
		levelMission[65]="7_14";
		levelMission[66]="11_4";
		levelMission[67]="1_18";
		levelMission[68]="9_13";
		levelMission[69]="6_6";
		levelMission[70]="5_15";
		levelMission[71]="7_15";
		levelMission[72]="8_3";
		levelMission[73]="2_5";
		levelMission[74]="4_16";
		levelMission[75]="5_16";
		levelMission[76]="11_5";
		levelMission[77]="1_21";
		levelMission[78]="3_17";
		levelMission[79]="12_4";
		levelMission[80]="6_7";
		levelMission[81]="7_18";
		levelMission[82]="5_19";
		levelMission[83]="10_17";
		levelMission[84]="9_18";
		levelMission[85]="8_4";
		levelMission[86]="1_24";
		levelMission[87]="3_20";
		levelMission[88]="4_20";
		levelMission[89]="7_19";
		levelMission[90]="2_6";
		levelMission[91]="11_6";
		levelMission[92]="10_18";
		levelMission[93]="5_22";
		levelMission[94]="12_5";
		levelMission[95]="6_8";
		levelMission[96]="9_20";
		levelMission[97]="1_28";
		levelMission[98]="7_25";
		levelMission[99]="3_24";
		levelMission[100]="8_8"; 


	}
	public void missionCheck(int levelStye){

		if (gameOverDO )
			return;
		if(levelStye==int.Parse(sArray [0])){
			if (!firstCheckDO) {
				firstCheckDO = true;
			}else{
				missionCount -= 1;
			}
			if (missionCount <= 0) {
				missionText.gameObject.GetComponent<Animator> ().SetTrigger ("over");
				missionText.text = "Mission Complete";
				gameOverDO = true;
				player.GetComponent<control> ().gameWin ();
				PlayerPrefs.SetInt ("levelComplete" + level, 1);

				StartCoroutine (waitGameWinUI ());

			} else {
				//Debug.Log ("missionCount" + missionCount);
				missionText.gameObject.GetComponent<Animator> ().SetTrigger ("play");
				if (sArray [0] == "1") {
					missionText.text = "Kill Enemy " + missionCount + " times";
				} else if (sArray [0] == "2") {
					missionText.text = "Get Heart " + missionCount + " times";
				} else if (sArray [0] == "3") {
					missionText.text = "Kill Axeman " + missionCount + " times";
				} else if (sArray [0] == "4") {
					missionText.text = "Kill Stick Soldier " + missionCount + " times";
				} else if (sArray [0] == "5") {
					missionText.text = "Kill Knife Soldier " + missionCount + " times";
				} else if (sArray [0] == "6") {
					missionText.text = "Kill Hammer Giant " + missionCount + " times";
				} else if (sArray [0] == "7") {
					missionText.text = "Shoot Gong " + missionCount + " times";
				} else if (sArray [0] == "8") {
					missionText.text = "Shoot into the well " + missionCount + " times";
				} else if (sArray [0] == "9") {
					missionText.text = "Shoot Tower " + missionCount + " times";
				} else if (sArray [0] == "10") {
					missionText.text = "Shoot Windmill " + missionCount + " times";
				} else if (sArray [0] == "11") {
					missionText.text = "Shoot Treasure Box in the tower " + missionCount + " times";
				} else if (sArray [0] == "12") {
					missionText.text = "Shoot into the door " + missionCount + " times";
				}
			}
		}
	}
	void showADS(){
		//???	
		bool isReady = Advertising.IsInterstitialAdReady();		
		if (isReady) {
			Advertising.ShowInterstitialAd();
		}		
	}
	void showRewardADS(){
		//???
		bool isReady = Advertising.IsRewardedAdReady();
		if (isReady) {
			Advertising.ShowRewardedAd();
		}		
	}
	IEnumerator waitGameWinUI(){
		yield return new WaitForSeconds (1);
		
		//???		
		//AdManager.Instance.GameOver();
		showADS();

		gameOverUI.SetActive (true);
		gameOverText.text = "Mission Complete";
		btnNextObj.transform.Find ("Text").GetComponent<Text> ().text = "Next";
	
		PlayerPrefs.SetInt ("level", level+1);

		player.GetComponent<control> ().happy  ();
	}
	IEnumerator waitGameLoseUI(){
		//if (score > 2) {
		if (Advertising.IsRewardedAdReady()) {
			yield return new WaitForSeconds (2);
			reliveTimeDO = true;
			reliveTime.fillAmount = 0f;
			//???
			if(btnReliveObj)btnReliveObj.SetActive(Advertising.IsRewardedAdReady()); 

			reliveUI.SetActive (true);

		} else { 
			cancelRelive ();
		} 
	}
	public void btnRelive(){
		//player.GetComponent<control>().relive();
		//reliveUI.SetActive (false);
		//???
		showRewardADS();
		//gameOverDO = false;
		//reliveTimeDO = false;
	}
	
	//???
	void OnEnable()
	{
		Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
	}
	void OnDisable()
	{
		Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
	}
	void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
	{
		player.GetComponent<control>().relive();
		reliveUI.SetActive (false);
		gameOverDO = false;
		reliveTimeDO = false;
	}
	void RewardedAdSkippedHandler(RewardedAdNetwork network, AdLocation location)
	{
		cancelRelive();
	}	
	
	
	
	
	public void cancelRelive(){
		reliveTimeDO = false;
		reliveUI.SetActive (false);

		if (level == 100) {
			btnNextObj.SetActive (false);
		}

		//???
		showADS();
		
		gameOverUI.SetActive (true);
		gameOverText.text = "Mission Fail";
		btnNextObj.transform.Find ("Text").GetComponent<Text> ().text = "Again";
		player.GetComponent<control> ().die ();
	}


	public void Success(string str) {
	}

	public void Fail() {
	} 

	// Update is called once per frame
	void FixedUpdate () {
		if (scoreTemp != score) {
			scoreTemp = score;
			missionCheck (1);
		}
		if (reliveTimeDO  == true){
			//Reduce fill amount over 30 seconds
			reliveTime .fillAmount += 1.0f / 4 * Time.deltaTime;
			if (reliveTime.fillAmount ==1f) {
				reliveTimeDO = false;

				cancelRelive ();

			}
		}

	}
	void create(){
		Vector3 pos = new Vector3 (platformLast.position.x+ 12 + Random.Range (-1f, 0.5f), Random.Range (-1.5f, 2f), platformLast.position.z );
		platformLast = Instantiate (platform[Random.Range(0,platform.Length)], pos, Quaternion.identity).transform;
		if (Random.Range (0, 100) < 40 && sArray [0] != "1" && sArray [0] != "0"&& sArray [0] != "2") {
			Instantiate (missionObj  [int.Parse(sArray [0])], pos, Quaternion.identity);
		} else {
			Instantiate (enemyObj [Random.Range (0, enemyObj.Length)], pos, Quaternion.identity);
		}
		if (glHeart > Random.Range (0, 100)) {
			Vector3 pos2 = new Vector3 (pos.x - 5, pos.y + Random.Range (0, 4), pos.z);
			Instantiate (Resources.Load("heart")as GameObject , pos2, Quaternion.identity);
		}
	}
	private GameObject enemyObjTemp;
	public void enemyDie(GameObject ObjTemp){
		enemyObjTemp = ObjTemp;
		StartCoroutine (waitJump ());
		score += 1;
	}
	IEnumerator waitJump(){
		yield return new WaitForSeconds (1);
		player.GetComponent<control> ().jump (platformLast);
		create ();
		if(enemyObjTemp.name.Substring(0,3)=="tar"){ 
			Instantiate (Resources.Load("FX/target_dead")as GameObject , enemyObjTemp.transform.position , Quaternion.identity);
		}
		Destroy (enemyObjTemp);
	}
	public void btnRestart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void btnPause(){
		Time.timeScale = 0;

	}
	public void gameOver(){
		StartCoroutine (waitGameLoseUI ());	
	}

	public void btnResume(){
		Time.timeScale = 1;
	}

	public void btnMain(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("main");
	}

	public void playerFireOver(){		
		StartCoroutine (waitPlayerFireOver ());
	}
	IEnumerator waitPlayerFireOver(){
		yield return new WaitForSeconds (1.5f);
		if (GameObject.FindGameObjectWithTag ("enemy")) {
			GameObject.FindGameObjectWithTag ("enemy").GetComponent<enemy> ().stateNum = 0;
		} else {
			enemyFireOver (3);
		}
	}
	public void enemyFireOver(float count){
		StartCoroutine (waitEnemyFireOver(count));
	}

	IEnumerator waitEnemyFireOver(float count){
		yield return new WaitForSeconds (count);
		player.GetComponent<control> ().stateNum = 0;
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Delete)) {
			PlayerPrefs.DeleteAll ();
		}
	}
}




/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;
//???
using System;
using GoogleMobileAds.Api;

public class gameManage : MonoBehaviour {
	public GameObject[] platform;
	public GameObject[] enemyObj;
	public GameObject[] missionObj;
	public Transform player;
	public Transform platformLast;
	public float glHeart;

	public GameObject gameOverUI;
	public Text gameOverText;

	public GameObject reliveUI;
	public GameObject btnReliveObj;
	public GameObject btnNextObj;
	public Image reliveTime;
	private int score;


	public string[] levelMission;//
	private int level;
	public Text missionText;
	private int missionCount;
	private string[] sArray;

	private bool reliveTimeDO;
	private bool gameOverDO;
	private bool firstCheckDO;
	private int scoreTemp;
	
	//???
	private InterstitialAd interstitial;
	private RewardBasedVideoAd rewardBasedVideo;
	
	//???
	public void RequestInterstitial()
	{
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-2697339358784861/8496757417";
		#else
			string adUnitId = "unexpected_platform";
		#endif
        interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request); 
    }	
	public void LoadInterstitial()
	{
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request); 
    }		
    private void RequestRewardBasedVideo()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-2697339358784861/3052859042";
        #else
            string adUnitId = "unexpected_platform";
        #endif
//		rewardBasedVideo = new RewardBasedVideoAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }	
	
	

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
		player.GetComponent<control>().relive();
		reliveUI.SetActive (false);
		gameOverDO = false;
		reliveTimeDO = false;
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        cancelRelive();
    }
	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		this.RequestRewardBasedVideo();
	}	
	
	// Use this for initialization
	void Start () {
		
		//???
        MobileAds.Initialize("ca-app-pub-2697339358784861~9940581460");		
		RequestInterstitial();	
		
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		

        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;	
		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		
		this.RequestRewardBasedVideo();	
		
		
		btnReliveObj.SetActive (false);
		//???
		if(btnReliveObj)btnReliveObj.SetActive(rewardBasedVideo.IsLoaded()); 
		
		levelMissionCount ();
		level = PlayerPrefs.GetInt ("level");
		if (level == 0) {
			PlayerPrefs.SetInt ("level",1);
			level = 1;
		}
		Debug.Log ("level"+level);
		sArray = levelMission [level].Split ('_');
		missionCount = int.Parse (sArray [1]);
		missionCheck (int.Parse(sArray [0]));

		scoreTemp = score;
		create ();
	}
	void levelMissionCount(){
		levelMission = new string[101];
		levelMission[0]="1_1";
		levelMission[1]="1_2";
		levelMission[2]="1_3";
		levelMission[3]="3_2";
		levelMission[4]="4_2";
		levelMission[5]="7_2";
		levelMission[6]="3_3";
		levelMission[7]="4_3";
		levelMission[8]="5_3";
		levelMission[9]="9_2";
		levelMission[10]="6_1";
		levelMission[11]="1_5";
		levelMission[12]="3_4";
		levelMission[13]="4_4";
		levelMission[14]="10_2";
		levelMission[15]="5_4";
		levelMission[16]="2_1";
		levelMission[17]="3_5";
		levelMission[18]="7_4";
		levelMission[19]="11_1";
		levelMission[20]="1_7";
		levelMission[21]="9_3";
		levelMission[22]="6_2";
		levelMission[23]="5_5";
		levelMission[24]="7_5";
		levelMission[25]="12_1";
		levelMission[26]="2_2";
		levelMission[27]="4_6";
		levelMission[28]="5_6";
		levelMission[29]="11_2";
		levelMission[30]="1_8";
		levelMission[31]="3_7";
		levelMission[32]="4_8";
		levelMission[33]="6_3";
		levelMission[34]="7_8";
		levelMission[35]="5_9";
		levelMission[36]="10_7";
		levelMission[37]="9_8";
		levelMission[38]="8_1";
		levelMission[39]="4_9";
		levelMission[40]="1_10";
		levelMission[41]="3_10";
		levelMission[42]="7_9";
		levelMission[43]="2_3";
		levelMission[44]="11_3";
		levelMission[45]="4_10";
		levelMission[46]="6_4";
		levelMission[47]="10_8";
		levelMission[48]="12_2";
		levelMission[49]="1_13";
		levelMission[50]="3_11";
		levelMission[51]="4_12";
		levelMission[52]="7_12";
		levelMission[53]="8_2";
		levelMission[54]="4_13";
		levelMission[55]="5_13";
		levelMission[56]="9_12";
		levelMission[57]="6_5";
		levelMission[58]="1_15";
		levelMission[59]="3_14";
		levelMission[60]="4_14";
		levelMission[61]="10_12";
		levelMission[62]="5_14";
		levelMission[63]="2_4";
		levelMission[64]="12_3";
		levelMission[65]="7_14";
		levelMission[66]="11_4";
		levelMission[67]="1_18";
		levelMission[68]="9_13";
		levelMission[69]="6_6";
		levelMission[70]="5_15";
		levelMission[71]="7_15";
		levelMission[72]="8_3";
		levelMission[73]="2_5";
		levelMission[74]="4_16";
		levelMission[75]="5_16";
		levelMission[76]="11_5";
		levelMission[77]="1_21";
		levelMission[78]="3_17";
		levelMission[79]="12_4";
		levelMission[80]="6_7";
		levelMission[81]="7_18";
		levelMission[82]="5_19";
		levelMission[83]="10_17";
		levelMission[84]="9_18";
		levelMission[85]="8_4";
		levelMission[86]="1_24";
		levelMission[87]="3_20";
		levelMission[88]="4_20";
		levelMission[89]="7_19";
		levelMission[90]="2_6";
		levelMission[91]="11_6";
		levelMission[92]="10_18";
		levelMission[93]="5_22";
		levelMission[94]="12_5";
		levelMission[95]="6_8";
		levelMission[96]="9_20";
		levelMission[97]="1_28";
		levelMission[98]="7_25";
		levelMission[99]="3_24";
		levelMission[100]="8_8"; 


	}
	public void missionCheck(int levelStye){

		if (gameOverDO )
			return;
		if(levelStye==int.Parse(sArray [0])){
			if (!firstCheckDO) {
				firstCheckDO = true;
			}else{
				missionCount -= 1;
			}
			if (missionCount <= 0) {
				missionText.gameObject.GetComponent<Animator> ().SetTrigger ("over");
				missionText.text = "Mission Complete";
				gameOverDO = true;
				player.GetComponent<control> ().gameWin ();
				PlayerPrefs.SetInt ("levelComplete" + level, 1);

				StartCoroutine (waitGameWinUI ());

			} else {
				//Debug.Log ("missionCount" + missionCount);
				missionText.gameObject.GetComponent<Animator> ().SetTrigger ("play");
				if (sArray [0] == "1") {
					missionText.text = "Kill Enemy " + missionCount + " times";
				} else if (sArray [0] == "2") {
					missionText.text = "Get Heart " + missionCount + " times";
				} else if (sArray [0] == "3") {
					missionText.text = "Kill Axeman " + missionCount + " times";
				} else if (sArray [0] == "4") {
					missionText.text = "Kill Stick Soldier " + missionCount + " times";
				} else if (sArray [0] == "5") {
					missionText.text = "Kill Knife Soldier " + missionCount + " times";
				} else if (sArray [0] == "6") {
					missionText.text = "Kill Hammer Giant " + missionCount + " times";
				} else if (sArray [0] == "7") {
					missionText.text = "Shoot Gong " + missionCount + " times";
				} else if (sArray [0] == "8") {
					missionText.text = "Shoot into the well " + missionCount + " times";
				} else if (sArray [0] == "9") {
					missionText.text = "Shoot Tower " + missionCount + " times";
				} else if (sArray [0] == "10") {
					missionText.text = "Shoot Windmill " + missionCount + " times";
				} else if (sArray [0] == "11") {
					missionText.text = "Shoot Treasure Box in the tower " + missionCount + " times";
				} else if (sArray [0] == "12") {
					missionText.text = "Shoot into the door " + missionCount + " times";
				}
			}
		}
	}
	void showADS(){
		//???	
		LoadInterstitial();
		if (interstitial.IsLoaded()) {
			interstitial.Show();
		}	
	}
	void showRewardADS(){
		//???
		RequestRewardBasedVideo();
		if (rewardBasedVideo.IsLoaded()) {
			rewardBasedVideo.Show();
		}		
	}
	IEnumerator waitGameWinUI(){
		yield return new WaitForSeconds (1);
		
		//???		
		//AdManager.Instance.GameOver();
		showADS();

		gameOverUI.SetActive (true);
		gameOverText.text = "Mission Complete";
		btnNextObj.transform.Find ("Text").GetComponent<Text> ().text = "Next";
	
		PlayerPrefs.SetInt ("level", level+1);

		player.GetComponent<control> ().happy  ();
	}
	IEnumerator waitGameLoseUI(){
		yield return new WaitForSeconds (1);
	//	if (score > 2) {
			reliveTimeDO = true;
			reliveTime.fillAmount = 0f;
			//???
			if(btnReliveObj)btnReliveObj.SetActive(rewardBasedVideo.IsLoaded()); 

			reliveUI.SetActive (true);

		/*} else { 
			cancelRelive ();
		} /
	}
	public void btnRelive(){
		//player.GetComponent<control>().relive();
		//reliveUI.SetActive (false);
		//???
		showRewardADS();
		//gameOverDO = false;
		//reliveTimeDO = false;
	}
		
	
	
	
	
	public void cancelRelive(){
		reliveTimeDO = false;
		reliveUI.SetActive (false);

		if (level == 100) {
			btnNextObj.SetActive (false);
		}

		//???
		showADS();
		
		gameOverUI.SetActive (true);
		gameOverText.text = "Mission Fail";
		btnNextObj.transform.Find ("Text").GetComponent<Text> ().text = "Again";
		player.GetComponent<control> ().die ();
	}


	public void Success(string str) {
	}

	public void Fail() {
	} 

	// Update is called once per frame
	void FixedUpdate () {
		if (scoreTemp != score) {
			scoreTemp = score;
			missionCheck (1);
		}
		if (reliveTimeDO  == true){
			//Reduce fill amount over 30 seconds
			reliveTime .fillAmount += 1.0f / 4 * Time.deltaTime;
			if (reliveTime.fillAmount ==1f) {
				reliveTimeDO = false;

				cancelRelive ();

			}
		}

	}
	void create(){
		Vector3 pos = new Vector3 (platformLast.position.x+ 12 + UnityEngine.Random.Range (-1f, 0.5f), UnityEngine.Random.Range (-1.5f, 2f), platformLast.position.z );
		platformLast = Instantiate (platform[UnityEngine.Random.Range(0,platform.Length)], pos, Quaternion.identity).transform;
		if (UnityEngine.Random.Range (0, 100) < 40 && sArray [0] != "1" && sArray [0] != "0"&& sArray [0] != "2") {
			Instantiate (missionObj  [int.Parse(sArray [0])], pos, Quaternion.identity);
		} else {
			Instantiate (enemyObj [UnityEngine.Random.Range (0, enemyObj.Length)], pos, Quaternion.identity);
		}
		if (glHeart > UnityEngine.Random.Range (0, 100)) {
			Vector3 pos2 = new Vector3 (pos.x - 5, pos.y + UnityEngine.Random.Range (0, 4), pos.z);
			Instantiate (Resources.Load("heart")as GameObject , pos2, Quaternion.identity);
		}
	}
	private GameObject enemyObjTemp;
	public void enemyDie(GameObject ObjTemp){
		enemyObjTemp = ObjTemp;
		StartCoroutine (waitJump ());
		score += 1;
	}
	IEnumerator waitJump(){
		yield return new WaitForSeconds (1);
		player.GetComponent<control> ().jump (platformLast);
		create ();
		if(enemyObjTemp.name.Substring(0,3)=="tar"){ 
			Instantiate (Resources.Load("FX/target_dead")as GameObject , enemyObjTemp.transform.position , Quaternion.identity);
		}
		Destroy (enemyObjTemp);
	}
	public void btnRestart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void btnPause(){
		Time.timeScale = 0;

	}
	public void gameOver(){
		StartCoroutine (waitGameLoseUI ());	
	}

	public void btnResume(){
		Time.timeScale = 1;
	}

	public void btnMain(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("main");
	}

	public void playerFireOver(){		
		StartCoroutine (waitPlayerFireOver ());
	}
	IEnumerator waitPlayerFireOver(){
		yield return new WaitForSeconds (1.5f);
		if (GameObject.FindGameObjectWithTag ("enemy")) {
			GameObject.FindGameObjectWithTag ("enemy").GetComponent<enemy> ().stateNum = 0;
		} else {
			enemyFireOver (3);
		}
	}
	public void enemyFireOver(float count){
		StartCoroutine (waitEnemyFireOver(count));
	}

	IEnumerator waitEnemyFireOver(float count){
		yield return new WaitForSeconds (count);
		player.GetComponent<control> ().stateNum = 0;
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Delete)) {
			PlayerPrefs.DeleteAll ();
		}
	}
}

*/