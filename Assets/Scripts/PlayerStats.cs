using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour{
	[Header("Stats")]
	public float	heal = 100f;
	public float	tp = 0f;
	private float	tpTime = 0f;
	public float	tpRecovery = 15f;
	public int		score = 0;

	void	Update(){
		if (heal <= 0f)
			SceneManager.LoadScene("Level");
		tp = Mathf.Min(Time.time - tpTime, tpRecovery);
	}

	void	Tp(){
		tpTime = Time.time;
	}
}
