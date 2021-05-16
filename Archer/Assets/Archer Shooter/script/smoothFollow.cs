using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothFollow : MonoBehaviour {
	public Transform target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	private float posTempX;
	private float posTempY;
	private float posTempZ;
	// Use this for initialization
	void Start () {
		posTempX = transform.position.x - target.position.x;
		posTempY = transform.position.y - target.position.y;
		posTempZ = transform.position.z - target.position.z;

	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 pos = new Vector3 (target.position.x+posTempX,target.position.y+posTempY,target.position.z+posTempZ);

		transform.position = Vector3.SmoothDamp (transform.position, pos, ref velocity, smoothTime);
	}
}
