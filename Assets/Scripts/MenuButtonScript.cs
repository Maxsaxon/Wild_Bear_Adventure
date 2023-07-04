using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtonScript : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null; // null, 'cause it is a text box
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;

    [Header("GamePlay Settings")]
    [SerializeField] private TMP_Text ControllerSensTextValue = null;
    [SerializeField] private Slider ControllerSensSlider = null;
    [SerializeField] private int defaultSens = 5;
    public int mainControllerSens = 5; // we need to declare this int second time as public for accessing it to other scripts

    [Header("Toggle Settings")]
    [SerializeField] private Toggle InvertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;
 
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;


    private void Start()
    {
        resolutions = Screen.resolutions; //setting resolutions
        resolutionDropdown.ClearOptions(); // we need to clear whatever in our current dropdown at the moment

        List<string> options = new List<string>(); // listing all different types of strings

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) // search through the length of the array
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; // we'll be able to put into a string the width and the height of those
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height); // check to see if the resolutions, we found, are equal to screen width/height
            {
                currentResolutionIndex = i; // set to resolution we want to choose
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
       GraphicsApply(); // Warning!!! check this more
       
       
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void NewGameDialogYes() // when we click yes button, we will load scene we want
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes() // loading the game if chosing yes
    {
        if(PlayerPrefs.HasKey("SavedLevel")) // check if we have file "SavedLevel"
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel"); // if do, we'll say, our local var of level to load is equal to the playerpref.getstring from this file
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit(); // means we quitting the Game
    }

    public void SetVolume(float volume) // this will set up volume values, change audio listener 
    {
        AudioListener.volume = volume; // we change audiolistener volume to any value we selected
        volumeTextValue.text = volume.ToString("0.0");// it allows us when specifizing this value, whatever we change it, we'll update that, so you see it
    }

    public void VolumeApply() // actions when applying
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        //show Prompt
        StartCoroutine(ConfirmationBox());
    }

    public void SetControlSens(float sensitivity)
    {
        mainControllerSens = Mathf.RoundToInt(sensitivity);
        ControllerSensTextValue.text = sensitivity.ToString("0");
    }

    public void GamePlayApply()
    {
        if (InvertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1); // 1 means true 
            // invert Y
        }
        else 
        {
            PlayerPrefs.SetInt("masterInvertY", 0); // 0 means false
            // not invert Y
        }

        PlayerPrefs.SetFloat("masterSens", mainControllerSens); // set to default sens value
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        // Change your brightness with post processing or whatever it is
       
        //Playerprefs, QualitySettings means already existing gameobjects in unity. You can write them in code
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Graphics")
        {
            // reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1; // 1 means medium value
            QualitySettings.SetQualityLevel(1);
            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if(MenuType == "GamePlay")
        {
            ControllerSensTextValue.text = defaultSens.ToString("0");
            ControllerSensSlider.value = defaultSens;
            mainControllerSens = defaultSens;
            InvertYToggle.isOn = false;
            GamePlayApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
