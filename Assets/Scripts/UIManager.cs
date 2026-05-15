using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
	[Header("Player Health")]
	[SerializeField] private TMP_Text	healText;
	[SerializeField] private Image		healBar1;
	[SerializeField] private Image		healBar2;
	[SerializeField] private Image		healBar3;
	[SerializeField] private TMP_Text	HealBarText;

	[Header("Teleportation")]
	[SerializeField] private TMP_Text	tpText;
	[SerializeField] private Image		tpBar;

	[Header("Multishot")]
	[SerializeField] private TMP_Text	multishotText;
	[SerializeField] private Image		multishotBar;

	[Header("Other")]
	[SerializeField] private TMP_Text	scoreText;
	[SerializeField] private GameObject	LevelBox;
	[SerializeField] private TMP_Text	LevelText;

	private static UIManager	instance = null;// Singleton pattern

	private PlayerStats	p;
	private float		timeStamp;

	private void	Awake(){
		if (instance != null && instance != this){
			Debug.LogError("Multiple instances of UIManager detected");
			Destroy(this.gameObject);
		} else {
			instance = this;
		}

		timeStamp = Time.time;
	}

	private void	Start(){
		p = FindAnyObjectByType<PlayerStats>();
		LevelText.text = $"Level {SceneManager.GetActiveScene().buildIndex}";
		if (p.level.useScore)
			LevelText.text += $"\r\nObjective : {p.level.scoreToPass}";
	}

	private void	Update(){
		// Player Health
		if (p.heal >= 0f)
			HealBarText.text = p.heal.ToString();
		healBar1.fillAmount = p.heal / 100f;
		healBar2.fillAmount = (p.heal / 100f) - 1f;
		healBar3.fillAmount = (p.heal / 100f) - 2f;

		// Teleportation
		tpBar.fillAmount = p.GetTp() / p.GetRecovery();

		// Multishot
		if (p.multishot > 20)
			multishotText.text = $"+{p.multishot - 20}";
		else
			multishotText.text = "Multishot";
		multishotBar.fillAmount = Mathf.Min(p.multishot / 20f, 1f);

		// Score
		if (p.level.useScore)
			scoreText.text = $"Score : {p.score:D3} / {p.level.scoreToPass}";
		else
			scoreText.text = $"Score : {p.score:D3}";	

		// Level Text
		if (Time.time - timeStamp > 5f)
			LevelBox.SetActive(false);
	}
}
