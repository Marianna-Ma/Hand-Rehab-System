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

public class CreatePlanTable : MonoBehaviour
{

    DataSet currentTP;
    public GameObject PlanData_Prefab;//表头预设
    public List<PlanToggleInfo> selectActList = new List<PlanToggleInfo>();

    void Awake()
    {

    }

    void Start()
    {
        currentTP = TrainingPlan.findOnesTrainingPlan("300001"); //TODO：获得病人编号
        if (currentTP.Tables[0].Rows.Count > 0)
        {
            GameObject table = GameObject.Find("Canvas/DoctorCheckPlanPanelNew/ScrollView/Viewport/Content");
            GameObject plandata = GameObject.Find("Canvas/DoctorCheckPlanPanelNew/ScrollView/Viewport/Content/ActionPlanData");

            plandata.SetActive(false);
            for (int i = 0; i < currentTP.Tables[0].Rows.Count; i++)//添加并修改预设的过程
            {
                //在Table下创建新的预设实例
                GameObject row = GameObject.Instantiate(PlanData_Prefab, table.transform.position, table.transform.rotation) as GameObject;
                row.name = "plan" + (i + 1);
                row.transform.SetParent(table.transform);
                row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                       //设置预设实例中的各个子物体的文本内容

                using (FileStream file = new FileStream(Application.dataPath +
                    "/StandardActionPic/" + currentTP.Tables[0].Rows[i][0] + ".jpg", FileMode.Open, FileAccess.Read))
                {
                    file.Seek(0, SeekOrigin.Begin);

                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    Texture2D texture = new Texture2D(250, 350);
                    texture.LoadImage(bytes);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    row.transform.Find("ActionImageButton").GetComponent<Image>().sprite = sprite;  //TODO：给按钮加图片
                }

                ButtonInfo info = new ButtonInfo();
                info.id = (string)currentTP.Tables[0].Rows[i][0];
                info.actName = (string)currentTP.Tables[0].Rows[i][1];
                info.obj = row.transform.Find("ActionImageButton").GetComponent<Button>().gameObject;
                row.transform.Find("ActionImageButton").GetComponent<Button>().onClick.AddListener(
                    delegate ()
                    {
                        selectAction(info);
                    }
                    );
                row.transform.Find("ActionNameToggle").GetComponent<Toggle>().isOn = false;
                row.transform.Find("ActionNameToggle").Find("Label").GetComponent<Text>().text = (string)currentTP.Tables[0].Rows[i][1];
                PlanToggleInfo toggleInfo = new PlanToggleInfo();
                toggleInfo.id = currentTP.Tables[0].Rows[i][0].ToString();
                toggleInfo.status = false;
                row.transform.Find("ActionNameToggle").GetComponent<Toggle>().onValueChanged.AddListener((value) => chooseAction(toggleInfo));
                row.transform.Find("Time").Find("TimeText").GetComponent<Text>().text = currentTP.Tables[0].Rows[i][3].ToString();
                row.transform.Find("Num").Find("NumText").GetComponent<Text>().text = currentTP.Tables[0].Rows[i][2].ToString();
                row.transform.Find("Day").Find("DayText").GetComponent<Text>().text = currentTP.Tables[0].Rows[i][4].ToString();
                row.SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    void selectAction(ButtonInfo info)
    {
        Debug.Log("info: " + info.id);
        Debug.Log("action Name: " + info.actName);
        PlayerPrefs.SetString("selectStdActionID", info.id);
        PlayerPrefs.SetString("selectStdActionName", info.actName);
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
