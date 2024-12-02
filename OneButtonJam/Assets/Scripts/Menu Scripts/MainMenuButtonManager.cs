using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtonManager : MonoBehaviour
{
    [field : Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button selectCharButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;
    

    [field : Header("In Scene Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingMenu;

    void Awake(){
        backButton.onClick.AddListener(onBackButtonClick);
        startButton.onClick.AddListener(OnStartButtonClick);
        settingButton.onClick.AddListener(OnSettingButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    void OnStartButtonClick(){
        Debug.Log("Going to Start menu");
        SceneManagerScript.Instance.LoadSceneByName("CharacterSelection");
    }

    void OnSettingButtonClick(){
        Debug.Log("Going to Settings");
        mainMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    void onBackButtonClick(){
        Debug.Log("Going back to Main Menu");
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void OnQuitButtonClicked(){
        Debug.Log("Going to Desktop");
        Application.Quit();
    }
}
