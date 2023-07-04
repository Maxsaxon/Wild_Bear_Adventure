using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuButtonScript menuContainer;


    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("FullScreen Setting")]
     [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity Setting")]
    [SerializeField] private TMP_Text ControllerSensTextValue = null;
    [SerializeField] private Slider ControllerSensSlider = null;

    [Header("Invert Y Setting")]
    [SerializeField] private Toggle InvertYToggle = null;

    void Awake()
    {
        if(canUse) 
        {
            if(PlayerPrefs.HasKey("masterVolume")) // checking if we have saved data about volume
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume"); // means getting value from masterVolume into localVolume 

                volumeTextValue.text = localVolume.ToString("0.0"); // setting up volume values to current volume 
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuContainer.ResetButton("Audio");
            }

            if(PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterquality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality); // calling quality settings to set quality level
            }

            if(PlayerPrefs.HasKey("masterFullScreen"))
            {
                int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");

                if(localFullScreen == 1) // means full screen is on so it's true
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else // if it's off, so false
                {
                     Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }

            if(PlayerPrefs.HasKey("masterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
                // change the brightness
            }

            if(PlayerPrefs.HasKey("masterSens"))
            {
                float localSens = PlayerPrefs.GetFloat("masterSens");

                ControllerSensTextValue.text = localSens.ToString("0");
                ControllerSensSlider.value = localSens;
                menuContainer.mainControllerSens = Mathf.RoundToInt(localSens); // the actual mainControllerSensis the localSens rounded to an int
            }

            if(PlayerPrefs.HasKey("masterInvertY"))
            {
                if(PlayerPrefs.GetInt("masterInvertY") == 1) // if Get Integer is equal to 1
                {
                    InvertYToggle.isOn = true;
                }
                else // if Get Int is not equal  to 1
                {
                    InvertYToggle.isOn = false;
                }
            }
        }
    }
    
}

