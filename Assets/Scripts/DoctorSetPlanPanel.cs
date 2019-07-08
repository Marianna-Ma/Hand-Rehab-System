using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorSetPlanPanel : MonoBehaviour, IPointerClickHandler{
    public InputField setDays;
    public InputField setTimes;
    public InputField setSpan;
    string select_act_id = "";
    string select_act_name = "";
    string patient_id = "";

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetString("selectPatientID", "300001");
        PlayerPrefs.SetString("selectStdActionID", "400002");
        PlayerPrefs.SetString("selectStdActionName", "食指伸展");
        
        select_act_id = PlayerPrefs.GetString("selectStdActionID");
        select_act_name = PlayerPrefs.GetString("selectStdActionName");
        patient_id = PlayerPrefs.GetString("selectPatientID");

        //GameObject.Find("StdActionLibraryID").GetComponent<Text>().text = patient_id;
        GameObject.Find("Canvas/DoctorSetPlanPanel/SetPlanIDText").GetComponent<Text>().text = select_act_id;
        GameObject.Find("Canvas/DoctorSetPlanPanel/SetPlanNameText").GetComponent<Text>().text = select_act_name;
        //GameObject.Find("NameText").GetComponent<Text>().text = select_act_name;
        //Debug.Log("select_act_name: " + select_act_name);
        //Debug.Log("------------------------------------------------------");

        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "ConfirmButton")
        {
            //timeDropDown.options[timeDropDown.value].text
            Dropdown select_hand_item = GameObject.Find("Canvas/DoctorSetPlanPanel/SelectHand").GetComponent<Dropdown>();
            
            int select_hand = select_hand_item.value;
            string set_day = setDays.text.ToString();
            string set_times = setTimes.text.ToString();
            string set_span = setSpan.text.ToString();
            Debug.Log(select_hand + " " + set_day + " " + set_times + " " + set_span);
            //TrainingPlan.addTrainingPlan(patient_id, select_act_id, select_hand, int.Parse(set_times), int.Parse(set_span), int.Parse(set_day));

        }
    
    }

	// Update is called once per frame
	void Update () {
		
	}
}
