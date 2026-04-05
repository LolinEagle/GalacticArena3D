using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour{
	[SerializeField] private TMP_Text		healText;
	[SerializeField] private TMP_Text		tpText;
	[SerializeField] private TMP_Text		scoreText;
	[SerializeField] private PlayerStats	playerStats;

	void Update(){
		healText.text = "Heal: " + playerStats.heal;
		tpText.text = "Tp: " + playerStats.tp;
		scoreText.text = "Score: " + playerStats.score;
	}
}
