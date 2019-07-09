using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;


public class ButtonClick : MonoBehaviour, IPointerClickHandler {
	// loginPanel中的输入框
	public InputField userNameField;
	public InputField passwordNameField;

	// adminPanel中的输入框
	public InputField dcIDField;
	public InputField dcNameField;
	public InputField dcSexField;
	public InputField dcProField;
	public InputField dcTeleField;
	public InputField adm_firstPswdField;
	public InputField adm_secondPswdField;

	// doctorPanel中的输入框
	public InputField ptIDField;
	public InputField ptNameField;
	public InputField ptSexField;
	public InputField ptTeleField;
	public InputField dc_firstPswdField;
	public InputField dc_secondPswdField;


//	public Panel[] Pages;
//	public Panel currentPanel;

//	private Text loginMessage;		//提示用户登录信息

	public string host;				//IP地址
	public string port;				//端口号
	public string userName;			//用户名
	public string password;			//密码
	public string databaseName;		//数据库名称

	//封装好的数据库类
	MySqlAccess mysql;


	private void Start() {
//		loginMessage = GameObject.FindGameObjectWithTag("LoginMessage").GetComponent<Text>();
		mysql = new MySqlAccess(host, port, userName, password, databaseName);

//		OnClickedReviewRecordButton ();



//		Pages [0].gameObject.AddComponent<Panel> ();
//		Pages [0] = GameObject.FindGameObjectWithTag ("loginPanel");
//		Pages [1] = GameObject.FindGameObjectWithTag ("adminPanel");
//		for (int i = 0; i < Pages.Length; i++)
//		{
//			Pages [i].gameObject.AddComponent<Panel> ();
//		}

	}

//	private void Awake()
//	{
//		if (Pages.Length <= 0)
//			return;
//		OpenPanel (Pages[0]);
//	}

//	/// <summary>
//	/// 打开面板
//	/// </summary>
//	public void OpenPanel (Panel page)
//	{
//		if (page == null)
//			return;
//		page.PanelBefore = currentPanel;
//		currentPanel = page;
//		CloseAllPanels ();
//		Animator anim = page.GetComponent<Animator> ();
//		if (anim && anim.isActiveAndEnabled)
//			anim.SetBool ("Open", true);
//		page.gameObject.SetActive (true);
//		Debug.Log("面板" + page.gameObject.name + "已经打开");
//	}
//	/// <summary>
//	/// 通过名字打开面板
//	/// </summary>
//	public void OpenPanelByName(string name)
//	{
//		Panel page = null;
//		for (int i = 0; i < Pages.Length; i++)
//		{
//			if (Pages [i].name == name) {
//				page = Pages [i];
//				break;
//			}
//		}
//		if (page == null)
//			return;
//		page.PanelBefore = currentPanel;
//		currentPanel = page;
//		CloseAllPanels ();
//		Animator anim = page.GetComponent<Animator> ();
//		if (anim && anim.isActiveAndEnabled) {
//			anim.SetBool ("Open", true);
//		}
//		page.gameObject.SetActive (true);
//	}
//	/// <summary>
//	/// 关闭所有面板
//	/// </summary>
//	public void CloseAllPanels() {
//		if (Pages.Length <= 0)
//			return;
//		for (int i = 0; i < Pages.Length; i++) {
//			Animator anim = Pages [i].gameObject.GetComponent<Animator> ();
//			if (anim && anim.isActiveAndEnabled) {
//				anim.SetBool ("Open", false);
//			}
//			if (Pages [i].isActiveAndEnabled) {
//				StartCoroutine (DisablePanelDeleyed (Pages [i]));
//			}
//		}
//	}
//	/// <summary>
//	/// 禁用面板
//	/// </summary>
//	public IEnumerator DisablePanelDeleyed(Panel page)
//	{
//		bool closedStateReached = false;
//		bool wantToClose = true;
//		Animator anim = page.gameObject.GetComponent<Animator> ();
//		if (anim && anim.enabled) {
//			while (!closedStateReached && wantToClose) {
//
//				if (anim.isActiveAndEnabled && !anim.IsInTransition (0)) {
//					closedStateReached = anim.GetCurrentAnimatorStateInfo (0).IsName ("Closing");
//				}
//				yield return new WaitForEndOfFrame ();
//			}
//			if (wantToClose) {
//				anim.gameObject.SetActive (false);
//			}
//		} else {
//			page.gameObject.SetActive (false);
//		}
//	}



