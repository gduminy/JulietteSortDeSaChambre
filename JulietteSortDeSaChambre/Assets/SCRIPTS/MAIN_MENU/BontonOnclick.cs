﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BontonOnclick : MonoBehaviour {

    public string NomDeLaSceneaCharger;

    public void LancerPartie()
    {
        SceneManager.LoadScene(NomDeLaSceneaCharger);
    }
    public void QuiterJeu()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
