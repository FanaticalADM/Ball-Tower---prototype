using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject joystickMove;
    [SerializeField]
    private GameObject joystickJump;
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject resumeButton;
    bool isGameOver;

    private void Start()
    {
        GameManager.Instance.onGameOver += isGameOverCheck;
        GameManager.Instance.onStartGame += isGameNotOverCheck;
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        JoysticSetup(false);
    }

    private void Update()
    {
        ResumeButtonCheck();
    }

    //function in animation
    void SetMenuOn()
    {
        menu.SetActive(true);
        resumeButton.SetActive(true);
        menuButton.SetActive(false);
        JoysticSetup(false);
    }

    //function in animation
    void SetMenuOff()
    {
        menuButton.SetActive(true);
        menu.SetActive(false);
        resumeButton.SetActive(false);
        JoysticSetup(false);
    }

    void JoysticSetup(bool state)
    {
        joystickJump.SetActive(state);
        joystickMove.SetActive(state);
    }

    void ResumeButtonCheck()
    {
        if (isGameOver)
        {
            resumeButton.SetActive(false);
        }
    }

    void isGameOverCheck()
    {
        isGameOver=true;
    }

    void isGameNotOverCheck()
    {
        isGameOver = false;
    }

}
