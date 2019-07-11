using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorEditPlanPanel : MonoBehaviour, IPointerClickHandler{
    public InputField setDays;
    public InputField setTimes;
    public InputField setSpan;

    string action_id = "";
    string select_act_id = "";
    string select_act_name = "";
    int select_hand = 0;
    string select_days = "";
    string select_times = "";
    string select_span = "";

    // Use this for initialization
    public void Start () {
        //PlayerPrefs.SetString("selectPatientID", "300001");
        //PlayerPrefs.SetString("selectStdActionID", "400003");
        //PlayerPrefs.SetString("selectStdActionName", "拇指伸展");
        //PlayerPrefs.SetInt("selectPlanHand", 1);
        //PlayerPrefs.SetString("selectPlanDays", "9");
        //PlayerPrefs.SetString("selectPlanTimes", "2");
        //PlayerPrefs.SetString("selectPlanSpan", "20");
        select_act_id = PlayerPrefs.GetString("selectStdActionID");
        select_act_name = PlayerPrefs.GetString("selectStdActionName");
        select_hand = PlayerPrefs.GetInt("selectPlanHand");
        select_days = PlayerPrefs.GetString("selectPlanDays");
        select_times = PlayerPrefs.GetString("selectPlanTimes");
        select_span = PlayerPrefs.GetString("selectPlanSpan");
        action_id = PlayerPrefs.GetString("selectPatientID");
        Debug.Log("selectStdActionID1:" + select_act_id);
        //设置初始值
        GameObject.Find("Canvas/DoctorEditPlanPanel/SetPlanIDText").GetComponent<Text>().text = select_act_id;
        GameObject.Find("Canvas/DoctorEditPlanPanel/SetPlanNameText").GetComponent<Text>().text = select_act_name;

        //Dropdown item_select_hand = GameObject.Find("Canvas/DoctorEditPlanPanel/SelectHand").GetComponent<Dropdown>();
        //item_select_hand.value = select_hand;
        if (select_hand == 0)
            GameObject.Find("Canvas/DoctorEditPlanPanel/SelectHand").GetComponent<Text>().text = "左";
        else
            GameObject.Find("Canvas/DoctorEditPlanPanel/SelectHand").GetComponent<Text>().text = "右";

        GameObject.Find("Canvas/DoctorEditPlanPanel/Days").GetComponent<InputField>().text = select_days;
        GameObject.Find("Canvas/DoctorEditPlanPanel/Times").GetComponent<InputField>().text = select_times;
        GameObject.Find("Canvas/DoctorEditPlanPanel/Span").GetComponent<InputField>().text = select_span;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "ConfirmButton")
        {
            string set_day = setDays.text.ToString();
            string set_times = setTimes.text.ToString();
            string set_span = setSpan.text.ToString();
            Debug.Log(select_hand + " " + set_day + " " + set_times + " " + set_span);
            Debug.Log("selectStdActionID2:" + select_act_id);
            TrainingPlan.changeTrainingPlan(action_id, select_act_id, select_hand, int.Parse(set_times), int.Parse(set_span), int.Parse(set_day));

            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorCheckPlanPanelNew");
            GameObject obj = GameObject.Find("Canvas/DoctorCheckPlanPanelNew");
            CreatePlanTable panel = (CreatePlanTable)obj.GetComponent(typeof(CreatePlanTable));
            panel.Start();
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
