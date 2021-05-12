using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    public Toggle fullscreenToggle;
    public Resolution[] resolutions;
    public TMP_Dropdown resolutionsDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public Slider mouseSensitivitySlider;
    public Button back;

    public void Start() {
        fullscreenToggle = GameObject.Find("FullscreenToggle").GetComponent<Toggle>();
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        fullscreenToggle.isOn = GameManager.isFullscreen;

        resolutions = Screen.resolutions;
        resolutionsDropdown = GameObject.Find("ResolutionsDropdown").GetComponent<TMP_Dropdown>();
        resolutionsDropdown.ClearOptions();
        List<string> resolutionsOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            resolutionsOptions.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(resolutionsOptions);
        resolutionsDropdown.onValueChanged.AddListener(SetResolution);
        resolutionsDropdown.value = GameManager.resolutionIndex == -1? currentResolutionIndex: GameManager.resolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        qualityDropdown = GameObject.Find("QualityDropdown").GetComponent<TMP_Dropdown>();
        qualityDropdown.ClearOptions();
        List<string> qualityOptions = new List<string> {"Very Low", "Low", "Medium", "High", "Very High", "Ultra"};
        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        qualityDropdown.value = GameManager.qualityIndex;

        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.value = GameManager.volume;

        mouseSensitivitySlider = GameObject.Find("MouseSensitivitySlider").GetComponent<Slider>();
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        mouseSensitivitySlider.value = GameManager.mouseSensitivity;

        back = GameObject.Find("BackButton").GetComponent<Button>();
        back.onClick.AddListener(Back);
    }
    public void SetFullscreen(bool isFullscreen) {
        GameManager.isFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int index) {
        GameManager.resolutionIndex = index;
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int index) {
        GameManager.qualityIndex = index;
        QualitySettings.SetQualityLevel(index);
    }
    public void SetVolume(float volume) {
        GameManager.volume = volume;
        audioMixer.SetFloat("volume", volume);
    }
    public void SetMouseSensitivity(float sensitivity) {
        GameManager.mouseSensitivity = sensitivity;
    }
    public void Back() {
        SceneManager.LoadScene("Menu");
    }
}
