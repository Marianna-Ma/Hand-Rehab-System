using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class CreateDocPanel : MonoBehaviour {

    //IP地址
    public static string host = "119.3.231.171";
    //端口号
    public static string port = "3306";
    //用户名
    public static string userName = "admin";
    //密码
    public static string password = "Rehabsys@2019";
    //数据库名称
    public static string databaseName = "rehabsys";

    DataTable docTable;
    Admin admin;
    List<string> selectActList = new List<string>();
    public GameObject DocData_Prefab;

    void Awake()
    {

    }

    // Use this for initialization
    public void Start()
    {
        GameObject table = GameObject.Find("Canvas/AdminCheckDoctorPanel/ScrollView/Viewport/Content");
        foreach (Transform t in table.GetComponentsInChildren<Transform>())
        {
            if (t.name.Contains("doctor".ToLower()))
            {
                Destroy(t.gameObject);//删除之前的内容
            }
        }
        PlayerPrefs.SetString("userID", "100001");
        if (PlayerPrefs.HasKey("userID"))
        {
            admin = new Admin(host, port, userName, password, databaseName);
            docTable = admin.QueryDoctor();
        }
        else docTable = null;
        CreateTable(docTable);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateTable(DataTable docTable)
    {
        GameObject table = GameObject.Find("Canvas/AdminCheckDoctorPanel/ScrollView/Viewport/Content");
        GameObject docdata = GameObject.Find("Canvas/AdminCheckDoctorPanel/ScrollView/Viewport/Content/DocData");
        if (docdata != null) docdata.SetActive(false);
        for (int i = 0; i < docTable.Rows.Count; i++)
        {
            GameObject row = GameObject.Instantiate(DocData_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "doctor" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("DocIDToggle").GetComponent<Toggle>().isOn = false;
            row.transform.Find("DocIDToggle").Find("Label").GetComponent<Text>().text = (string)docTable.Rows[i][0];
            row.transform.Find("DocName").Find("DocNameText").GetComponent<Text>().text = (string)docTable.Rows[i][1];
            ButtonInfo info = new ButtonInfo();
            info.id = (string)docTable.Rows[i][0];
            info.actName = (string)docTable.Rows[i][1];
            info.obj = row.transform.Find("DocName").GetComponent<Button>().gameObject;
            row.transform.Find("DocName").GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    selectAction(info);
                    GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminCheckPatientPanel");
                    GameObject obj = GameObject.Find("Canvas/AdminCheckPatientPanel");
                    CreateAdminPatientPanel panel = (CreateAdminPatientPanel)obj.GetComponent(typeof(CreateAdminPatientPanel));
                    panel.Start();

                }
                );
            row.transform.Find("DocSex").GetComponent<Text>().text = (string)docTable.Rows[i][2];
            row.transform.Find("DocPro").GetComponent<Text>().text = (string)docTable.Rows[i][3];
            row.transform.Find("DocTele").GetComponent<Text>().text = (string)docTable.Rows[i][4];
            ToggleInfo toggleInfo = new ToggleInfo();
            toggleInfo.id = (string)docTable.Rows[i][0];
            toggleInfo.status = false;
            row.transform.Find("DocIDToggle").GetComponent<Toggle>().onValueChanged.AddListener((value) => chooseAction(toggleInfo));
            row.SetActive(true);
        }
    }

    void selectAction(ButtonInfo info)
    {
        Debug.Log("Clicked");
        PlayerPrefs.SetString("selectDoctorID", info.id);
        PlayerPrefs.SetString("selectDoctorName", info.actName);
    }

    void chooseAction(ToggleInfo info)
    {
        info.status = !info.status;
        Debug.Log("info id: " + info.id);
        Debug.Log("info status: " + info.status);
        if (info.status == true)
            selectActList.Add(info.id);
        else
            selectActList.Remove(info.id);
        foreach (string id in selectActList)
            Debug.Log(id);
    }

    public void getSelectActList(List<string> newList)
    {
        Debug.Log("*******************************");
        foreach (string id in selectActList)
        {
            Debug.Log(id);
            newList.Add(id);

        }

    }
}
