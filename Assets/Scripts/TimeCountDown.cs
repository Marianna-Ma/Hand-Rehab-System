using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour {
    public GameObject textField;
    GameObject leftHand;
    GameObject rightHand;
    GameObject recordSaver;
    SaveHandData handData;
    public GameObject AssistantCamera;
    bool allowEvaluate = true;

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

    public void CreateDataHands(string leftPath,string rightPath)
    {
        if (leftPath != "")
        {
            Debug.Log("Creating left hand");
            leftHand = (GameObject)Resources.Load("leftHand");
            leftHand = Instantiate(leftHand);
            leftHand.name = "datacontrolledlefthand";
            leftHand.GetComponent<DataControlledLeftHand>().Init(leftPath);
        }
        if (rightPath != "")
        {
            Debug.Log("Creating right hand");
            rightHand = (GameObject)Resources.Load("rightHand");
            rightHand = Instantiate(rightHand);
            rightHand.name = "datacontrolledrighthand";
            rightHand.GetComponent<DataControlledRightHand>().Init(rightPath);
        }
    }

    public void DestroyDataHands()
    {
        Debug.Log("Destroying---");
        Destroy(leftHand);
        Destroy(rightHand);
    }

    public void StartCountDown()
    {
        GameObject.Find("Canvas/PatientTrainPanel/ResultText").SetActive(false);
        GameObject.Find("Canvas").GetComponent<HandControl>().ShowHands();
        PlayerPrefs.SetString("patientHandDataPath", "0");
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
            GameObject.Find("Canvas/PatientTrainPanel/TimeCountDownText").GetComponent<Text>().text = time.ToString("00");
            yield return new WaitForSeconds(1);
            time--;
        }
        TrainOver();
    }

    IEnumerator CountThree()    //测评函数
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
        SaveRecord();
        yield return new WaitForSeconds(6);
        allowEvaluate = true;
    }

    IEnumerator UploadDatas()   //上传数据
    {
        Debug.Log("Start upload");
        OSSConnect.UploadFile(PlayerPrefs.GetString("patientHandDataPath"),
            Application.dataPath + "/" + PlayerPrefs.GetString("patientHandDataPath"));
        Debug.Log("Upload completed");
        yield return null;
    }

    void TrainOver()
    {
        DestroyDataHands();
        AssistantCamera.SetActive(false);
        GameObject.Find("TimeCountDownText").GetComponent<Text>().text = "训练完成";
        Messagebox.MessageBox(IntPtr.Zero, "您已完成一次训练！", "提示", 0);
        if (PlayerPrefs.GetString("patientHandDataPath") != "0" && handData.IsSaveCompleted())
        {
            GameObject.Find("PatientTrainPanel").GetComponent<evaluate>().AddRecTest();
            StartCoroutine(UploadDatas());
        }
        TrainingPlan.finishOnceTrainingPlan(PlayerPrefs.GetString("userID"),
            PlayerPrefs.GetString("selectStdActionID"), PlayerPrefs.GetInt("selectPlanHand"));
        TrainingPlan.finishOneDayTrainingPlan(PlayerPrefs.GetString("userID"));
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientCheckPlanPanel");
        GameObject.Find("PatientCheckPlanPanel").GetComponent<CreatePatientPlanTable>().Start();
    }

    public void CountDownThree()    //点击“评测”后执行
    {
        if (allowEvaluate)
        {
            allowEvaluate = false;
            //GameObject.Find("BackButton").SetActive(true);
            //GameObject.Find("TimeCountDownText").SetActive(false);
            StartCoroutine(CountThree());
            GameObject.Find("CountDownThreeText").GetComponent<Text>().text = time2.ToString();
        }
        else return;
    }

    void SaveRecord()
    {
        recordSaver = new GameObject();
        recordSaver.AddComponent<SaveHandData>();
        handData = (SaveHandData)recordSaver.GetComponent(typeof(SaveHandData));
        handData.Init(100, "/patient_datas/" + GenDataName());
        PlayerPrefs.SetString("patientHandDataPath", handData.savedDataPath.Substring(1));  //文件名不能以“/”开头
        Debug.Log("Ready to save data:" + handData.savedDataPath);
    }

    string GenDataName()
    {
        string time = DateTime.Now.ToString("yyyyMMddHHmmss");
        string dataName = PlayerPrefs.GetString("userID") + "_" + time + "_"
            + PlayerPrefs.GetString("selectStdActionID") + ".json";
        return dataName;
    }
}
