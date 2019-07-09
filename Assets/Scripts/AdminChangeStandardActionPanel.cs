using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminChangeStandardActionPanel : MonoBehaviour {
    public int time = 3;
    GameObject obj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        //CountDownThree();
    }

    public void ClickTranscribeButton()
    {
        CountDownThree();
    }

    public void CountDownThree()
    {
        StartCoroutine(CountThree());
        GameObject.Find("ChangeCountDownText").GetComponent<Text>().text = time.ToString();
    }

    IEnumerator CountThree()
    {
        while (time > 0)
        {
            GameObject.Find("ChangeCountDownText").GetComponent<Text>().text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        GameObject.Find("ChangeCountDownText").GetComponent<Text>().text = "开始记录标准动作";
        //记录标准动作
        string hand_type = PlayerPrefs.GetString("handtype");
        string act_id = PlayerPrefs.GetString("newActionID");
        Debug.Log(hand_type + " " + act_id);
        obj = new GameObject();
        obj.AddComponent<StandardActionLibrary>();
        StandardActionLibrary stdlibrary = (StandardActionLibrary)obj.GetComponent(typeof(StandardActionLibrary));
        stdlibrary.saveStandardAction(act_id, hand_type);

    }
}
