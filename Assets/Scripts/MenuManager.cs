using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public GameObject configPanel, creditsPanel;
    public GameObject lastSelected;

    private void Awake() {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        if (configPanel)
            configPanel.SetActive(false);
        if (creditsPanel)
            creditsPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (configPanel && configPanel.activeSelf) {
                toggleConfigPanel();

            } else if (creditsPanel && creditsPanel.activeSelf) {
                toggleCreditsPanel();
            }
        }
    }

    public void startGame() {
        SceneManager.LoadScene(1);
    }

    public void GoStartMenu() {
        SceneManager.LoadScene(0);
    }

    public void exitGame() {
        Application.Quit();
    }

    public void toggleConfigPanel() {
        configPanel.SetActive(!configPanel.activeSelf);
        if (configPanel.activeSelf) {
            //EventSystem.current.
        }

    }

    public void toggleCreditsPanel() {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void closeConfigPanel() {
        toggleConfigPanel();
    }
}