	public void OnPointerClick(PointerEventData eventData) {
		// loginPanel
		if (eventData.pointerPress.name == "initialButton")		//如果当前按下的按钮是初始化按钮
			OnClickedInitialButton();
		if (eventData.pointerPress.name == "loginButton")       //如果当前按下的按钮是注册按钮 
			OnClickedLoginButton();
		// adminPanel
		if (eventData.pointerPress.name == "addDoctorButton")	//如果当前按下的按钮是添加医生按钮
			OnClickedAddDoctorButton();
		// doctorPanel
		if (eventData.pointerPress.name == "addPatientButton")	//如果当前按下的按钮是添加患者按钮
			OnClickedAddPatientButton();

		if (eventData.pointerPress.name == "updatePswdButton")	//如果当前按下的按钮是修改密码按钮
			OnClickedUpdatePasswordButton();
	}


	/// <summary>
	/// 按下Initial按钮（将人员表中所有的管理员添加到管理员表中）
	/// </summary>
	public void OnClickedInitialButton() {
		mysql.OpenSql ();
		string query = "select ppl_id from ppl where ppl_role = '管理员'";
		DataSet ds1 = mysql.QuerySet (query);
		if (ds1 != null) {
			DataTable table = ds1.Tables [0];
			for (int i = 0; i < table.Rows.Count; i++) {
				Debug.Log (table.Rows [i] [0]);
				// 把激活改成1
				string actquery = "update ppl set ppl_act = 1 where ppl_id = " + table.Rows[i][0];
				DataSet ds2 = mysql.QuerySet (actquery);
				// 往管理员表里加
				string dupliquery = "select * from adm where adm_id = " + table.Rows[i][0];
				DataSet ds3 = mysql.QuerySet (dupliquery);
				if (ds3 == null) {
					string addquery = "insert into adm values ('" + table.Rows[i][0] + "', '" + table.Rows[i][0] + "')";
					DataSet ds4 = mysql.QuerySet (addquery);
				}
			}
		} else {
			Debug.Log ("No admin in database!");
		}
		mysql.Close ();
	}


	/// <summary>
	/// 按下Login按钮
	/// </summary>
	public void OnClickedLoginButton() {
		mysql.OpenSql();
		string id = userNameField.text;
		string pswd = passwordNameField.text;
		Debug.Log (id + " " + pswd);
		if (id [0] == '1') {
			//管理员
			string query = "select * from adm where adm_id = " + id + " and adm_pswd = " + pswd;
			Debug.Log (query);
			DataSet ds = mysql.QuerySet (query);
			DataTable table = ds.Tables [0];
			if (table.Rows.Count != 0) {
				Debug.Log ("管理员，登录成功");
				PlayerPrefs.SetString ("userID", userNameField.text);
			} else {
				Debug.Log ("管理员，登录失败，不在管理员表中或密码错误");
			}

		} else if (id [0] == '2') {
			//医生
			string query = "select * from doc where dc_id = " + id + " and dc_pswd = " + pswd;
			DataSet ds = mysql.QuerySet (query);
			DataTable table = ds.Tables [0];
			if (table.Rows.Count != 0) {
				Debug.Log ("医生，登录成功");
				PlayerPrefs.SetString ("userID", userNameField.text);
			} else {
				Debug.Log ("医生，登录失败，不在医生表中或密码错误");
			}
		} else if (id [0] == '3') {
			//患者
			string query = "select * from pat where pt_id = " + id + " and pt_pswd = " + pswd;
			DataSet ds = mysql.QuerySet (query);
			DataTable table = ds.Tables [0];
			if (table.Rows.Count != 0) {
				Debug.Log ("患者，登录成功");
				PlayerPrefs.SetString ("userID", userNameField.text);
			} else {
				Debug.Log ("患者，登录失败，不在患者表中或密码错误");
			}
		} else {
			Debug.Log ("账号类型错误");
		}

//		DataSet ds = mysql.Select("userinfo", new string[] { "level" }, new string[] { "`" + "account" + "`", "`" + "password" + "`" }, new string[] { "=", "=" }, new string[] { userNameInput.text, passwordInput.text });
//		if (ds != null) {
//			DataTable table = ds.Tables[0];
//			if (table.Rows.Count > 0) {
//				loginMsg = "登陆成功！";
//				loginMessage.color = Color.green;
//				Debug.Log("用户权限等级：" + table.Rows[0][0]);
//			} else {
//				loginMsg = "用户名或密码错误！";
//				loginMessage.color = Color.red;
//			}
//			loginMessage.text = loginMsg;
//		}
		mysql.Close();

	}

