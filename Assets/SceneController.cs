﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// Use this for initialization

    public void ChangeScene(int sceneId )
    {

        
        SceneManager.LoadScene(sceneId);

        
    }
}
