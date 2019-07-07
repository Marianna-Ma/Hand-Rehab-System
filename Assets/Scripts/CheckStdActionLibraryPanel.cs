using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckStdActionLibraryPanel : MonoBehaviour, IPointerClickHandler{

	// Use this for initialization
	void Start () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "AddActionButton")
        {
            Debug.Log("clickclick");
            string act_id = "";
            string act_name = "";
            act_id = PlayerPrefs.GetString("selectStdActionID");
            act_name = PlayerPrefs.GetString("selectStdActionName");
            Debug.Log("act_id: " + act_id);
            Debug.Log("act_name: " + act_name);
        }
           
       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
