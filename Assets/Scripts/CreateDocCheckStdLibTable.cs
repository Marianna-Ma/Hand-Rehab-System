using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DocButtonInfo
{
    public string id;
    public string actName;
    public GameObject obj;
}

public class CreateDocCheckStdLibTable : MonoBehaviour {

    public GameObject PlanData_Prefab;//表头预设
    StandardActionLibrary actionLib;
    string localPicPath;
    string[,] actionTable;
    string patientName = "";

    // Use this for initialization
    public void Start () {
        //PlayerPrefs.SetString("selectPatientID", "李四");
        patientName = PlayerPrefs.GetString("selectPatientID");
        GameObject.Find("Canvas/DoctorCheckStdActionLibraryPanel/NameText").GetComponent<Text>().text = patientName;

        GameObject table = GameObject.Find("Canvas/DoctorCheckStdActionLibraryPanel/ScrollView/Viewport/Content");
        foreach (Transform t in table.GetComponentsInChildren<Transform>())
        {
            if (t.name.Contains("act".ToLower()))
            {
                Destroy(t.gameObject);//删除之前的内容
            }
        }
        localPicPath = Application.dataPath + "/StandardActionPic/";
        actionLib = new StandardActionLibrary();
        actionTable = actionLib.findStandardActions();

        if (actionTable[0, 0] != "null")
        {
            CreateTable(actionTable);
        }
    }
	
    void CreateTable(string[,] actionTable)
    {
        GameObject table = GameObject.Find("Canvas/DoctorCheckStdActionLibraryPanel/ScrollView/Viewport/Content");
        GameObject plandata = GameObject.Find("Canvas/DoctorCheckStdActionLibraryPanel/ScrollView/Viewport/Content/DocCheckLib");
        plandata.SetActive(false);

        //Debug.Log("======================================================");
        //Debug.Log(actionTable.Length);
        //actionLib.showFindActions(actionTable);
        for (int i = 0; i < actionTable.GetLength(0); i++)//添加并修改预设的过程，将创建10行
        {
            Debug.Log(i);
            //在Table下创建新的预设实例
            GameObject row = GameObject.Instantiate(PlanData_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "act" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                   //设置预设实例中的各个子物体的文本内容
                                                   //row.transform.Find("ActionName").GetComponent<Text>().text = "动作" + (i + 1);
            using (FileStream file = new FileStream(Application.dataPath +
                "/StandardActionPic/" + actionTable[i, 2], FileMode.Open, FileAccess.Read))
            {
                file.Seek(0, SeekOrigin.Begin);

                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                Texture2D texture = new Texture2D(250, 350);
                texture.LoadImage(bytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                row.transform.Find("ActionImageButton").GetComponent<Image>().sprite = sprite;

                DocButtonInfo info = new DocButtonInfo();
                info.id = actionTable[i, 0];
                info.actName = actionTable[i, 1];
                info.obj = row.transform.Find("ActionImageButton").GetComponent<Button>().gameObject;
                row.transform.Find("ActionImageButton").GetComponent<Button>().onClick.AddListener(
                    delegate ()
                    {
                        docSelectAction(info);
                        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorSetPlanPanel");
                        GameObject obj = GameObject.Find("Canvas/DoctorSetPlanPanel");
                        obj.GetComponent<DoctorSetPlanPanel>().Start();
                    }
                    );
            }

            row.transform.Find("Text").GetComponent<Text>().text = actionTable[i, 1];
            row.SetActive(true);
        }
    }

    void docSelectAction(DocButtonInfo info)
    {
        Debug.Log("info: " + info.id);
        Debug.Log("action Name: " + info.actName);
        PlayerPrefs.SetString("selectStdActionID", info.id);
        PlayerPrefs.SetString("selectStdActionName", info.actName);
        GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("DoctorSetPlanPanel");

    }

	// Update is called once per frame
	void Update () {
		
	}
}
