using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientTrainPanel : MonoBehaviour {

    public GameObject leaphandleft;
    public GameObject leaphandright;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
            //TODO:Pause and ask if patient needed to quit
            ClickTrainPauseButton();
        }
	}

    public void ClickTestButton()
    {
        
    }

    public void ClickTrainPauseButton()
    {
        Debug.Log("Pause");
        leaphandleft.SetActive(false);
        leaphandright.SetActive(false);
    }
}
