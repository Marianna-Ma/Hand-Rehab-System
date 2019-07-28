using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class CreateAdminPatientPanel : MonoBehaviour {

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

    DataTable patientTable;
    Admin admin;
    public GameObject PatientData_Prefab;

    void Awake()
    {

    }

    // Use this for initialization
    public void Start()
    {
        GameObject table = GameObject.Find("Canvas/AdminCheckPatientPanel/ScrollView/Viewport/Content");
        foreach (Transform t in table.GetComponentsInChildren<Transform>())
        {
            if (t.name.Contains("patient".ToLower()))
            {
                Destroy(t.gameObject);//删除之前的内容
            }
        }
        PlayerPrefs.SetString("userID", "100001");
        if (PlayerPrefs.HasKey("userID"))
        {
            admin = new Admin(host, port, userName, password, databaseName);
            string dc_ID = PlayerPrefs.GetString("selectDoctorID");
            patientTable = admin.QueryPatient(dc_ID);
            //patientTable = admin.QueryPatient("200001");
        }
        else patientTable = null;
        CreateTable(patientTable);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateTable(DataTable patientTable)
    {
        GameObject table = GameObject.Find("Canvas/AdminCheckPatientPanel/ScrollView/Viewport/Content");
        GameObject patientdata = GameObject.Find("Canvas/AdminCheckPatientPanel/ScrollView/Viewport/Content/AdminPatientData");
        if (patientdata != null) patientdata.SetActive(false);
        for (int i = 0; i < patientTable.Rows.Count; i++)
        {
            GameObject row = GameObject.Instantiate(PatientData_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "patient" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("PatientID").GetComponent<Text>().text = patientTable.Rows[i][0].ToString();
            row.transform.Find("PatientName").Find("PatientNameText").GetComponent<Text>().text = patientTable.Rows[i][1].ToString();
            ButtonInfo info = new ButtonInfo();
            info.id = (string)patientTable.Rows[i][0];
            info.actName = (string)patientTable.Rows[i][1];
            info.obj = row.transform.Find("PatientName").GetComponent<Button>().gameObject;
            row.transform.Find("PatientName").GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    selectAction(info);
                    GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminCheckPlanPanel");
                    GameObject obj = GameObject.Find("Canvas/AdminCheckPlanPanel");
                    CreateAdminPlanTable panel = (CreateAdminPlanTable)obj.GetComponent(typeof(CreateAdminPlanTable));
                    panel.Start();
                }
                );
            row.transform.Find("PatientSex").GetComponent<Text>().text = patientTable.Rows[i][2].ToString();
            row.transform.Find("PatientTele").GetComponent<Text>().text = patientTable.Rows[i][3].ToString();
            row.SetActive(true);
        }
    }

    void selectAction(ButtonInfo info)
    {
        Debug.Log("Clicked");
        PlayerPrefs.SetString("selectPatientID", info.id);
        PlayerPrefs.SetString("selectPatientName", info.actName);
    }
}

