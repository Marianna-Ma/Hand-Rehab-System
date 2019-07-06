using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class CreatePatientPanel : MonoBehaviour
{
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
    Doctor doctor;
    public GameObject PatientData_Prefab;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetString("userID", "200001");
        if (PlayerPrefs.HasKey("userID"))
        {
            doctor = new Doctor(host, port, userName, password, databaseName);
            patientTable = doctor.QueryPatient();
        }
        else patientTable = null;

        GameObject table = GameObject.Find("Canvas/DoctorCheckPatientPanel/ScrollView/Viewport/Content");
        GameObject patientdata = GameObject.Find("Canvas/DoctorCheckPatientPanel/ScrollView/Viewport/Content/PatientData");
        patientdata.SetActive(false);
        for (int i = 0; i < patientTable.Rows.Count; i++)
        {
            GameObject row = GameObject.Instantiate(PatientData_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "patient" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
            row.transform.Find("PatientID").GetComponent<Text>().text = (string)patientTable.Rows[i][0];
            row.transform.Find("PatientName").Find("PatientNameText").GetComponent<Text>().text = (string)patientTable.Rows[i][1];
            row.transform.Find("PatientSex").GetComponent<Text>().text = (string)patientTable.Rows[i][2];
            row.transform.Find("PatientTele").GetComponent<Text>().text = (string)patientTable.Rows[i][3];
            row.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}