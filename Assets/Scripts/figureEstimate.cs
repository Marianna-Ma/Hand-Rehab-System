using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System;

public class figureEstimate : MonoBehaviour, IPointerClickHandler {
	public static string host = "119.3.231.171";		//IP地址
	public static string port = "3306";					//端口号
	public static string userName = "admin";			//用户名
	public static string password = "Rehabsys@2019";	//密码
	public static string databaseName = "rehabsys";		//数据库名称

	//封装好的数据库类
	MySqlAccess mysql;

    public GameObject DistancePanel;

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "confirmButton") {
			string[] group1 = { "fingerStraight", "fingerBend", "fingerKnead", "fingerOpening" };
			string[] group2 = { "ThumbToggle", "IndexToggle", "MiddleToggle", "RingToggle", "PinkyToggle" };
			string[] group3 = { "ThumbIndexToggle", "ThumbMiddleToggle", "ThumbRingToggle", "ThumbPinkyToggle", "IndexMiddleToggle", "IndexRingToggle", "IndexPinkyToggle", "MiddleRingToggle", "MiddlePinkyToggle", "RingPinkyToggle" };
			string[] group4 = { "ThumbIndexToggle", "IndexMiddleToggle", "MiddleRingToggle", "RingPinkyToggle"};
			string res = "";
			string path = "AdminEstimatePanel/";
			for (int i = 0; i < 4; i++) {
				string path1 = path + group1 [i] + "/ToggleGroup/";
				if (i == 0 || i == 1) {
					for (int j = 0; j < 5; j++) {
						string path2 = path1 + group2 [j];
//						Debug.Log ("aaa  " + path2);
						int flag = GameObject.Find (path2).GetComponent<Toggle> ().isOn ? 1 : 0;
						res += flag.ToString ();
					}
				} else if (i == 2) {
					for (int j = 0; j < 10; j++) {
						string path2 = path1 + group3 [j];
//						Debug.Log ("bbb  " + path2);
						int flag = GameObject.Find (path2).GetComponent<Toggle> ().isOn ? 1 : 0;
						res += flag.ToString ();
					}
				} else if (i == 3) {
					for (int j = 0; j < 4; j++) {
						string path2 = path1 + group4 [j];
//						Debug.Log ("ccc  " + path2);
						int flag = GameObject.Find (path2).GetComponent<Toggle> ().isOn ? 1 : 0;
						res += flag.ToString ();
					}
				}
			}
			Debug.Log ("res   " + res);

			//PlayerPrefs.SetString ("newActionID", "400005");
			string newActionID = "";
			if (PlayerPrefs.HasKey("newActionID")) {
				newActionID = PlayerPrefs.GetString ("newActionID");
			}
			Debug.Log ("newActionID" + newActionID);

			mysql = new MySqlAccess(host, port, userName, password, databaseName);
			mysql.OpenSql ();
			string query = "insert into est values('" + newActionID + "', '" + res + "')";
			mysql.QuerySet (query);
			Debug.Log (query);
			mysql.Close ();
            Messagebox.MessageBox(IntPtr.Zero, "添加评测方式成功！", "成功", 0);
            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("CheckStdActionLibraryPanel");
            DistancePanel.GetComponent<CreateStdActionTable>().Start();
        }
	}
}
