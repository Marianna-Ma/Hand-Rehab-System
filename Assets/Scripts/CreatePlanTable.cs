using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanToggleInfo
{
    public string id;
    public int hand;
    public bool status;
    public GameObject obj;
}

public class PlanButtonInfo
{
    public string id;
    public string actName;
    public int hand;
    public string day, time, span;
    public GameObject obj;
}

public class CreatePlanTable : MonoBehaviour {
    
    public DataSet currentTP;
    public GameObject PlanData_Prefab;//表头预设
    public List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();
    public GameObject table;
    public bool clicked=false;
    public InputField nameInput;

    public GameObject SmallPanel;

    void Awake()
    {
        table = GameObject.Find("Canvas/DoctorCheckPlanPanelNew");
    }

    public void Start()
    {
        GameObject table = GameObject.Find("Canvas/DoctorCheckPlanPanelNew/ScrollView/Viewport/Content");
        foreach (Transform t in table.GetComponentsInChildren<Transform>())
        {
            if (t.name.Contains("plan".ToLower()))
            {
                Destroy(t.gameObject);//删除之前的内容
            }
        }
        string ID = PlayerPrefs.GetString("selectPatientID");
		GameObject.Find ("Canvas/DoctorCheckPlanPanelNew/NameText").GetComponent<Text> ().text = ID;
        currentTP = TrainingPlan.findOnesTrainingPlan(ID); //TODO：获得病人编号
       
        if (currentTP.Tables[0].Rows.Count > 0)
        {
            CreateTable(currentTP);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            Debug.Log("Clicked");
            clicked = !clicked;
            GameObject table = GameObject.Find("Canvas/DoctorCheckPlanPanelNew/ScrollView/Viewport/Content");
            foreach(Transform t in table.GetComponentsInChildren<Transform>())
            {
                if (t.name.Contains("plan".ToLower()))
                {
                    Destroy(t.gameObject);//删除之前的内容
                }
            }

            currentTP = TrainingPlan.searchTrainingPlan(nameInput.text.ToString(), PlayerPrefs.GetString("selectPatientID"));
            if (currentTP.Tables[0].Rows.Count > 0)
            {
                CreateTable(currentTP);
            }
        }
    }

    void CreateTable(DataSet dataSet)
    {
        GameObject table = GameObject.Find("Canvas/DoctorCheckPlanPanelNew/ScrollView/Viewport/Content");
        GameObject plandata = GameObject.Find("Canvas/DoctorCheckPlanPanelNew/ScrollView/Viewport/Content/ActionPlanData");
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
            info.actName = (string)dataSet.Tables[0].Rows[i][2];
            info.hand = int.Parse(dataSet.Tables[0].Rows[i][3].ToString());
            info.day = dataSet.Tables[0].Rows[i][6].ToString();
            info.time = dataSet.Tables[0].Rows[i][4].ToString();
            info.span = dataSet.Tables[0].Rows[i][5].ToString();
            info.obj = row.transform.Find("ActionImageButton").GetComponent<Button>().gameObject;
            row.transform.Find("ActionImageButton").GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    selectAction(info);
                    GameObject.Find("Canvas/DoctorCheckPlanPanelNew/SearchButton").GetComponent<Button>().interactable = false;
                    GameObject.Find("Canvas/DoctorCheckPlanPanelNew/AddPlanButton").GetComponent<Button>().interactable = false;
                    GameObject.Find("Canvas/DoctorCheckPlanPanelNew/DeletePlanButton").GetComponent<Button>().interactable = false;
                    GameObject.Find("Canvas/DoctorCheckPlanPanelNew/BackButton").GetComponent<Button>().interactable = false;
                    //GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorEditPlanPanel");
                    SmallPanel.SetActive(true);
                    //SmallPanel.GetComponent<DoctorEditPlanPanel>().Start();
                    //GameObject obj = GameObject.Find("Canvas/DoctorEditPlanPanel");

                    DoctorEditPlanPanel panel = (DoctorEditPlanPanel)SmallPanel.GetComponent(typeof(DoctorEditPlanPanel));
                    panel.Start();
                }
                );
            row.transform.Find("ActionNameToggle").GetComponent<Toggle>().isOn = false;
            row.transform.Find("ActionNameToggle").Find("Label").GetComponent<Text>().text = (string)dataSet.Tables[0].Rows[i][2];
            PlanToggleInfo toggleInfo = new PlanToggleInfo();
            toggleInfo.id = dataSet.Tables[0].Rows[i][1].ToString();
            toggleInfo.hand = int.Parse(dataSet.Tables[0].Rows[i][3].ToString());
            toggleInfo.status = false;
            row.transform.Find("ActionNameToggle").GetComponent<Toggle>().onValueChanged.AddListener((value) => chooseAction(toggleInfo));
            row.transform.Find("Time").Find("TimeText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][5].ToString();
            row.transform.Find("Num").Find("NumText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][4].ToString();
            row.transform.Find("Day").Find("DayText").GetComponent<Text>().text = dataSet.Tables[0].Rows[i][6].ToString();
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
        PlayerPrefs.SetString("selectPlanDays", info.day);
        PlayerPrefs.SetString("selectPlanTimes", info.time);
        PlayerPrefs.SetString("selectPlanSpan", info.span);
    }

    void chooseAction(PlanToggleInfo info)
    {
        info.status = !info.status;
        if (info.status == true)
            selectActList.Add(info);
        else
            selectActList.Remove(info);
        Debug.Log("selectActList:--------------------");
        foreach (PlanToggleInfo tInfo in selectActList)
            Debug.Log(tInfo.id);
    }

    public void getSelectActList(List<PlanToggleInfo> newList)
    {
        foreach (PlanToggleInfo tInfo in selectActList)
        {
            Debug.Log(tInfo.id);
            newList.Add(tInfo);
        }
    }

}
