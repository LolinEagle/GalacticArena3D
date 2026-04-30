using UnityEngine;
using UnityEngine.InputSystem;

public class FullScreenManager : MonoBehaviour{
	[SerializeField] private MainMenu	mainMenu;

	void	Update(){
		if (Keyboard.current.f1Key.wasPressedThisFrame)
			mainMenu.SetFullScreen(!Screen.fullScreen);
	}
}
