using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour {
    public GameObject textField;
    //GameObject leftHand;
    //GameObject rightHand;
    int time = 30; //此处使用数据库中读取的数据
    public int time2 = 3;
	// Use this for initialization
	 void Start () {
        //GameObject.Find("Canvas/PatientTrainPanel/ResultText").SetActive(false);
        //GameObject.Find("Canvas").GetComponent<HandControl>().ShowHands();
        //int tempTime = int.Parse(PlayerPrefs.GetString("selectPlanSpan")) * 60;
        //time = tempTime; //训练计划时间
        //StartCoroutine(Count());
        //GameObject.Find("CountDownThreeText").GetComponent<Text>().text = "";
    }

    public void StartCountDown()
    {
        GameObject.Find("Canvas/PatientTrainPanel/ResultText").SetActive(false);
        GameObject.Find("Canvas").GetComponent<HandControl>().ShowHands();
        int tempTime = int.Parse(PlayerPrefs.GetString("selectPlanSpan")) * 60;
        time = tempTime; //训练计划时间
        StartCoroutine(Count());
        GameObject.Find("CountDownThreeText").GetComponent<Text>().text = "";
    }
	
	// Update is called once per frame
	void Update () {
        //GameObject.Find("LeapControlledHands/leftHand").SetActive(true);
        //GameObject.Find("LeapControlledHands/rightHand").SetActive(true);
        //GameObject.Find("LeapControlledHands/leftHand").SetActive(true);
        //GameObject.Find("LeapControlledHands/rightHand").SetActive(true);
        
    }

    IEnumerator Count()
    {
        GameObject.Find("Canvas/PatientTrainPanel/ResultText").SetActive(false);
        while (time > 0)
        {
            GameObject.Find("TimeCountDownText").GetComponent<Text>().text = time.ToString("00");
            yield return new WaitForSeconds(1);
            time--;
        }
        TrainOver();
    }

    IEnumerator CountThree()
    {
        //GameObject.Find("BackButton").SetActive(false);
        while (time2 > 0)
        {
            GameObject.Find("CountDownThreeText").GetComponent<Text>().text = time2.ToString();
            yield return new WaitForSeconds(1);
            time2--;
        }
        GameObject.Find("CountDownThreeText").GetComponent<Text>().text = "开始测评";
        yield return new WaitForSeconds(1);
        GameObject.Find("CountDownThreeText").GetComponent<Text>().text = "";
        GameObject.Find("PatientTrainPanel").GetComponent<evaluate>().StartToEvaluate();
    }

    void TrainOver()
    {
        GameObject.Find("TimeCountDownText").GetComponent<Text>().text = "训练完成";
        Messagebox.MessageBox(IntPtr.Zero, "您已完成一次训练！", "提示", 0);
        TrainingPlan.finishOnceTrainingPlan(PlayerPrefs.GetString("userID"),
            PlayerPrefs.GetString("selectStdActionID"), PlayerPrefs.GetInt("selectPlanHand"));
        TrainingPlan.finishOneDayTrainingPlan(PlayerPrefs.GetString("userID"));
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientCheckPlanPanel");
        GameObject.Find("PatientCheckPlanPanel").GetComponent<CreatePatientPlanTable>().Start();
    }

    public void CountDownThree()
    {
        //GameObject.Find("BackButton").SetActive(true);
        GameObject.Find("TimeCountDownText").SetActive(false);
        StartCoroutine(CountThree());
        GameObject.Find("CountDownThreeText").GetComponent<Text>().text =time2.ToString();
    }

}
