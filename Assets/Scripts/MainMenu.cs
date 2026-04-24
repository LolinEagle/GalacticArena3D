using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour{
	[SerializeField] private Dropdown	resolutionsDropdown;
	[SerializeField] private AudioMixer	audioMixer;
	[SerializeField] private Slider		soundSlider;
	[SerializeField] private GameObject	menuPanel;
	[SerializeField] private GameObject	optionsPanel;
	[SerializeField] private Toggle		fullScreenToggle;

	public static bool	loadSavedData;
	private List<int>	resolutionIndex = new List<int>();

	void		Start(){
		// Volume
		audioMixer.GetFloat("Volume", out float soundValue);
		soundSlider.value = soundValue;

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
			if (res[i].width == Screen.width && res[i].height == Screen.height){
				currentResolutionIndex = i;
			}
		}
		resolutionsDropdown.AddOptions(resolutionOptrion);
		resolutionsDropdown.value = currentResolutionIndex;
		resolutionsDropdown.RefreshShownValue();

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

	public void	SetQuality(int qualityIndex){
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void	SetResolution(int i){
		Resolution	res = Screen.resolutions[resolutionIndex[i]];

		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
	}

	public void	SetFullScreen(bool isFullScreen){
		Screen.fullScreen = isFullScreen;
	}

	public void	SetVolume(float volume){
		audioMixer.SetFloat("Volume", volume);
	}

	public void	EnableDisableOptionsPanel(){
		menuPanel.SetActive(!menuPanel.activeSelf);
		optionsPanel.SetActive(!optionsPanel.activeSelf);
	}
}
