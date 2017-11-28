﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour {

    private const string VERSION = "v0.2 by Adrian Popovici, 2017";

    [SerializeField]
    private Text _version;

    private void Awake() {
        _version.text = VERSION;
    }

    public void OnStartGamePressed() {
        SceneManager.LoadScene("main_scene_2");
    }

    public void OnQuitPressed() {
        Application.Quit();
    }
}
