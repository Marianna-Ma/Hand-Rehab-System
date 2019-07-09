using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class figureEstimate : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.pointerPress.name == "confirmButton") {
			string[] group1 = { "fingerStraight", "fingerBend", "fingerKnead", "fingerOpening" };
			string[] group2 = { "ThumbToggle", "IndexToggle", "MiddleToggle", "RingToggle", "PinkyToggle" };
			string[] group3 = { "ThumbIndexToggle", "ThumbMiddleToggle", "ThumbRingToggle", "ThumbPinkyToggle", "IndexMiddleToggle", "IndexRingToggle", "IndexPinkyToggle", "MiddleRingToggle", "MiddlePinkyToggle", "RingPinkyToggle" };
			string[] group4 = { "ThumbIndexToggle", "IndexMiddleToggle", "MiddleRingToggle", "RingPinkyToggle"};
			string res = "";
			string path = "DoctorEstimatePanel/";
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
		}
	}
}
