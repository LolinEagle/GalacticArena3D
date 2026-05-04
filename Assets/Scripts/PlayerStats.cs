using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour{
	[Header("Stats")]
	public float					heal = 100f;
	[SerializeField] private float	maxHeal = 300f;
	private float					tp;
	private float					tpTime;
	[SerializeField] private float	tpRecovery = 15f;
	public int						multishot;

	[Header("Current score")]
	public int	score;

	[Header("Levels list")]
	[SerializeField] private Level[]	levels;
	
	private EnnemieSpawnRandom	ennemieSpawnRandom;
	private Image				levelEndScreen;
	private float				iLevelEnded = 0f;
	private bool				levelEnded = false;

	void	Awake(){
		tpTime = Time.time - tpRecovery;
		ennemieSpawnRandom = FindAnyObjectByType<EnnemieSpawnRandom>();
		levelEndScreen = GameObject.FindGameObjectWithTag("Finish")?.GetComponent<Image>();
	}

	void	Update(){
		// Heal
		heal = Mathf.Min(heal, maxHeal);
		if (heal <= 0f)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		// Teleportation
		tp = Mathf.Min(Time.time - tpTime, tpRecovery);

		// Score
		Level	level = levels[SceneManager.GetActiveScene().buildIndex - 1];
		if (level.useScore && score >= level.scoreToPass){
			if (levelEnded == false){
				ennemieSpawnRandom.spawnRandom = float.MaxValue;
				ennemieSpawnRandom.spawnRandomBonus = float.MaxValue;

				GameObject[]	allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (GameObject foe in allEnemy)
					Destroy(foe);

				iLevelEnded = Time.time + 5f;
				levelEnded = true;
			}
			Color	tmp = levelEndScreen.color;
			tmp.a = 1f - ((iLevelEnded - Time.time) / 5f);
			levelEndScreen.color = tmp;

			if (Time.time >= iLevelEnded)
				SceneManager.LoadScene(level.targetRoom);
		}
	}

	public float	GetTp(){
		return (tp);
	}

	public float	GetRecovery(){
		return (tpRecovery);
	}

	public bool		CanTp(){
		return (tp >= tpRecovery);
	}

	public void		Tp(){
		tpTime = Time.time;
	}

	public void		BonusTp(){
		tpTime = Time.time - tpRecovery;
	}

	[System.Serializable]
	public class Level{
		public bool		useScore = true;
		public int		scoreToPass = 100;
		public string	targetRoom;
	}
}
