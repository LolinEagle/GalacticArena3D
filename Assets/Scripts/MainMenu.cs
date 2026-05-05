using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
	// Options
	[SerializeField] private AudioMixer		musicMixer;
	[SerializeField] private AudioMixer		sfxMixer;
	[SerializeField] private Slider			soundSlider;
	[SerializeField] private Slider			sfxSlider;
	[SerializeField] private TMP_Dropdown	resolutionsDropdown;
	[SerializeField] private Toggle			fullScreenToggle;
	[SerializeField] private GameObject		optionsPanel;

	private static MainMenu	instance = null;// Singleton pattern

	private List<int>	resolutionIndex = new List<int>();
	private GameObject	menuPanel;
	private PauseMenu	pauseMenu;

	private void	Awake(){
		if (instance != null && instance != this){
			Debug.LogError("Multiple instances of UIManager detected");
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	private void	Start(){
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

		// Menu
		menuPanel = transform.gameObject;
		pauseMenu = FindAnyObjectByType<PauseMenu>();
	}

	// Buttons
	public void	NewGame(){
		Time.timeScale = 1f;
		SceneManager.LoadScene("Level1");
	}

	public void	ContinueGame(){
		Time.timeScale = 1f;
	}

	public void	EnableDisableOptionsPanel(){
		menuPanel.SetActive(!menuPanel.activeSelf);
		optionsPanel.SetActive(!optionsPanel.activeSelf);
	}

	public void	QuitGame(){
		Application.Quit();
	}

	public void	ReturnGame(){
		pauseMenu.ClosePauseMenu();
	}

	public void	LoadMainMenu(){
		SceneManager.LoadScene("Title");
	}

	// Options
	public void	SetResolution(int i){
		Resolution	res = Screen.resolutions[resolutionIndex[i]];

		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
	}

	public void	SetFullScreen(bool isFullScreen){
		Screen.fullScreen = isFullScreen;
		fullScreenToggle.SetIsOnWithoutNotify(isFullScreen);
	}

	public void	SetVolume(float volume){
		musicMixer.SetFloat("Volume", volume);
	}

	public void	SetSfx(float volume){
		sfxMixer.SetFloat("Sfx", volume);
	}
}
