using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour{
	[SerializeField] private GameObject	pauseMenu;
	[SerializeField] private GameObject	levelBox;

	private static PauseMenu	instance = null;// Singleton pattern

	private bool	isMenuOpen = false;

	private void	Awake(){
		if (instance != null && instance != this){
			Debug.LogError("Multiple instances of PauseMenu detected");
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	private void	Update(){
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
			OpenClosePauseMenu();
	}

	public void		OpenClosePauseMenu(){
		isMenuOpen = !isMenuOpen;
		pauseMenu.SetActive(isMenuOpen);
		levelBox.SetActive(!isMenuOpen);
		Time.timeScale = isMenuOpen ? 0 : 1;
	}

	public void		ClosePauseMenu(){
		isMenuOpen = false;
		pauseMenu.SetActive(false);
		levelBox.SetActive(true);
		Time.timeScale = 1;
	}
}
