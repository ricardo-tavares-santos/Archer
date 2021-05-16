using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	public int HP;
	public float power;

	public Transform fireForward;
	public Transform firePoint;
	public Transform jiantou;
	public GameObject atkObj;
	public Transform[] point;
	public Transform target;
	public float fireCD;
	public float smoothTime = 1f;
	public Transform points;

	private Transform pointTemp;
	private float powerTemp;

	private float powerMaxDist = 3;
	[HideInInspector]
	public int stateNum;
	private GameObject player;
	private Vector3 velocity=Vector3.zero;
	private Animator ani;
	private gameManage gm;
	private bool dieDO;
	private Animator bodyAni;
	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("gameManage").GetComponent<gameManage> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		jiantou.gameObject.SetActive (false);
		stateNum = 3;
		//StartCoroutine (waitNextShoot (1));
		ani = this.GetComponent<Animator> ();
		bodyAni = this.transform.Find ("body/charters").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (dieDO)
			return;
		if (stateNum == 0) {
			target.localPosition = new Vector3 (0, 0, 0);
			pointTemp=point[Random.Range(0,point.Length)];
			jiantou.localScale = new Vector3 (jiantou.localScale.x, jiantou.localScale.y, 0 );
			jiantou.gameObject.SetActive (true);

			points.LookAt (player.transform.position);
			bodyAni.SetTrigger ("atk");
			stateNum = 1;
		}else if (stateNum == 1) {
			target.position = Vector3.SmoothDamp (target.position, pointTemp.position, ref velocity, smoothTime);
			if (Vector3.Distance (target.position, pointTemp.position) < 0.05f)
				stateNum = 2;

			fireForward.LookAt (target.position );
			float dist = Vector3.Distance (fireForward.position, target.position);
			if (dist > powerMaxDist)
				dist = powerMaxDist;
			powerTemp = dist*power ;
			jiantou.localScale = new Vector3 (jiantou.localScale.x, jiantou.localScale.y, powerTemp/power/2 );
		} else if (stateNum == 2) {
			atkObjS atkObjClone = Instantiate (atkObj, firePoint.position, firePoint.rotation).GetComponent<atkObjS> ();
			atkObjClone.power = powerTemp + Random.Range (-powerTemp / 4, powerTemp / 4); 
			atkObjClone.enemyObj = this.GetComponent<enemy> ();
			atkObjClone.begin ();
			jiantou.gameObject.SetActive (false);
			bodyAni.SetTrigger ("atk2");
			//StartCoroutine (waitNextShoot ());
			stateNum = 3;
			gm.enemyFireOver (1.5f);
		}
	}
	IEnumerator waitNextShoot(float waitTime){
		yield return new WaitForSeconds (waitTime );
		stateNum = 0;
	}
	public void hit(){
		ani.SetTrigger ("hit");
		HP -= 1;
		if (HP <= 0) {
			HP = 0;
			//Debug.Log (this.name.Split ('_') [0]);
			gm.enemyDie (this.gameObject);
			if (this.name.Split ('_') [0] == "enemy00") {
				gm.missionCheck (3);

			}else if (this.name.Split ('_') [0] == "enemy01") {
				gm.missionCheck (4);
			}else if (this.name.Split ('_') [0] == "enemy02") {
				gm.missionCheck (5);
			}else if (this.name.Split ('_') [0] == "enemy03") {
				gm.missionCheck (6);
			}
			dieDO = true;
			//StartCoroutine (waitDestroy ());
			jiantou.gameObject.SetActive (false);
			this.GetComponent<Collider> ().enabled = false;
			bodyAni.SetTrigger ("dead");
		}
	}


	public void happy(){
		if(bodyAni)
			bodyAni.SetTrigger ("happy");
	}
	public void angry(){
		if(bodyAni)
			bodyAni.SetTrigger ("angry");
	}
}
