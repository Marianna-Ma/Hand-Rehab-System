using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckStdActionLibraryPanel : MonoBehaviour, IPointerClickHandler{

    StandardActionLibrary stdlib = new StandardActionLibrary();
    List<string> selectActList = new List<string>();

    // Use this for initialization
    void Start () {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "AddActionButton")
        {
            //跳转到添加动作界面

            Debug.Log("clickclick");
            string next_id = stdlib.getActionId();
            PlayerPrefs.SetString("newActionID", next_id);
            Debug.Log(next_id);

            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AddStdActionLibraryPanel");
        }
        if (eventData.pointerPress.name == "DeleteActionButton")
        {
            GameObject.Find("CheckStdActionLibraryPanel").GetComponent<CreateStdActionTable>().getSelectActList(selectActList);
            Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            foreach (string id in selectActList)
            {
                Debug.Log(id);
                //stdlib.deleteStandardAction(id);

            }
        }
        if (eventData.pointerPress.name == "BackButton")
        {
            //跳转到开始界面

            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AdminStartPanel");
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
