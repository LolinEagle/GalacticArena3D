using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{
	[Header("References")]
	[SerializeField] private PlayerStats	playerStats;

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

	private string	objective = "100";
	private float	timeStamp;

	void	Start(){
		timeStamp = Time.time;
	}

	void	Update(){
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
