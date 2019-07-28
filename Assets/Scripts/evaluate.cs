using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Leap;
using Leap.Unity;
using System;
using UnityEngine.UI;



/// <summary>
/// 把每个手指的组合测量都写一个函数，然后把一个数组（代表调用哪些函数）
/// 所需要的函数：
/// 综合函数(string status)
/// TODO:updata函数在做动作的时候判断动作的一状态，然后给动作进行一些提示
/// 每个手指的伸直（弯曲）bool StrightState(Finger finger) 是否计算伸直
/// 每个手指的弯曲角度（TAM函数）void CalculateTAM() 是否计算握拳
/// 每两个手指指尖的距离（共有10个组合）double [] FingerTipPosition() 是否需要手指捏合
/// 相邻两个手指之间的夹角（共有4个组合）double [] FingerAngle() 是否需要张开手指
/// </summary>

public class evaluate : MonoBehaviour
{
    //用于存放每个手指的评测值
    private double[] TAM = new double[5];

    //用于存放总评测值
    private double Evaluation;

    private double[] tip_position = new double[10];

    private double[] finger_angle = new double[4];
    //数据读取器
    LeapProvider provider;

    //用于显示特效
    public GameObject _Pre_1;
    public GameObject _Pre_2;
    public GameObject _pre_3;

    //用于显示评级
    public GameObject text_cha;
    public GameObject text_ke;
    public GameObject text_liang;
    public GameObject text_you;

    public GameObject leaphandleft;
    public GameObject leaphandright;

    public static string host = "119.3.231.171";                //IP地址
    public static string port = "3306";             //端口号
    public static string userName = "admin";            //用户名
    public static string password = "Rehabsys@2019";            //密码
    public static string databaseName = "rehabsys";     //数据库名称

    MySqlAccess mysql;

    Finger.FingerType[] arr = { Finger.FingerType.TYPE_INDEX, Finger.FingerType.TYPE_MIDDLE };

