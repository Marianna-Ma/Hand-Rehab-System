using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;



public class createHistoryRecordTable : MonoBehaviour, IPointerClickHandler {
	List<string> selectRecordList = new List<string> ();

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "historyRecordButton") {
//			GameObject.Find ("Canvas/PatientHistoryRecordPanel").GetComponent<PatientUI> ().getSelectRecordList (selectRecordList);
//			Debug.Log ("%%%%%%%%%%%  " + selectRecordList.Count);
//			foreach (string info in selectRecordList)
//				Debug.Log (info);

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
		}

	}
}
