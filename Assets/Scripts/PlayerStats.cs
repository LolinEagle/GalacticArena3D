using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
	[Header("Stats")]
	public float	heal = 100f;
	private float	tp = 0f;
	private float	tpTime;
	private float	tpRecovery = 15f;
	public int		score = 0;

	void	Awake(){
		tpTime = Time.time - tpRecovery;
	}

	void	Update(){
		// Heal
		if (heal <= 0f)
			SceneManager.LoadScene("Level");

		// Teleportation
		tp = Mathf.Min(Time.time - tpTime, tpRecovery);

		// Score
		if (score >= 100)
			SceneManager.LoadScene("Title");
	}

	public float	GetTp(){
		return (tp);
	}

	public bool		CanTp(){
		return (tp >= tpRecovery);
	}

	public void		Tp(){
		tpTime = Time.time;
	}
}
