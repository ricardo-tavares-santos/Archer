using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class mainManage : MonoBehaviour {



	public GameObject levelSelectUI;
	public GameObject btnLevel;
	public int levelCount;
	public Slider levelSlider;
	public Transform levelObj;
	public Text guideText;
	public Transform[] fingerPointBegin;
	public Transform[] fingerPointEnd;
	public float fingerCD;
	public GameObject loadingUI;
	private bool fingerDO;
	private Transform fingerTemp;
	private Transform fingerPointBeginTemp;
	private Transform fingerPointEndTemp;
	private float timeTemp;
	private float levelHeight;
	private int unLockNum;
	private Transform windObj;
	private float  smoothTime=1f;
	private Vector3 velocity=Vector3.zero;
	private bool guideDO=true ;

	// Use this for initialization


	void Start () {
		timeTemp = Time.time + 2;

		for (int i = levelCount; i > 0; i--) {
			if (PlayerPrefs.GetInt ("levelComplete" + i) == 1) {
				unLockNum = i;
				break;
			}
		}

		for (int i = 0; i < levelCount; i++) {
			btncontrol btnLevelClone = Instantiate (btnLevel, btnLevel.transform.position, Quaternion.identity).GetComponent<btncontrol> ();
			btnLevelClone.levelNum = i + 1;
			btnLevelClone.gameObject.transform.SetParent (btnLevel.transform.parent);
			btnLevelClone.gameObject.transform.localScale = new Vector3 (1, 1, 1);
			btnLevelClone.gameObject.SetActive (true);
			if (i < unLockNum + 2) {
				btnLevelClone.unLockDO = true;

			} else {
				btnLevelClone.unLockDO = false;
			}
			btnLevelClone.begin ();
		}

		levelHeight = Mathf.Ceil (levelCount / 10) * 70;


	}
	void Update(){
		if (Time.time > timeTemp && guideDO ) {
			timeTemp = Time.time + fingerCD;
			fingerPointBeginTemp = fingerPointBegin [Random.Range (0, fingerPointBegin.Length)];
			fingerPointEndTemp=fingerPointEnd  [Random.Range (0, fingerPointEnd.Length)];
			windObj = Instantiate (Resources.Load ("wind")as GameObject, fingerPointBeginTemp.position, Quaternion.identity).transform;
			fingerTemp=Instantiate (Resources.Load ("finger")as GameObject, fingerPointBeginTemp.position, Quaternion.identity).transform ;
			fingerDO = true;
		}

			if (fingerDO) {
				fingerTemp.position = Vector3.SmoothDamp (fingerTemp.position, fingerPointEndTemp.position, ref velocity, smoothTime);
				float lenght = Vector3.Distance (fingerTemp.position, fingerPointBeginTemp.position);
				windObj.LookAt (fingerTemp.position);
				windObj.localScale = new Vector3 (windObj.localScale.x, windObj.localScale.y, lenght);
				if (Vector3.Distance (fingerTemp.position, fingerPointEndTemp.position) < 0.1f) {	
					Destroy (fingerTemp.gameObject);
					Destroy (windObj.gameObject);
					fingerDO = false;
				}
			}

	}


	public void btnPlay(){		
		loadingUI.SetActive (true);
		SceneManager.LoadScene ("gameMission");
	}
	public void btnLevelSelect(){
		levelSelectUI.SetActive (true);
	}
	public void levelSliderMove(){
		levelObj.transform.localPosition  = new Vector2 (levelObj.transform.localPosition.x, levelSlider.value *levelHeight );
	}
	public void btnGuide(){
		if (guideDO) {
			guideDO = false;
			guideText.text = "Guide\nclose";
		} else {
			guideDO = true;
			guideText.text = "Guide\nopen";
		}
	}
}
