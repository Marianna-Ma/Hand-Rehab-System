using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Leap;
using Leap.Unity;
using System;
using System.Data;
using System.IO;

public class StandardActionLibrary : MonoBehaviour {
    //IP地址
    public static string host = "119.3.231.171";
    //端口号
    public static string port = "3306";
    //用户名
    public static string userName = "admin";
    //密码
    public static string password = "Rehabsys@2019";
    //数据库名称
    public static string databaseName = "rehabsys";
    //封装好的数据库类
    MySqlAccess mysql;
    
    GameObject hd;
    SaveHandData handData;
    int flag_S = 0;
    bool allowSaveData = false;
    string path = "";

    // Use this for initialization
    void Start () {
        hd = new GameObject();
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        //string next_id = getActionId();
        //Debug.Log("neext iiiid: " + next_id);

        //saveStandardAction(next_id, "0");
        //-----------------------删除动作--------------------------
        //deleteStandardAction("400011");

        //----------------------查找所有的动作---------------------
        //string[,] res = findStandardActions();
        //showFindActions(res);

        //-----------------------删除指定动作---------------------
        //string[] res = deleteStandardAction("400011");
        //showDeleteInfo(res);

        //----------------------修改指定动作-----------------------
        //changeStandardAction("400009", "屈指");

        //----------------------添加动作---------------------------
        //uploadActions("400001", "0");
        //uploadPicture("400001", "F:/cat.jpg");
        //string next_id = getActionId();
        //Debug.Log("next_id: " + next_id);
        //saveStandardAction("400001", "0");
        //Debug.Log("out....next");
        //saveStandardAction(next_id, "1");
        //addStandardAction(next_id, "拇指屈曲", "F:/115 - Learning - 暑期实训/大三/001_标准动作库/actionsPic/1_拇指屈曲.png");

        //----------------------下载图片--------------------------
        //downloadPicture("400011.jpg");
    }


