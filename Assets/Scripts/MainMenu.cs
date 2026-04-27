using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
	// Audio 
	[SerializeField] private AudioMixer		musicMixer;
	[SerializeField] private AudioMixer		sfxMixer;
	[SerializeField] private Slider			soundSlider;
	[SerializeField] private Slider			sfxSlider;

	// Resolutions dropdown
	[SerializeField] private TMP_Dropdown	resolutionsDropdown;

	// Full Screen
	[SerializeField] private Toggle			fullScreenToggle;

	// Panels
	[SerializeField] private GameObject		menuPanel;
	[SerializeField] private GameObject		optionsPanel;

	public static bool	loadSavedData;
	private List<int>	resolutionIndex = new List<int>();

	void		Start(){
		// Volume
		musicMixer.GetFloat("Volume", out float soundValue);
		sfxMixer.GetFloat("Sfx", out float sfxValue);
		soundSlider.value = soundValue;
		sfxSlider.value = sfxValue;

		// Resolutions
		Resolution[]	res = Screen.resolutions;
		List<string>	resolutionOptrion = new List<string>();

		int	currentResolutionIndex = 0;
		var	currentHz = Screen.currentResolution.refreshRateRatio.value;

		resolutionsDropdown.ClearOptions();
		for (int i = 0; i < res.Length ; i++){
			if (res[i].refreshRateRatio.value != currentHz ||
				res[i].width < 1366 || res[i].height < 768)
				continue ;

			string	option = res[i].width + " x " + res[i].height;

			resolutionOptrion.Add(option);
			resolutionIndex.Add(i);
			if (res[i].width == Screen.width && res[i].height == Screen.height)
				currentResolutionIndex = i;
		}
		resolutionsDropdown.AddOptions(resolutionOptrion);
		resolutionsDropdown.value = currentResolutionIndex;
		resolutionsDropdown.RefreshShownValue();

		// Full Screen
		fullScreenToggle.isOn = Screen.fullScreen;
	}

	public void	NewGame(){
		loadSavedData = false;
		Time.timeScale = 1;
		SceneManager.LoadScene("Level1");
	}

	public void	QuitGame(){
		Application.Quit();
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene("Title");
	}

	public void	SetResolution(int i){
		Resolution	res = Screen.resolutions[resolutionIndex[i]];

		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
	}

	public void	SetFullScreen(bool isFullScreen){
		Screen.fullScreen = isFullScreen;
	}

	public void	SetVolume(float volume){
		musicMixer.SetFloat("Volume", volume);
	}

	public void	SetSfx(float volume){
		sfxMixer.SetFloat("Sfx", volume);
	}

	public void	EnableDisableOptionsPanel(){
		menuPanel.SetActive(!menuPanel.activeSelf);
		optionsPanel.SetActive(!optionsPanel.activeSelf);
	}
}
