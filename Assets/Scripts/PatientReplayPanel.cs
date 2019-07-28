using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientReplayPanel : MonoBehaviour {

    string replayJson;

	// Use this for initialization
	void Start () {
        string path = Application.dataPath + "/StandardActionLibrary/4000010.json";
        PlayerPrefs.SetString("replayJson", path);
        replayJson = PlayerPrefs.GetString("replayJson");
	}

    private void OnEnable()
    {
        Debug.Log("双手显示");
        GameObject.Find("Canvas").GetComponent<HandControl>().ShowHands();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ClickBackButton()
    {
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("PatientHistoryRecordPanel");
    }
}
