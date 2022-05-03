using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Volume settings")] 
    [SerializeField] private TMP_Text VolumeValue = null;
    [SerializeField] private Slider VolumeSlider = null;
    
    [Header("Gameplay settings")]
    [SerializeField] private TMP_Text SensValue = null;
    [SerializeField] private Slider SensSlider = null;
    public int mainSensValue = 4;
    
    [Header("Graphics settings")]
    [SerializeField] private TMP_Text BrightnessValue = null;
    [SerializeField] private Slider BrightnessSlider = null;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")] 
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        VolumeValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetSensivity(float sens)
    {
        mainSensValue = Mathf.RoundToInt(sens);
        SensValue.text = sens.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterSens", mainSensValue);
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        BrightnessValue.text = brightness.ToString("0.0");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0));
        Screen.fullScreen = _isFullscreen;
    }
}
