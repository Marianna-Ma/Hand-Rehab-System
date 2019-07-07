using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo
{
    public string id;
    public string actName;
    public GameObject obj;
}


public class CreateStdActionTable : MonoBehaviour {

    public GameObject PlanData_Prefab;//表头预设
    public List<Button> btnArray = new List<Button>();
    string[,] actionTable;
    StandardActionLibrary actionLib;
    string localPicPath;
    Transform root;

    // Use this for initialization
    void Start () {
        localPicPath = Application.dataPath + "/StandardActionPic/";
        actionLib = new StandardActionLibrary();
        actionTable = actionLib.findStandardActions();
        if(actionTable[0, 0] != "null")
        {
            GameObject table = GameObject.Find("Canvas/CheckStdActionLibraryPanel/ScrollView/Viewport/Content");
            GameObject plandata = GameObject.Find("Canvas/CheckStdActionLibraryPanel/ScrollView/Viewport/Content/StdActionLibrary");
            plandata.SetActive(false);
            Debug.Log("======================================================");
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
                    
                    ButtonInfo info = new ButtonInfo();
                    info.id = actionTable[i, 0];
                    info.actName = actionTable[i, 1];
                    info.obj = row.transform.Find("ActionImageButton").GetComponent<Button>().gameObject;
                    row.transform.Find("ActionImageButton").GetComponent<Button>().onClick.AddListener(
                        delegate ()
                        {
                            selectAction(info);
                        }
                        );
                }
                row.transform.Find("ActionNameToggle").Find("Label").GetComponent<Text>().text = actionTable[i, 1];      
                row.SetActive(true);
            }
        }
 
    }

    void selectAction(ButtonInfo info)
    {
        Debug.Log("info: " + info.id);
        Debug.Log("action Name: " + info.actName);
        PlayerPrefs.SetString("selectStdActionID", info.id);
        PlayerPrefs.SetString("selectStdActionName", info.actName);
        string act_id = "";
        string act_name = "";
        act_id = PlayerPrefs.GetString("selectStdActionID");
        act_name = PlayerPrefs.GetString("selectStdActionName");
        Debug.Log(act_id);
        Debug.Log(act_name);
    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
