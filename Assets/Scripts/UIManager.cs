using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
	[Header("Player Health")]
	[SerializeField] private TMP_Text		healText;
	[SerializeField] private Image			healBar1;
	[SerializeField] private Image			healBar2;
	[SerializeField] private Image			healBar3;
	[SerializeField] private TMP_Text		HealBarText;

	[Header("Teleportation")]
	[SerializeField] private TMP_Text		tpText;
	[SerializeField] private Image			tpBar;

	[Header("Multishot")]
	[SerializeField] private TMP_Text		multishotText;
	[SerializeField] private Image			multishotBar;

	[Header("Other")]
	[SerializeField] private TMP_Text		scoreText;
	[SerializeField] private GameObject		levelTextBox;

	private static UIManager	instance = null;// Singleton pattern

	private PlayerStats	playerStats;
	private string		objective = "100";
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
		playerStats = FindAnyObjectByType<PlayerStats>();
	}

	private void	Update(){
		// Player Health
		if (playerStats.heal >= 0f)
			HealBarText.text = playerStats.heal.ToString();
		healBar1.fillAmount = playerStats.heal / 100f;
		healBar2.fillAmount = (playerStats.heal / 100f) - 1f;
		healBar3.fillAmount = (playerStats.heal / 100f) - 2f;

		// Teleportation
		tpBar.fillAmount = playerStats.GetTp() / playerStats.GetRecovery();

		// Multishot
		if (playerStats.multishot > 20)
			multishotText.text = $"+{playerStats.multishot - 20}";
		else
			multishotText.text = "Multishot";
		multishotBar.fillAmount = Mathf.Min(playerStats.multishot / 20f, 1f);

		// Score
		scoreText.text = $"Score : {playerStats.score:D3} / {objective}";

		// Level Text
		if (Time.time - timeStamp > 5f) levelTextBox.SetActive(false);
	}
}
