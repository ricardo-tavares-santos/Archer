using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour {
	public float power;
	public Transform point;
	// Use this for initialization
	public void begin () {
		Destroy (this.gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) {
		if (other.tag == "atkObj") {			
			if (other.GetComponent<atkObjS> ().jianDO == true) {
				Vector3 direction = point.position - transform.position;
				other.GetComponent<Rigidbody> ().AddForceAtPosition (direction.normalized * power * Time.deltaTime, transform.position);
			}
		}
	}
}
