using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreatePatientPlanTable : MonoBehaviour {

    public DataSet currentTP;
    public GameObject PlanData_Prefab;//表头预设

    public GameObject AssistantCamera;

    public void Start()
    {
        GameObject table = GameObject.Find("Canvas/PatientCheckPlanPanel/ScrollView/Viewport/Content");
        foreach (Transform t in table.GetComponentsInChildren<Transform>())
        {
            if (t.name.Contains("plan".ToLower()))
            {
                Destroy(t.gameObject);//删除之前的内容
            }
        }
        //string pt_ID = PlayerPrefs.GetString("selectPatientID");
        //currentTP = TrainingPlan.findUnfinishedTrainingPlan(pt_ID); //TODO：获得病人编号
        currentTP = TrainingPlan.findUnfinishedTrainingPlan("300001");

        if (currentTP.Tables[0].Rows.Count > 0)
        {
            CreateTable(currentTP);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateTable(DataSet dataSet)
    {
        GameObject table = GameObject.Find("Canvas/PatientCheckPlanPanel/ScrollView/Viewport/Content");
        GameObject plandata = GameObject.Find("Canvas/PatientCheckPlanPanel/ScrollView/Viewport/Content/PatientPlanData");
        plandata.SetActive(false);

        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)//添加并修改预设的过程
        {
            //在Table下创建新的预设实例
            GameObject row = GameObject.Instantiate(PlanData_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "plan" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                   //设置预设实例中的各个子物体的文本内容

            using (FileStream file = new FileStream(Application.dataPath +
                "/StandardActionPic/" + dataSet.Tables[0].Rows[i][1] + ".jpg", FileMode.Open, FileAccess.Read))
            {
                file.Seek(0, SeekOrigin.Begin);

                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                Texture2D texture = new Texture2D(250, 350);
                texture.LoadImage(bytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                row.transform.Find("ActionImageButton").GetComponent<Image>().sprite = sprite;  //TODO：给按钮加图片
            }

            PlanButtonInfo info = new PlanButtonInfo();
            info.id = (string)dataSet.Tables[0].Rows[i][1];
            info.actName = dataSet.Tables[0].Rows[i][2].ToString();
            info.hand = int.Parse(dataSet.Tables[0].Rows[i][3].ToString());
            info.span = dataSet.Tables[0].Rows[i][5].ToString();
            info.obj = row.transform.Find("ActionImageButton").GetComponent<Button>().gameObject;
            row.transform.Find("ActionImageButton").GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    selectAction(info);
                    GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientTrainPanel");
                    AssistantCamera.SetActive(true);
                    GameObject.Find("PatientTrainPanel/TrainTestButton").GetComponent<TimeCountDown>().StartCountDown();
                    string left = "", right = "";
                    if (info.hand == 0) left = "/StandardActionLibrary/" + info.id + "0.json";
                    else right = "/StandardActionLibrary/" + info.id + "1.json";
                    Debug.Log(left + "\n" + right);
                    GameObject.Find("PatientTrainPanel/TrainTestButton").GetComponent<TimeCountDown>()
                    .CreateDataHands(left, right);
                }
                );
            string name = dataSet.Tables[0].Rows[i][2].ToString();
            if ("0" == dataSet.Tables[0].Rows[i][3].ToString()) name += "(左手)";
            else name += "(右手)";
            row.transform.Find("ActionNameText").GetComponent<Text>().text = name;
            row.transform.Find("Time").Find("TimeText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][5].ToString();
            row.transform.Find("NumTotal").Find("NumText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][4].ToString();
            row.transform.Find("DayTotal").Find("DayText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][6].ToString();
            row.transform.Find("Num").Find("NumText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][8].ToString();
            row.transform.Find("Day").Find("DayText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][7].ToString();
            row.SetActive(true);
        }
    }

    void selectAction(PlanButtonInfo info)
    {
        Debug.Log("info: " + info.id);
        Debug.Log("action Name: " + info.actName);
        PlayerPrefs.SetString("selectStdActionID", info.id);
        PlayerPrefs.SetString("selectStdActionName", info.actName);
        PlayerPrefs.SetInt("selectPlanHand", info.hand);
        PlayerPrefs.SetString("selectPlanSpan", info.span);
    }
}
