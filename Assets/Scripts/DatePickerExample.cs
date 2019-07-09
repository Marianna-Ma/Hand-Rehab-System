
/*=========================================
* Author: springDong
* Description: SpringGUI.Calendar example.
==========================================*/

using UnityEngine;
using SpringGUI;

public class DatePickerExample : MonoBehaviour
{
    public DatePicker DatePicker = null;
	public Calendar calendar = null;

    public void Awake()
	{
		calendar.onDayClick.AddListener(time =>
        {
				Debug.Log(string.Format("Today is {0}Year{1}Month{2}Day" ,
                time.Year , time.Month , time.Day));
        });
		calendar.onMonthClick.AddListener(time =>
        {
				Debug.Log(string.Format("This month is {0}Year{1}Month" ,
            time.Year , time.Month));
        });
		calendar.onYearClick.AddListener(time =>
        {
				Debug.Log(string.Format("This year{0}Year" , time.Year));
        });
    }
}