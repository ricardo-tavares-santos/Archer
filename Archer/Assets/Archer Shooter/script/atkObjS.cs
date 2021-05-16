using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkObjS : MonoBehaviour {
	[HideInInspector]
	public float power;
	public bool jianDO;
	private Rigidbody rig;
	private Vector3 posLast;
	private Vector3 posNew;
	[HideInInspector ]
	public enemy enemyObj;
	private control player;
	private gameManage gm;
	private bool hitDO;
	// Use this for initialization
	public void begin () {
		rig = this.GetComponent<Rigidbody> ();
		rig.AddRelativeForce (Vector3.forward * power);
		if (!jianDO) {
			rig.AddRelativeTorque (Vector3.right * power);
			this.GetComponent<Collider> ().isTrigger = false;
		} else {
			this.GetComponent<Collider> ().isTrigger = true;
		}
		StartCoroutine ("waitDestroy");
		gm=GameObject.FindGameObjectWithTag ("gameManage").GetComponent<gameManage> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<control> ();


	}
	IEnumerator waitDestroy(){
		yield return new WaitForSeconds (5);
		Destroy (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
		if(jianDO)
			transform.LookAt (posLast);

	}
	void LateUpdate () {
		if(jianDO)
			posLast = transform.position;
	}
	void OnTriggerEnter(Collider other) {
		if (jianDO) {
			if (other.tag == "enemy") {
				this.transform.SetParent (other.transform.Find ("body/charters/Bip001"));
				Destroy (this.GetComponent<Rigidbody> ());
				this.GetComponent<Collider> ().enabled = false;

				other.gameObject.GetComponent<enemy> ().hit ();
				player.happy ();
				Instantiate (Resources.Load ("FX/enemy_hit")as GameObject, this.transform.Find ("hitPoint").position, Quaternion.identity).transform.SetParent (this.transform);
			} else if (other.tag == "platform") {
				this.transform.SetParent (FindUpParent(other.transform) );
				Destroy (this.GetComponent<Rigidbody> ());
				this.GetComponent<Collider> ().enabled = false;
				player.angry ();
			} else if (other.tag == "obj") {				

				player.happy ();
				gm.enemyDie (other.gameObject);
				if (other.name.Split ('(') [0] == "target00") {
					gm.missionCheck (7);
				} else if (other.name.Split ('(') [0] == "target01") {
					gm.missionCheck (8);
				} else if (other.name.Split ('(') [0] == "target02") {
					gm.missionCheck (9);
				} else if (other.name.Split ('(') [0] == "target03") {
					gm.missionCheck (10);
				} else if (other.name.Split ('(') [0] == "target04") {
					gm.missionCheck (11);
				} else if (other.name.Split ('(') [0] == "target05") {
					gm.missionCheck (12);
				}
				this.transform.SetParent (FindUpParent(other.transform) );
				Destroy (this.GetComponent<Rigidbody> ());
				this.GetComponent<Collider> ().enabled = false;

			} else if (other.tag == "heart") {	
				
				Instantiate (Resources.Load ("FX/heart_hit")as GameObject, other.transform.position, Quaternion.identity);
				player.addHeart ();
				Destroy (other.gameObject);
				player.happy ();
				gm.missionCheck (2);
			} else if (other.tag == "objMain") {	
				this.transform.SetParent (other.transform);
				Destroy (this.GetComponent<Rigidbody> ());
				this.GetComponent<Collider> ().enabled = false;
				player.happy  ();
			}
			StopCoroutine  ("waitDestroy");
		}
	}
	Transform FindUpParent(Transform zi){
		if (zi.parent == null)
			return zi;
		else
			return FindUpParent(zi.parent);
	}

	void OnCollisionEnter(Collision collision) {
		if (!jianDO && !hitDO ) {
			if (collision.gameObject .tag == "Player") {
				//Debug.Log ("hit");
				collision.gameObject .GetComponent<control > ().hit();
				//this .GetComponent<Collider> ().enabled = false;
				enemyObj.happy ();
				ContactPoint contact = collision.contacts[0];
				Vector3 pos = contact.point;
				Instantiate (Resources.Load ("FX/people_hit")as GameObject, pos, Quaternion.identity);
				hitDO = true;
			}else if (collision.gameObject .tag == "platform") {				
				//this .GetComponent<Collider> ().enabled = false;
				enemyObj.angry ();
			}
		}
	}


}