    //初始化
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CancelInvoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Invoke("PrintEvaluation", 0f);
        }
        //if()
    }

    public void OnEvaluation()
    {
        leaphandleft.SetActive(true);
        leaphandright.SetActive(true);
        string s = SearchEva();
        MainEvaluation(s);
    }

    /// <summary>
    /// 开始评测的时候调用
    /// </summary>
    public void StartToEvaluate()
    {
        Invoke("OnEvaluation", 5f);
        Invoke("PrintEvaluation", 6f);
    }

    /// <summary>
    /// 通过动作标号查找评测方法
    /// </summary>
    /// <returns></returns>
    public string SearchEva()
    {
        PlayerPrefs.SetString("selectStdActionID", "400001");
        string ac_id = "";
        ac_id = PlayerPrefs.GetString("selectStdActionID");
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        mysql.OpenSql();
        String querySQL = "select est_way from est where est_id='" + ac_id + "'";
        DataSet ds = mysql.SimpleSql(querySQL);
        string res = "";
        int row_num = ds.Tables[0].Rows.Count;
        if (row_num > 0)
        {
            res = ds.Tables[0].Rows[0][0].ToString();
        }
        mysql.Close();
        return res;
    }

    /// <summary>
    /// 将评测结果上传数据库
    /// </summary>
    public void AddRecTest()
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
        mysql.OpenSql();

        string nowtime = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ac_id = PlayerPrefs.GetString("selectStdActionID");
        int hand = PlayerPrefs.GetInt("selectPlanHand");
        string ptId = PlayerPrefs.GetString("userID");
        string link = PlayerPrefs.GetString("patientHandDataPath");
        string querySql = "insert into rec(rec_date, rec_actID, rec_hand, rec_ptID, rec_link, rec_test) " +
            "values('" + nowtime + "','" + ac_id + "','" + hand + "','" + ptId + "','" + link + "','" + GetEvaluation() + "')";

        mysql.SimpleSql(querySql);
        mysql.Close();
    }

    public double GetEvaluation()
    {
        return Evaluation;
    }

    /// <summary>
    /// 总函数
    /// </summary>
    /// <param name="status"></param>
    public void MainEvaluation(string status)
    {
        int count = 0;
        Evaluation = 0;
        tip_position = FingerTipPosition();
        finger_angle = FingerAngle();
        for (int i = 0; i < status.Length; i++)
        {
            if (status[i] == '1')
            {
                Evaluation += CaseEvaluation(i);

                count++;
            }
        }
        Evaluation /= count;
    }

    /// <summary>
    /// 其实这个才是总函数（划掉）
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public double CaseEvaluation(int n)
    {
        double eva = 0;
        switch (n)
        {
            //0-4:用于评测伸直
            case 0:
                eva = Finger_Extension(n);
                Debug.Log(eva);
                break;
            case 1:
                eva = Finger_Extension(n);
                Debug.Log(eva);
                break;
            case 2:
                eva = Finger_Extension(n);
                Debug.Log(eva);
                break;
            case 3:
                eva = Finger_Extension(n);
                Debug.Log(eva);
                break;
            case 4:
                eva = Finger_Extension(n);
                Debug.Log(eva);
                break;
            //5-9:用于评测弯曲
            case 5:
                CalculateTAM();
                eva = TAM[0];
                break;
            case 6:
                CalculateTAM();
                eva = TAM[1];
                break;
            case 7:
                CalculateTAM();
                eva = TAM[2];
                break;
            case 8:
                CalculateTAM();
                eva = TAM[3];
                break;
            case 9:
                CalculateTAM();
                eva = TAM[4];
                break;
            //10-19：用于评测手指是否捏合
            case 10:
                eva = 1.1 - 1.1 * (tip_position[0] / 0.13);
                break;
            case 11:
                eva = 1 - tip_position[1] / 0.14;
                break;
            case 12:
                eva = 1.1 - 1.1 * (tip_position[2] / 0.15);
                break;
            case 13:
                eva = 1.1 - 1.1 * (tip_position[3] / 0.17);
                break;
            case 14:
                eva = 1 - tip_position[4] / 0.065;
                break;
            case 15:
                eva = 1 - tip_position[5] / 0.1;
                break;
            case 16:
                eva = 1 - tip_position[6] / 0.13;
                break;
            case 17:
                eva = 1 - tip_position[7] / 0.08;
                break;
            case 18:
                eva = 1 - tip_position[8] / 0.1;
                break;
            case 19:
                eva = 1 - tip_position[9] / 0.08;
                break;
            //20-23：用于评测手掌是否张开
            case 20:
                eva = 1.2 * ((finger_angle[0] - 0.45) / 0.2);
                break;
            case 21:
                eva = 1.2 * (finger_angle[1] / 0.2);
                break;
            case 22:
                eva = 1.2 * (finger_angle[2] / 0.2);
                break;
            case 23:
                eva = 1.2 * (finger_angle[3] / 0.2);
                break;
        }
        return eva;
    }

    /// <summary>
    /// 计算每一个手指的关节活动度
    /// </summary>
    public void CalculateTAM()
    {
        //中间变量：角度，余弦定理分子，余弦定理分母
        double angle;

        //获取当前帧
        Frame frame = provider.CurrentFrame;

        //初始化TAM数组的索引
        int tamIndex = 0;

        //遍历每一个手
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                //遍历每一个手指
                foreach (Finger finger in hand.Fingers)
                {
                    //拇指
                    if (finger.Type == Finger.FingerType.TYPE_THUMB)
                    {
                        angle = 0f;
                        for (int i = 0; i < 2; i++)
                        {
                            angle += CalculateAngle(finger.Bone(ToBone(i)), finger.Bone(ToBone(i + 1)));
                        }
                        //Debug.Log(tamIndex + ":          " + angle);
                        //角度除以标准值得到TAM百分比评测值
                        TAM[tamIndex] = (double)Mathf.Abs((float)angle / 0.57f);
                        //Debug.Log(tamIndex + ":          " + TAM[tamIndex]);
                    }
                    //其他手指
                    else
                    {
                        angle = 0f;
                        for (int i = 0; i < 3; i++)
                        {
                            angle += CalculateAngle(finger.Bone(ToBone(i)), finger.Bone(ToBone(i + 1)));
                        }
                        //Debug.Log(tamIndex + ":          " + angle);
                        TAM[tamIndex] = (double)Mathf.Abs((float)angle / 1.7f);
                    }
                    tamIndex++;
                }
            }
        }
    }

    /// <summary>
    /// 用于评测伸指
    /// </summary>
    public double Finger_Extension(int n)
    {
        //获取当前帧
        Frame frame = provider.CurrentFrame;
        bool isStright = true;
        double eva = 0;

        //判断每个手指是否都处于伸直状态
        foreach (Hand hand in frame.Hands)
        {
            foreach (Finger finger in hand.Fingers)
            {
                if (finger.Type != TypeToFingerType(n))
                    if (!StrightState(finger))
                    {
                        isStright = false;
                    }
            }
        }

        //当有手指没有伸直时，计算每个手指的屈曲度,否则动作达标
        if (!isStright)
        {
            CalculateTAM();
            //因为TAM是计算手指弯曲程度的，这里我们要求的是伸展时的程度，实际上是弯曲程度的相对值
            eva = 1 - TAM[n];
            //Debug.Log(Evaluation);
        }
        else
        {
            eva = 0.95;
        }

        return eva;
    }

    /// <summary>
    /// 判断手指伸直和弯曲
    /// </summary>
    /// <param name="finger"></param>
    /// <returns>true:伸直；false:弯曲</returns>
    public bool StrightState(Finger finger)
    {
        return finger.IsExtended;
    }

    /// <summary>
    /// 用于输出评测结果
    /// </summary>
    public void PrintEvaluation()
    {
        //leaphandleft.SetActive(false);
        //leaphandright.SetActive(false);
        //GameObject.Find("Canvas").GetComponent<HandControl>().InvisiHands();

        SpecialEffectFirst();
        Invoke("SpecialEffectSecond", 2.5f);

        
    }

    private void SpecialEffectFirst()
    {
        _Pre_1.SetActive(true);
        Invoke("LeaveFirst", 2.4f);
    }

    private void SpecialEffectSecond()
    {
        _Pre_2.SetActive(true);
        _pre_3.SetActive(true);
        GameObject.Find("Canvas/PatientTrainPanel/ResultText").SetActive(true);
        if (Evaluation < 0.5)
        {
            GameObject.Find("ResultText").GetComponent<Text>().text = "继续努力哦~";
        }
        else if (Evaluation >= 0.5 && Evaluation < 0.75)
        {
            GameObject.Find("ResultText").GetComponent<Text>().text = "还不错";
        }
        else if (Evaluation >= 0.75 && Evaluation < 0.9)
        {
            GameObject.Find("ResultText").GetComponent<Text>().text = "好";
        }
        else if (Evaluation >= 0.9)
        {
            GameObject.Find("ResultText").GetComponent<Text>().text = "太棒啦~";
        }
    }

    private void LeaveFirst()
    {
        _Pre_1.SetActive(false);
    }

    /// <summary>
    /// 结束评测的时候调用
    /// </summary>
    public void LeaveEvaluation()
    {
        _Pre_2.SetActive(false);
        _pre_3.SetActive(false);
        text_cha.SetActive(false);
        text_ke.SetActive(false);
        text_liang.SetActive(false);
        text_you.SetActive(false);
    }

    /// <summary>
    /// 转换Leap motion所需的骨类型
    /// </summary>
    /// <param name="i">骨</param>
    /// <returns>对应的骨类型</returns>
    public Bone.BoneType ToBone(int i)
    {
        if (i == 1) return Bone.BoneType.TYPE_PROXIMAL;
        else if (i == 2) return Bone.BoneType.TYPE_INTERMEDIATE;
        else if (i == 3) return Bone.BoneType.TYPE_DISTAL;
        else if (i == 0) return Bone.BoneType.TYPE_METACARPAL;
        else return Bone.BoneType.TYPE_INVALID;
    }

    /// <summary>
    /// 转换手指名称
    /// </summary>
    /// <param name="i">手指</param>
    /// <returns>对应的手指名称</returns>
    public string ToFinger(int i)
    {
        if (i == 1) return "食指";
        else if (i == 2) return "中指";
        else if (i == 3) return "无名指";
        else return "小拇指";
    }

    /// <summary>
    /// 根据数字转换成Finger
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="i"></param>
    /// <returns>指定的Finger</returns>
    public Finger TypeToFinger(Hand hand, int i)
    {
        if (i == 0) return hand.GetThumb();
        else if (i == 1) return hand.GetIndex();
        else if (i == 2) return hand.GetMiddle();
        else if (i == 3) return hand.GetRing();
        else if (i == 4) return hand.GetPinky();
        else return null;
    }

    /// <summary>
    /// 根据数字转换成手指类型
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public Finger.FingerType TypeToFingerType(int i)
    {
        if (i == 0) return Finger.FingerType.TYPE_THUMB;
        else if (i == 1) return Finger.FingerType.TYPE_INDEX;
        else if (i == 2) return Finger.FingerType.TYPE_MIDDLE;
        else if (i == 3) return Finger.FingerType.TYPE_RING;
        else if (i == 4) return Finger.FingerType.TYPE_PINKY;
        else return Finger.FingerType.TYPE_UNKNOWN;
    }

    /// <summary>
    /// 计算各个手指指尖的距离
    /// </summary>
    /// <returns>数组：各个手指指尖的距离</returns>
    public double[] FingerTipPosition()
    {
        double[] finger_tip_position = new double[10];
        Frame frame = provider.CurrentFrame;
        int index = 0;
        foreach (Hand hand in frame.Hands)
        {
            if (index >= 10)
                break;
            for (int i = 0; i < 4; i++)
            {
                Finger f1 = TypeToFinger(hand, i);
                for (int j = i + 1; j <= 4; j++)
                {
                    Finger f2 = TypeToFinger(hand, j);
                    double twoTipPosition = Mathf.Sqrt((f1.TipPosition.x - f2.TipPosition.x) * (f1.TipPosition.x - f2.TipPosition.x) +
                        (f1.TipPosition.y - f2.TipPosition.y) * (f1.TipPosition.y - f2.TipPosition.y) +
                        (f1.TipPosition.z - f2.TipPosition.z) * (f1.TipPosition.z - f2.TipPosition.z));
                    finger_tip_position[index] = twoTipPosition;
                    index++;
                }
            }
        }
        return finger_tip_position;
    }

    /// <summary>
    /// 计算相邻手指之间的角度
    /// </summary>
    /// <returns>数组：相邻手指之间的角度</returns>
    public double[] FingerAngle()
    {
        double[] finger_angle = new double[4];
        Frame frame = provider.CurrentFrame;
        int i = 0;
        foreach (Hand hand in frame.Hands)
        {
            if (i >= 4)
                break;
            for (int j = 0; j < 4; j++)
            {
                Bone b1 = TypeToFinger(hand, j).Bone(Bone.BoneType.TYPE_PROXIMAL);
                Bone b2 = TypeToFinger(hand, j + 1).Bone(Bone.BoneType.TYPE_PROXIMAL);
                finger_angle[i] = CalculateAngle(b1, b2);
                i++;
            }
        }
        return finger_angle;
    }

    /// <summary>
    /// 余弦定理计算两个Bone的方向向量之间的角度
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public double CalculateAngle(Bone b1, Bone b2)
    {
        float temp1 = b1.Basis.CalculateRotation().x * b2.Basis.CalculateRotation().x +
            b1.Basis.CalculateRotation().y * b2.Basis.CalculateRotation().y +
            b1.Basis.CalculateRotation().z * b2.Basis.CalculateRotation().z;
        float temp2 = Mathf.Sqrt(b1.Basis.CalculateRotation().x * b1.Basis.CalculateRotation().x +
            b1.Basis.CalculateRotation().y * b1.Basis.CalculateRotation().y +
            b1.Basis.CalculateRotation().z * b1.Basis.CalculateRotation().z) *
            Mathf.Sqrt(b2.Basis.CalculateRotation().x * b2.Basis.CalculateRotation().x +
            b2.Basis.CalculateRotation().y * b2.Basis.CalculateRotation().y +
            b2.Basis.CalculateRotation().z * b2.Basis.CalculateRotation().z);
        temp1 /= temp2;
        double angle = Mathf.Acos(temp1);
        return angle;
    }
}


