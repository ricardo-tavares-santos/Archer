using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour {
	public bool mainDO;
	public int HP;
	public GameObject heart;
	public float power;

	public Transform fireForward;
	public Transform firePoint;
	public Transform jiantou;
	public GameObject atkObj;

	private Transform targetJump;
	public Animator ani;

	private float powerTemp;
	private Vector3 pos;
	private float powerMaxDist = 3;

	private  GameObject[] hearts;

	private bool jumpDO;
	public float smoothTime_jump = 0.3f;
	private Vector3 velocityJump=Vector3.zero;

	public Transform[] point;
	public Transform target;
	public float fireCD;
	public float smoothTime = 1f;
	public Transform points;
	private Transform pointTemp;
	private bool gameOverDO;
	[HideInInspector]
	public int stateNum;
	//private GameObject enemy;
	private Vector3 velocity=Vector3.zero;
	private gameManage gm;
	private Animator bodyAni;
	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("gameManage").GetComponent<gameManage> ();
		jiantou.gameObject.SetActive (false);
		stateNum = 3;
		StartCoroutine (waitNextShoot (1));

		if (!mainDO) {
			hearts = new GameObject[HP ];
			createHeart (HP);
		}
		bodyAni = this.transform.Find ("body/charters").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (gameOverDO)
			return;
		if (jumpDO ) {			

			transform.position = Vector3.SmoothDamp (transform.position, targetJump.position, ref velocityJump, smoothTime_jump);
			if (Vector3.Distance (targetJump.position, transform.position) < 0.05f) {
				jumpDO = false;
				this.GetComponent<Collider> ().enabled = true;
				StartCoroutine (waitNextShoot (0.3f));

			}
		} else {

			if (stateNum == 0) {
				target.localPosition = new Vector3 (0, 0, 0);
				pointTemp = point [Random.Range (0, point.Length)];
				jiantou.localScale = new Vector3 (jiantou.localScale.x, jiantou.localScale.y, 0);
				jiantou.gameObject.SetActive (true);

				//points.LookAt (enemy.transform.position);
				if (pointTemp.name == "pointD") {
					bodyAni.SetTrigger ("atkD");
				}else if (pointTemp.name == "pointM") {
					bodyAni.SetTrigger ("atkM");
				}else if (pointTemp.name == "pointU") {
					bodyAni.SetTrigger ("atkU");
				}

				stateNum = 1;
			} else if (stateNum == 1) {
				target.position = Vector3.SmoothDamp (target.position, pointTemp.position, ref velocity, smoothTime);
				if (Vector3.Distance (target.position, pointTemp.position) < 0.05f)
					stateNum = 2;

				fireForward.LookAt (target.position);
				float dist = Vector3.Distance (fireForward.position, target.position);
				if (dist > powerMaxDist)
					dist = powerMaxDist;
				powerTemp = dist * power;
				jiantou.localScale = new Vector3 (jiantou.localScale.x, jiantou.localScale.y, powerTemp / power / 2);
			} else if (stateNum == 2) {
				atkObjS atkObjClone = Instantiate (atkObj, firePoint.position, firePoint.rotation).GetComponent<atkObjS> ();
				atkObjClone.power = powerTemp + Random.Range (-powerTemp / 2, powerTemp / 2);

				atkObjClone.begin ();
				jiantou.gameObject.SetActive (false);
				bodyAni.SetTrigger ("atk");
				stateNum = 3;
				if (mainDO) {
					StartCoroutine (waitNextShoot (fireCD));
				} else {
					gm.playerFireOver ();
				}

			}
		}
		if (Input.GetKeyDown (KeyCode.Delete)) {
			PlayerPrefs.DeleteAll ();
		}
	}
	IEnumerator waitNextShoot(float waitTime){
		yield return new WaitForSeconds (waitTime );
		stateNum = 0;
		//if (GameObject.FindGameObjectWithTag ("enemy")) {
		//	enemy = GameObject.FindGameObjectWithTag ("enemy");
		//} else {
		//	enemy = GameObject.FindGameObjectWithTag ("obj");
		//}
	}

	public void hit(){
		ani.SetTrigger ("hit");
		HP -= 1;
		if (HP <= 0) {
			HP = 0;
			gm.gameOver ();
			gameOverDO = true;
		} 
		createHeart (HP);
	}
	public void addHeart(){
		HP += 1;
		if (HP > 3) {
			HP = 3;
		}
		createHeart (HP);
	}
	void createHeart(int hpTemp){		
		foreach (GameObject h in hearts) {
			if(h)
				Destroy (h.gameObject );				
		}

		hearts=new GameObject[hpTemp];
		for (int i = 0; i < hpTemp; i++) {
			GameObject heartClone= Instantiate (heart, heart.transform.position, Quaternion.identity);
			heartClone.transform.SetParent (heart.transform.parent);
			heartClone.transform.localScale = new Vector3 (1, 1, 1);
			heartClone.SetActive (true);
			hearts [i] = heartClone;
		}
	}
	public void jump(Transform targetPoint){
		targetJump = targetPoint;
		jumpDO = true;
		ani.SetTrigger ("jump");
		bodyAni.SetTrigger ("jump");
		this.GetComponent<Collider> ().enabled = false;	
	}
	public void relive(){
		gameOverDO = false;
		HP = 3;
		createHeart (HP);
		stateNum = 0;
		bodyAni.SetTrigger ("happy");
		Instantiate (Resources.Load ("FX/FXrelive")as GameObject, this.transform.position, Quaternion.identity);
	}
	public void gameWin(){
		gameOverDO = true ;
		bodyAni.SetTrigger ("happy");
		this.GetComponent<Collider> ().enabled = false;
	}
	public void die(){
		bodyAni.SetTrigger ("dead");
	}
	public void happy(){
		bodyAni.SetTrigger ("happy");
	}
	public void angry(){
		bodyAni.SetTrigger ("angry");
	}
}
