using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
/*
public class ToggleInfo
{
	public string id;
	public bool status;
	public GameObject obj;
}
*/

public class PatientUI : MonoBehaviour, IPointerClickHandler {
	// 修改密码 面板的输入框
	public InputField pt_firstPswdField;
	public InputField pt_secondPswdField;
	// 修改信息 面板的输入框
	public InputField ptNameField;
	public InputField ptSexField;
	public InputField ptTeleField;
	// 查看历史记录
	public Text startDateText;
	public Text endDateText;
	public Text noRecord;
	public GameObject historyRecordsPrefab;//表头预设
	public List<string> selectRecordList = new List<string>(); // 保存哪些toggle被选择
//	public int toggleNum = 0;
//	public string toggleName = "";

	public static string host = "119.3.231.171";		//IP地址
	public static string port = "3306";					//端口号
	public static string userName = "admin";			//用户名
	public static string password = "Rehabsys@2019";	//密码
	public static string databaseName = "rehabsys";		//数据库名称

	public Patient test;

	// Use this for initialization
	void Start () {
		test = new Patient(host, port, userName, password, databaseName);
		PlayerPrefs.SetInt ("selectRecordNum", 0);
		PlayerPrefs.SetString ("selectRecord", "");
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "updatePswdButton")	//如果当前按下的按钮是修改密码按钮
			test.UpdatePassword (pt_firstPswdField.text.ToString(), pt_secondPswdField.text.ToString());
		if (eventData.pointerPress.name == "changeInfoButton")
			test.ChangeInfo (ptNameField.text.ToString(), ptSexField.text.ToString(), ptTeleField.text.ToString());
		if (eventData.pointerPress.name == "selectDateButton") {
			string startDate = startDateText.text.ToString ();
			string endDate = endDateText.text.ToString ();
			string startDateString = changeDateFormat (startDate);
			string endDateString = changeDateFormat (endDate);
			string[,] records = test.SelectRecords (startDateString, endDateString);


			GameObject table = GameObject.Find ("Canvas/PatientHistoryRecordPanel/selectableRecordsScrollView/Viewport/Content");
			GameObject selectableRecord = GameObject.Find ("Canvas/PatientHistoryRecordPanel/selectableRecordsScrollView/Viewport/Content/selectableRecord");
			GameObject noRecord = GameObject.Find ("Canvas/PatientHistoryRecordPanel/selectableRecordsScrollView/Viewport/Content/noRecord");
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
					
					if (Convert.ToDouble (records [i, 4]) < 0.5) {
						row.transform.Find ("estimateValue").GetComponent<Text> ().text = "差";
					} else if (Convert.ToDouble (records [i, 4]) < 0.75 && Convert.ToDouble (records [i, 4]) >= 0.5) {
						row.transform.Find ("estimateValue").GetComponent<Text> ().text = "可";
					} else if (Convert.ToDouble (records [i, 4]) < 0.9 && Convert.ToDouble (records [i, 4]) >= 0.75) {
						row.transform.Find ("estimateValue").GetComponent<Text> ().text = "良";
					} else if (Convert.ToDouble (records [i, 4]) >= 0.9) {
						row.transform.Find ("estimateValue").GetComponent<Text> ().text = "优";
					}

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
//			Debug.Log ("===================================");
//			getSelectRecordList ();


//			for (int i = 0; i < 10; i++)//添加并修改预设的过程，将创建10行
//			{
//				noRecord.SetActive (false);
//				Debug.Log(i);
//				//在Table下创建新的预设实例
//				//GameObject table = GameObject.Find("Canvas/DoctorCheckPatientPanel/ScrollView/Viewport/Content");
//				//Debug.Log(table.name);
//				GameObject row = GameObject.Instantiate(historyRecordsPrefab, table.transform.position, table.transform.rotation) as GameObject;
//				row.name = "row" + (i + 1);
//				row.transform.SetParent(table.transform);
//				row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
//				//设置预设实例中的各个子物体的文本内容
//				row.transform.Find("recordDate").GetComponent<Text>().text = "20190708";
//				row.transform.Find("actionID").GetComponent<Text>().text = "400001";
//				row.transform.Find("actionName").GetComponent<Text>().text = "握拳";
//				row.transform.Find("handIndex").GetComponent<Text>().text = "左手";
//
//				row.SetActive(true);
//				row.transform.Translate(230, -15-i*16, 0);
//			}

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
                PlayerPrefs.SetString("replayJson", res);
                GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientReplayPanel");
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

	public void getSelectRecordList(List<string> newList) {
		Debug.Log ("*************" + selectRecordList.Count);
		foreach (string info in selectRecordList) {
			Debug.Log (info);
			newList.Add (info);
		}
	}
}