    public string getActionId()
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        mysql.OpenSql();
        string querySQL = "select max(ac_id) from act;";
        DataSet ds = mysql.SimpleSql(querySQL);
        string res_id = "";
        if(ds != null)
        {
            if(ds.Tables[0].Rows.Count == 1)
            {
                if(ds.Tables[0].Rows[0][0].ToString() == "")
                {
                    res_id = "400001";
                }
                else
                {
                    string cur_max_id = ds.Tables[0].Rows[0][0].ToString();
                    //Debug.Log("max id is: "  + cur_max_id );
                    int next_id = int.Parse(cur_max_id) + 1;
                    res_id = next_id.ToString();
                    //Debug.Log("next id is: " + res_id);
                }
                
            }
            else
            {
                Debug.Log("Error");
                res_id = "error";
            }
        }
        mysql.Close();
        return res_id;
    }

    //OSS相关
    public static void uploadPicture(string act_id, string picPath)
    {
        string objectName = "standard_pictures/" + act_id + ".jpg";
        Debug.Log("picPath: " + objectName);
        OSSConnect.UploadFile(objectName, picPath);
    }

    public static void uploadActions(string act_id, string handtype)
    {
        string objectName = "standard_actions/" + act_id + handtype + ".json";
        string actPath = Application.dataPath + "/StandardActionLibrary/" + act_id + handtype + ".json"; ;
        Debug.Log("objectName: " + objectName);
        Debug.Log("actPath: " + actPath);
        OSSConnect.UploadFile(objectName, actPath);
    }

    public static void downloadPicture(string picName)
    {
        string objectName = "standard_pictures/" + picName;
        string filePath = Application.dataPath + "/StandardActionPic/" + picName;
        Debug.Log("filePath: " + filePath);
        OSSConnect.DownLoadFile(objectName, filePath);
    }

    public static void downloadActions(string actName)
    {
        string objectName = "standard_actions/" + actName;
        string filePath = Application.dataPath + "/StandardActionLibrary/" + actName;
        Debug.Log("filePath: " + filePath);
        OSSConnect.DownLoadFile(objectName, filePath);
    }

    public void deletePicture(string picName)
    {
        string objectName = "standard_pictures/" + picName;
        OSSConnect.DeleteFile(objectName);
    }

    public void deleteActions(string fileName)
    {
        string objectName = "standard_actions/" + fileName;
        OSSConnect.DeleteFile(objectName);
    }

    //增
    public void addStandardAction(string next_id, string actionName, string picPath)
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        string next_pic = next_id + ".jpg";
        string next_left = next_id + "0.json";
        string next_right = next_id + "1.json";
        mysql.OpenSql();
        uploadPicture(next_id, picPath);
        uploadActions(next_id, "0");
        uploadActions(next_id, "1");
        string querySql = "insert into act(ac_id, ac_name, ac_pic, ac_left, ac_right) values(";
        querySql = querySql + "'" + next_id + "', '" + actionName + "', '" + next_pic + "', '" + next_left + "', '" + next_right + "')";
        Debug.Log(querySql);
        //Debug.Log("next_pic: " + next_pic + " next left: " + next_left + " next right: " + next_right);
        mysql.SimpleSql(querySql);
        mysql.Close();
        
    }

    //查
    public string[,] findStandardActions()
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        string[,] res;
        res = new string[1, 1];
        mysql.OpenSql();
        Debug.Log("database name: " + databaseName);
        string querySql = "select ac_id, ac_name, ac_pic from act where ac_ex = 1";
        //string querySql = "select trp_ptID from trp";
        DataSet ds = mysql.SimpleSql(querySql);
        int row_num = ds.Tables[0].Rows.Count;
        //Debug.Log("aaaa" + row_num);
        if(row_num != 0)
        {
            int col_num = ds.Tables[0].Rows[0].ItemArray.Length;
            //Debug.Log("rows num: " + row_num);
            //Debug.Log("cols_num: " + col_num);
            res = new string[row_num, col_num];
            for(int i=0; i<row_num; i++)
            {
                for(int j=0; j<col_num; j++)
                {
                    //Debug.Log("content: " + ds.Tables[0].Rows[i][j]);
                    res[i, j] = ds.Tables[0].Rows[i][j].ToString();
                }
            }
        }
        else
        {
            res[0, 0] = "null";

        }
        mysql.Close();
        return res;
    }

    public void showFindActions(string[,] res)
    {
        int row = res.GetLength(0);
        int col = res.GetUpperBound(res.Rank - 1) + 1;
        Debug.Log("res rows: " + row);
        Debug.Log("res cols: " + col);
        if(res[0, 0] == "null")
        {
            Debug.Log("为空");
        }
        else
        {
            for(int i=0; i<row; i++)
            {
                for(int j=0; j<col; j++)
                {
                    Debug.Log(res[i, j]);
                }
            }
        }
    }

    //删
    public int deleteStandardActions(List<string> id_list)
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        mysql.OpenSql();
        foreach(string id in id_list)
        {
            string querySql = "select * from act where ac_id = '" + id + "'";
            DataSet ds = mysql.SimpleSql(querySql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                mysql.Close();
                return 1;
            }
        }
        foreach(string id in id_list)
        {
            string querySql = "select * from rec where rec_actID = '" + id + "'";
            DataSet ds = mysql.SimpleSql(querySql);
            if(ds.Tables[0].Rows.Count != 0)
            {
                deleteStandardAction(id, 1);
            }
            else
            {
                querySql = "select * from trp where trp_actID = '" + id + "'";
                ds = mysql.SimpleSql(querySql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    deleteStandardAction(id, 1);
                }
                else
                {
                    deleteStandardAction(id, 0);
                }
            }
            
            
        }
        mysql.Close();
        return 0;
    }

    public void deleteLocJson(string filePath)
    {
        string dirPath = Application.dataPath + "/StandardActionLibrary/" + filePath;
        if (File.Exists(dirPath))
        {
            File.Delete(dirPath);
        }
    }

    public void deleteStandardAction(string hand_id, int flag)
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        mysql.OpenSql();
        if(flag == 1)
        {
            string querySql = "update act set ac_ex = 0 where ac_id = '" + hand_id + "'";
            mysql.SimpleSql(querySql);
        }
        else if(flag == 0)
        {
            string querySql = "select * from act where ac_id = '" + hand_id + "'";
            DataSet ds = mysql.SimpleSql(querySql);
            string picName = ds.Tables[0].Rows[0][2].ToString();
            string leftData = ds.Tables[0].Rows[0][3].ToString();
            string rightData = ds.Tables[0].Rows[0][4].ToString();
            querySql = "delete from act where ac_id = '" + hand_id + "'";
            Debug.Log(querySql);
            deletePicture(picName);
            deleteLocJson(leftData);
            deleteLocJson(rightData);
            deleteActions(leftData);
            deleteActions(rightData);
            mysql.SimpleSql(querySql);
            Debug.Log("delete!!!!");
        }
        
        mysql.Close();

    }

    public void showDeleteInfo(string[] res)
    {
        if(res[0] != "null")
        {
            Debug.Log(res[0] + ", " + res[1]);
        }
        else
        {
            Debug.Log("无该动作");
        }
        
    }

    //改
    public void changeStandardAction(string hand_id, string actionName, string picPath)
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        mysql.OpenSql();
        string querySql = "select ac_id from act where ac_id = '" + hand_id + "'";
        DataSet ds = mysql.SimpleSql(querySql);
        if(ds.Tables[0].Rows.Count == 1)
        {
            uploadPicture(hand_id, picPath);
            uploadActions(hand_id, "0");
            uploadActions(hand_id, "1");
            querySql = "update act set ac_name = '" + actionName + "' where ac_id = '" + hand_id + "'";
            mysql.SimpleSql(querySql);
            Debug.Log("change already");
        }
        else
        {
            Debug.Log("无指定动作");
        }
        mysql.Close();
    }

    public void saveStandardAction(string hand_id, string handtype)
    {
        Debug.Log("flag_S: " + flag_S);
        path = "/StandardActionLibrary/" + hand_id + handtype + ".json";
        flag_S = 1;
        allowSaveData = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag_S == 1 && allowSaveData)
        {
            
            hd.AddComponent<SaveHandData>();
            handData = (SaveHandData)hd.GetComponent(typeof(SaveHandData));
            handData.Init(200, path);
            PlayerPrefs.SetString("standardHandDataPath", handData.savedDataPath);
            Debug.Log("ready to save data: " + handData.savedDataPath);
            allowSaveData = false;

        }
        if (flag_S == 1 && handData.IsSaveCompleted())
        {
            Debug.Log("save completed detected!");
            flag_S = 0;
            Debug.Log("flag: " + flag_S);
            Messagebox.MessageBox(IntPtr.Zero, "录制动作成功！", "成功", 0);
            Destroy(hd.GetComponent<SaveHandData>());
            GameObject.Find("Canvas").GetComponent<MainMenuManager>().OpenPanelByName("AddStdActionLibraryPanel");

        }

    }
}
