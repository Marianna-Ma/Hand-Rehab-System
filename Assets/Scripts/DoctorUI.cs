using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class DoctorUI : MonoBehaviour, IPointerClickHandler {
	// 修改密码 面板的输入框
	public InputField dc_firstPswdField;
	public InputField dc_secondPswdField;
	// 修改信息 面板的输入框
	public InputField dcNameField;
	public InputField dcSexField;
	public InputField dcProField;
	public InputField dcTeleField;
	// 添加患者 面板的输入框
	public InputField ptIDField;
	public InputField ptNameField;
	public InputField ptSexField;
	public InputField ptTeleField;

	// 查看历史记录
	public Text startDateText;
	public Text endDateText;
	public Text noRecord;
	public GameObject historyRecordsPrefab;//表头预设
	public List<string> selectRecordList = new List<string>(); // 保存哪些toggle被选择


	public static string host = "119.3.231.171";		//IP地址
	public static string port = "3306";					//端口号
	public static string userName = "admin";			//用户名
	public static string password = "Rehabsys@2019";	//密码
	public static string databaseName = "rehabsys";		//数据库名称

	public Doctor test;

	// Use this for initialization
	void Start () {
		test = new Doctor(host, port, userName, password, databaseName);
		PlayerPrefs.SetInt ("selectRecordNum", 0);
		PlayerPrefs.SetString ("selectRecord", "");
	}
	
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "updatePswdButton")	//如果当前按下的按钮是修改密码按钮
			test.UpdatePassword (dc_firstPswdField.text.ToString(), dc_secondPswdField.text.ToString());
		if (eventData.pointerPress.name == "changeInfoButton")
			test.ChangeInfo (dcNameField.text.ToString(), dcSexField.text.ToString(), dcProField.text.ToString(), dcTeleField.text.ToString());
        if (eventData.pointerPress.name == "addPatientButton")  //如果当前按下的按钮是添加患者按钮
        {
            int status = test.AddPatient(ptIDField.text.ToString(), ptNameField.text.ToString(), ptSexField.text.ToString(), ptTeleField.text.ToString());
            if(status == 1)
                Messagebox.MessageBox(IntPtr.Zero, "患者账号或姓名不能为空！", "失败", 0);
            else if(status == 2)
                Messagebox.MessageBox(IntPtr.Zero, "患者账号不在人员表中！", "失败", 0);
            else if(status == 3)
                Messagebox.MessageBox(IntPtr.Zero, "患者账号已存在！", "失败", 0);
            else 
                Messagebox.MessageBox(IntPtr.Zero, "添加患者成功！", "成功", 0);
            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorAddPatientPanel");

        }
        if (eventData.pointerPress.name == "deletePatientButton")	//如果当前按下的按钮是删除患者按钮
			test.DeletePatient ();

		if (eventData.pointerPress.name == "selectDateButton") {
			string startDate = startDateText.text.ToString ();
			string endDate = endDateText.text.ToString ();
			string startDateString = changeDateFormat (startDate);
			string endDateString = changeDateFormat (endDate);
			string[,] records = test.SelectRecords (startDateString, endDateString);


			GameObject table = GameObject.Find ("Canvas/DoctorHistoryRecordPanel/selectableRecordsScrollView/Viewport/Content");
			GameObject selectableRecord = GameObject.Find ("Canvas/DoctorHistoryRecordPanel/selectableRecordsScrollView/Viewport/Content/selectableRecord");
			GameObject noRecord = GameObject.Find ("Canvas/DoctorHistoryRecordPanel/selectableRecordsScrollView/Viewport/Content/noRecord");
			selectableRecord.SetActive (false);
			noRecord.SetActive (true);
			Debug.Log ("row " + records.GetLength(0));
			Debug.Log ("col " + (records.GetUpperBound (records.Rank - 1) + 1));
			if (records.GetUpperBound (records.Rank - 1) + 1 == 5) {
				noRecord.SetActive (false);
				for (int i = 0; i < records.GetLength (0); i++) {
					GameObject row = GameObject.Instantiate (historyRecordsPrefab, table.transform.position, table.transform.rotation) as GameObject;
					row.name = "record" + (i + 1);
					row.transform.SetParent (table.transform);
					row.transform.localScale = Vector3.one;
					ToggleInfo toggleInfo = new ToggleInfo();
					toggleInfo.id = records[i, 0] + records[i,1] + records[i,3];
					toggleInfo.status = false;
					row.transform.Find("Toggle").GetComponent<Toggle>().onValueChanged.AddListener((value) => chooseRecord(toggleInfo));
					row.transform.Find ("recordDate").GetComponent<Text> ().text = records [i, 0];
					row.transform.Find ("actionID").GetComponent<Text> ().text = records [i, 1];
					row.transform.Find ("actionName").GetComponent<Text> ().text = records [i, 2];
					if (records [i, 3] == "0")
						row.transform.Find ("handIndex").GetComponent<Text> ().text = "左手";
					else
						row.transform.Find ("handIndex").GetComponent<Text> ().text = "右手";
					row.transform.Find ("estimateValue").GetComponent<Text> ().text = records [i, 4];
					row.transform.Translate (230, -15 - i * 25, 0);
					row.SetActive (true);

				}
			} else {
				noRecord.SetActive (true);
			}

			Debug.Log ("111111111111111111111111111111111111111111111111");
			Debug.Log (selectRecordList.Count);
			foreach (string id in selectRecordList)
				Debug.Log (id);

		}

		if (eventData.pointerPress.name == "historyRecordButton") {
//			test.HistoryRecord ("20190620", "400001", 0);
			Debug.Log ("2222222222222222222222222222222222");
			int recordNum = 0;
			if (PlayerPrefs.HasKey("selectRecordNum")) {
				recordNum = PlayerPrefs.GetInt ("selectRecordNum");
			}
			Debug.Log (recordNum.ToString());
			string record = "";
			if (PlayerPrefs.HasKey ("selectRecord")) {
				record = PlayerPrefs.GetString ("selectRecord");
			}
			Debug.Log (record);

			if (recordNum == 1) {
				string date = record [0].ToString() + record [1].ToString() + record [2].ToString() + record [3].ToString() + record [4].ToString() + record [5].ToString() + record [6].ToString() + record [7].ToString();
				string actID = record [8].ToString() + record [9].ToString() + record [10].ToString() + record [11].ToString() + record [12].ToString() + record [13].ToString();
				int hand = Convert.ToInt32 (record[14].ToString());
				string res = test.HistoryRecord (date, actID, hand);
			} else {
				Debug.Log ("只能选择一条记录，请重新选择");
			}
		}

	}

	private string changeDateFormat(string initDate) {
		int index_year = -1;
		int index_month = -1;
		int index_day = -1;
		for (int i = 0; i < initDate.Length; i++) {
			if (initDate [i] == '年')
				index_year = i;
			else if (initDate [i] == '月')
				index_month = i;
			else if (initDate [i] == '日')
				index_day = i;
		}
		string retDate = initDate[0].ToString() + initDate[1].ToString() + initDate[2].ToString() + initDate[3].ToString();
		if (index_month - index_year == 2)
			retDate += "0" + initDate [index_year + 1].ToString ();
		else
			retDate += initDate [index_year + 1].ToString () + initDate [index_year + 2].ToString ();
		if (index_day - index_month == 2)
			retDate += "0" + initDate [index_month + 1].ToString ();
		else
			retDate += initDate [index_month + 1].ToString () + initDate [index_month + 2].ToString ();
		Debug.Log (retDate);
		return retDate;
	}

	void chooseRecord(ToggleInfo info)
	{
		info.status = !info.status;
		Debug.Log("info info: " + info.id);
		Debug.Log("info status: " + info.status);
		if (info.status == true) {
			selectRecordList.Add (info.id);

			int num = 0;
			if (PlayerPrefs.HasKey("selectRecordNum")) {
				num = PlayerPrefs.GetInt ("selectRecordNum") + 1;
			}
			PlayerPrefs.SetInt ("selectRecordNum", num);
			PlayerPrefs.SetString ("selectRecord", info.id);


		} else {
			selectRecordList.Remove(info.id);

			int num = 0;
			if (PlayerPrefs.HasKey("selectRecordNum")) {
				num = PlayerPrefs.GetInt ("selectRecordNum") - 1;
			}
			PlayerPrefs.SetInt ("selectRecordNum", num);

		}
		Debug.Log ("aaa列表长度" + selectRecordList.Count);
		foreach (string id in selectRecordList)
			Debug.Log(id);
	}
}
