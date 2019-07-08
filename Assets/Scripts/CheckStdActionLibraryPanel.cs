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
            Debug.Log(next_id);
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


    }

    // Update is called once per frame
    void Update () {
		
	}
}
