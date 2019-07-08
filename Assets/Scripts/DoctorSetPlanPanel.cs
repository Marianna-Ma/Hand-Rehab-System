using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorSetPlanPanel : MonoBehaviour, IPointerClickHandler{
    public InputField setDays;
    public InputField setTimes;
    public InputField setSpan;


    // Use this for initialization
    void Start () {
        PlayerPrefs.SetString("selectStdActionID", "400002");
        PlayerPrefs.SetString("selectStdActionName", "食指伸展");
        string select_act_id = "";
        string select_act_name = "";
        select_act_id = PlayerPrefs.GetString("selectStdActionID");
        select_act_name = PlayerPrefs.GetString("selectStdActionName");
        //GameObject.Find("StdActionLibraryID").GetComponent<Text>().text = patient_id;
        GameObject.Find("SetPlanIDText").GetComponent<Text>().text = select_act_id;
        GameObject.Find("SetPlanNameText").GetComponent<Text>().text = select_act_name;
        //GameObject.Find("NameText").GetComponent<Text>().text = select_act_name;
        //Debug.Log("select_act_name: " + select_act_name);
        //Debug.Log("------------------------------------------------------");

        //设置dropdown中的值，1是右手，0是左手
        Dropdown item_select_hand = GameObject.Find("SelectHand").GetComponent<Dropdown>();
        item_select_hand.value = 1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "ConfirmButton")
        {
            //timeDropDown.options[timeDropDown.value].text
            Dropdown select_hand_item = GameObject.Find("SelectHand").GetComponent<Dropdown>();
            string select_hand = select_hand_item.options[select_hand_item.value].text.ToString();
            string set_day = setDays.text.ToString();
            string set_times = setTimes.text.ToString();
            string set_span = setSpan.text.ToString();
            Debug.Log(select_hand + " " + set_day + " " + set_times + " " + set_span);

        }
    
    }

	// Update is called once per frame
	void Update () {
		
	}
}
