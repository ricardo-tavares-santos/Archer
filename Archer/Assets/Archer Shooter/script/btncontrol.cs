using UnityEngine;  
using UnityEngine.Events;  
using UnityEngine.EventSystems;  
using System.Collections;  
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class btncontrol:MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler  
{  
	[HideInInspector]
	public int levelNum;
	public Text levelNumText;
	public GameObject completeUI;
	[HideInInspector]
	public bool unLockDO;
	public GameObject lockObj;


	// Use this for initialization  
	public void begin ()  
	{  
		levelNumText.text = "" + levelNum;
		if (unLockDO) {			
			lockObj.SetActive (false);
		} else {
			lockObj.SetActive (true);
		}
		if (PlayerPrefs.GetInt ("levelComplete" + levelNum) == 1) {
			completeUI.SetActive (true);
		} else {
			completeUI.SetActive (false);
		}
	}  

	public void OnPointerDown (PointerEventData eventData)  
	{  
		
	}  

	public void OnPointerUp (PointerEventData eventData)  
	{  
		GameObject.FindGameObjectWithTag("gameManage").GetComponent<mainManage>().loadingUI .SetActive (true);
		PlayerPrefs.SetInt ("level", levelNum);
		SceneManager.LoadScene ("gameMission");
	}  

	public void OnPointerExit (PointerEventData eventData)  
	{  

	}  
}  