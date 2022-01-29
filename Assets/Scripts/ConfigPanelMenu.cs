using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigPanelMenu : MonoBehaviour {
    public GameObject configPanel;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        configPanel.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowConfigPanel();
        }
    }

    public void ShowConfigPanel() {
        configPanel.SetActive(!configPanel.activeSelf);
        if (!configPanel.activeSelf) {
            Time.timeScale = 1;
        } else {
            Debug.Log("PAUSAO");
            Time.timeScale = 0;
        }
    }

    public void backToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
