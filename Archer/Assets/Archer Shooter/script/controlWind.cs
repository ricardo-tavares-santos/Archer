using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlWind : MonoBehaviour {

	private Camera cameraObj;
	private Transform windObj;
	private Vector3 posDown;
	private Vector3 posMove;
	private bool hitDownDO;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown (0)) {//按住屏幕是执行
			//记录按下时点的位置
			Ray rayDown = cameraObj.ScreenPointToRay (Input.mousePosition);//定义射线
			RaycastHit hitDown;
			if (Physics.Raycast (rayDown, out hitDown)) {//射线发生碰创时执行                
				if (hitDown.transform.tag == "touchPlane") {
					hitDownDO = true;
					if (windObj)
						Destroy (windObj.gameObject);
					posDown = new Vector3 (hitDown.point.x, hitDown.point.y, 0);
					windObj = Instantiate (Resources.Load ("wind")as GameObject, posDown, Quaternion.identity).transform;
				}
			}
		} else if (Input.GetMouseButton (0)  && hitDownDO) {//按住屏幕是执行
			//记录按下时点的位置
			Ray ray = cameraObj.ScreenPointToRay (Input.mousePosition);//定义射线
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {//射线发生碰创时执行                
				if (hit.transform.tag == "touchPlane") {
					if (windObj) {
						posMove = new Vector3 (hit.point.x, hit.point.y, 0);
						windObj.LookAt (posMove);
						float lenght = Vector3.Distance (posDown, posMove);
						windObj.localScale = new Vector3 (windObj.localScale.x, windObj.localScale.y, lenght);
					}
				}
			}
		} else if (Input.GetMouseButtonUp (0) && hitDownDO) {//按住屏幕是执行
			//记录按下时点的位置
			Ray ray = cameraObj.ScreenPointToRay (Input.mousePosition);//定义射线
			RaycastHit hitUp;
			if (Physics.Raycast (ray, out hitUp)) {//射线发生碰创时执行                
				if (hitUp.transform.tag == "touchPlane") {
					hitDownDO = false;
					windObj.GetComponent<wind> ().begin ();
				}
			}
		}

	}

}