	/// <summary>
	/// 按下AddDoctor按钮
	/// </summary>
	public void OnClickedAddDoctorButton() {
		mysql.OpenSql ();
		string dcID = dcIDField.text;
		string dcName = dcNameField.text;
		string dcSex = dcSexField.text;
		string dcPro = dcProField.text;
		string dcTele = dcTeleField.text;

		int flag = 1; // 1可添加，0不可添加
		if (dcID == "" | dcName == "") {
			flag = 0;
			Debug.Log ("医生编号和姓名不能为空");
		}
		if (dcID != "") {
			string query = "select * from ppl where ppl_id = " + dcID;
			DataSet ds = mysql.QuerySet (query);
			DataTable table = ds.Tables [0];
			if (table.Rows.Count == 0) {
				flag = 0;
				Debug.Log ("医生编号不在人员表中");
			}
		}
		if (flag == 1) {
			string query = "insert into doc values ('" + dcID + "','" + dcName + "','" + dcSex + "','" + dcPro + "','" + dcTele + "','" + dcID + "')";
			DataSet ds = mysql.QuerySet (query);
//			DataTable table = ds.Tables [0];
			string query1 = "update ppl set ppl_act = 1 where ppl_id = '" + dcID + "'";
			DataSet ds1 = mysql.QuerySet (query1);
			Debug.Log ("添加医生账号成功，医生编号：" + dcID);
		}
		mysql.Close ();
	}


	/// <summary>
	/// 按下UpdatePswd按钮
	/// </summary>
	public void OnClickedUpdatePasswordButton() {
//		string userID = "200001";
		string userID = "";
		if (PlayerPrefs.HasKey("userID")) {
			userID = PlayerPrefs.GetString ("userID");
		}
		Debug.Log ("userID" + userID);

		int role = userID[0];

		mysql.OpenSql ();
		string firstPswd = "";
		string secondPswd = "";
		if (role == '1') {
			Debug.Log (adm_firstPswdField);
			firstPswd = adm_firstPswdField.text;
			secondPswd = adm_secondPswdField.text;
		} else if (role == '2') {
			firstPswd = dc_firstPswdField.text;
			secondPswd = dc_secondPswdField.text;
		} else if (role == '3') {
//			firstPswd = pt_firstPswdField.text;
//			secondPswd = pt_secondPswdField.text;
		}

		int flag = 0;
		Debug.Log (firstPswd + " " + secondPswd);
		if (firstPswd.Length < 6 || firstPswd.Length > 20) {
			flag = 0;
			Debug.Log ("密码长度应为6~20个字符");
		}
		else {
			string query = "";
			if (role == '1')
				query = "select adm_pswd from adm where adm_id = '" + userID + "'";
			else if (role == '2')
				query = "select dc_pswd from doc where dc_id = '" + userID + "'";
			else if (role == '3')
				query = "select pt_pswd from pat where pt_id = '" + userID + "'";
			DataSet ds = mysql.QuerySet (query);
			DataTable table = ds.Tables [0];
			string beforePswd = table.Rows[0][0].ToString();
			Debug.Log ("beforePswd" + beforePswd);
			if (beforePswd == firstPswd) {
				flag = 0;
				Debug.Log ("新密码不可与原密码相同");
			} else {
				if (firstPswd != secondPswd) {
					flag = 0;
					Debug.Log ("两次密码不一致");
				} else {
					flag = 1;
				}
			}
		}
		
		if (flag == 1) {
			string query = "";
			if (role == '1')
				query = "update adm set adm_pswd = '" + firstPswd + "' where adm_id = '" + userID + "'";
			else if (role == '2')
				query = "update doc set dc_pswd = '" + firstPswd + "' where dc_id = '" + userID + "'";
			else if (role == '3')
				query = "update pat set pt_pswd = '" + firstPswd + "' where pt_id = '" + userID + "'";
			DataSet ds = mysql.QuerySet (query);
			Debug.Log ("修改密码成功，新密码为：" + firstPswd);
		}
		mysql.Close ();
	}

