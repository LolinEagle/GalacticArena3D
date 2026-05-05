using UnityEngine;
using UnityEngine.InputSystem;

public class FullScreenManager : MonoBehaviour{
	[SerializeField] private MainMenu	mainMenu;

	private static FullScreenManager	instance = null;// Singleton pattern

	private void	Awake(){
		if (instance != null && instance != this){
			Debug.LogError("Multiple instances of FullScreenManager detected");
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	private void	Update(){
		if (Keyboard.current.f1Key.wasPressedThisFrame)
			mainMenu.SetFullScreen(!Screen.fullScreen);
	}
}
