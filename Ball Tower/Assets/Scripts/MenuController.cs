using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Animator menuBackgroundAnim;
    [SerializeField]
    private GameObject menuBackground;
    private bool gameJustStarted;
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject hardcoreMode;
    private Text hardcoreModeText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onStartGame += MenuOff;
        GameManager.Instance.onStartGame += GameStatusUpdate;
        GameManager.Instance.onGameOver += MenuOn;
        GameManager.Instance.onHardcoreModeToggle += HardcoreModeStatusUpdate;
        menuBackgroundAnim = menuBackground.GetComponent<Animator>();
        hardcoreModeText = hardcoreMode.GetComponent<Text>();
        gameJustStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameJustStarted !=true && Input.GetKeyDown(KeyCode.Escape)) //Input system
            ToogleMenu();
    }


    public void ToogleMenu()
    {
        menuBackgroundAnim.SetBool("OnScreen", !menuBackgroundAnim.GetBool("OnScreen"));
    }

    void MenuOff()
    {
        menuBackgroundAnim.SetBool("OnScreen", true);
        menuButton.gameObject.SetActive(true);
    }

    void MenuOn()
    {
        menuBackgroundAnim.SetBool("OnScreen", false);
        menuButton.gameObject.SetActive(false);
    }

    void GameStatusUpdate()
    {
        gameJustStarted = false;
    }

    void HardcoreModeStatusUpdate(bool isHardcoreModeOn)
    {

        if (isHardcoreModeOn)
        {
            hardcoreModeText.text = ("Hardcore Mode (On)");

        }
        if (!isHardcoreModeOn)
            hardcoreModeText.text = ("Hardcore Mode (Off)");
    }
}