	/// <summary>
	/// 按下Add Patient按钮
	/// </summary>
	public void OnClickedAddPatientButton() {
		string dcID = "";
		if (PlayerPrefs.HasKey("userID")) {
			dcID = PlayerPrefs.GetString ("userID");
		}
		Debug.Log ("userID" + dcID);

		if (dcID [0] != '2') {
			Debug.Log ("该用户身份不是医生！");
		}
		else {
			mysql.OpenSql ();
			string ptID = ptIDField.text;
			string ptName = ptNameField.text;
			string ptSex = ptSexField.text;
			string ptTele = ptTeleField.text;
			int flag = 1; // 1可添加，0不可添加
			if (ptID == "" | ptName == "") {
				flag = 0;
				Debug.Log ("患者编号和姓名不能为空");
			}
			if (ptID != "") {
				string query = "select * from ppl where ppl_id = " + ptID;
				DataSet ds = mysql.QuerySet (query);
				DataTable table = ds.Tables [0];
				if (table.Rows.Count == 0) {
					flag = 0;
					Debug.Log ("患者编号不在人员表中");
				}
			}
			if (flag == 1) {
				string query = "insert into pat values ('" + ptID + "','" + ptName + "','" + ptSex + "','" + ptTele + "','" + ptID + "','" + dcID + "')";
				DataSet ds = mysql.QuerySet (query);
				string query1 = "update ppl set ppl_act = 1 where ppl_id = '" + ptID + "'";
				DataSet ds1 = mysql.QuerySet (query1);
				Debug.Log ("添加患者账号成功，患者编号：" + ptID);
			}
			mysql.Close ();
		}
	}

	public DataTable OnClickedReviewRecordButton()
	{
		string userID = "";
		string pat_id = "";
		if (PlayerPrefs.HasKey("userID")) {
			userID = PlayerPrefs.GetString ("userID");
		}
		Debug.Log ("userID" + userID);

		if (userID [0] == '3') {
			pat_id = userID;
		}
//		else (userID[0] == '2') {
//			pat_id = pat_id;  /// 如果是医生调用此函数，患者账号需要医生选择
//		}

//		string pat_id = "300001";
		string startDate = "20190621";
		string endDate = "20190623";
		string act_id = "400001";
		int hand = 0;

		string nowTime;
		nowTime = DateTime.Now.ToString ("yyyyMMdd");
		Debug.Log ("nowTime" + nowTime);

		int startDateInt = Convert.ToInt32(startDate);
		int endDateInt = Convert.ToInt32(endDate);
		if (startDateInt > endDateInt) {
			Debug.Log ("");
		} 
		string query = "select rec_date, rec_link, rec_test from rec where rec_ptID = '" + pat_id + "' and rec_hand = " + hand + 
			" and rec_actID = '" + act_id + "' and rec_date >= '" + startDate + "' and rec_date <= '" + endDate + "'";
		DataSet ds = mysql.QuerySet (query);
		DataTable table = ds.Tables [0];
		Debug.Log (table.Rows.Count);
		//		string[][] retList = {{0,0,0},{0,0,0},{0,0,0}};
		////		string[][] retList = new string[table.Rows.Count][3];
		//		int i = 0;
		//		for (; i < table.Rows.Count; i++) {
		////			string[] tempList;
		////			tempList [0] = table.Rows [i] [0].ToString();
		////			tempList [1] = table.Rows [i] [1].ToString();
		////			tempList [2] = table.Rows [i] [2].ToString();
		//			retList [i] [0] = table.Rows [i] [0].ToString();
		//			retList [i] [1] = table.Rows [i] [1].ToString();
		//			retList [i] [2] = table.Rows [i] [2].ToString();
		//		}
		return table;
	}
}
