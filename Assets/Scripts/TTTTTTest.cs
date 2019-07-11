using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TTTTTTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("*********************************");
        string dirPath = Application.dataPath + "/StandardActionLibrary/4000080.json";
        //DirectoryInfo mydir = new DirectoryInfo(dirPath);
        if (File.Exists(dirPath))
        {
            Debug.Log("file exists");
            File.Delete(dirPath);

        }
        else
        {
            Debug.Log("file unexits");
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
