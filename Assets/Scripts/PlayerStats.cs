using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
	[Header("Stats")]
	public float					heal = 100f;
	[SerializeField] private float	maxHeal = 300f;
	private float					tp;
	private float					tpTime;
	[SerializeField] private float	tpRecovery = 15f;
	public int						score;
	public int						multishot;

	void	Awake(){
		tpTime = Time.time - tpRecovery;
	}

	void	Update(){
		// Heal
		heal = Mathf.Min(heal, maxHeal);
		if (heal <= 0f)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		// Teleportation
		tp = Mathf.Min(Time.time - tpTime, tpRecovery);

		// Score
		if (score >= 100)
			SceneManager.LoadScene("Title");
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
}
