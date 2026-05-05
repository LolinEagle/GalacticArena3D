using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour{
	[Header("Stats")]
	[SerializeField] private float	timeToSpawn;
	[SerializeField] private int	maxHealth;
	[SerializeField] private float	speedAddOnHit;

	private float			ennemieSpawn;
	private int				currentHealth;
	private NavMeshAgent	agent;
	private Material		material;
	private Transform		player;

	private void	Awake(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if (player == null){
			Debug.LogError("Player not found");
			Destroy(gameObject);
		}

		ennemieSpawn = Time.time;
		currentHealth = maxHealth;
		agent = GetComponent<NavMeshAgent>();
		material = GetComponent<Renderer>().material;
	}

	private void	FixedUpdate(){
		if (IsSpawn()){
			agent.SetDestination(player.position);
		} else {
			float	s = Mathf.Min((Time.time - ennemieSpawn) / timeToSpawn, 1f);
			transform.localScale = new Vector3(s, transform.localScale.y, s);
		}
	}

	private void	OnCollisionStay(Collision collision){
		if (IsSpawn() && collision.gameObject.CompareTag("Player"))
			player.GetComponent<PlayerStats>().heal--;
	}

	private bool	IsSpawn(){
		return (Time.time >= ennemieSpawn + timeToSpawn);
	}

	public void		TakeDamage(){
		if (!IsSpawn())
			return ;
		if (--currentHealth <= 0){
			// Die
			player.GetComponent<PlayerAudio>().PlayDie();
			player.GetComponent<PlayerStats>().score++;
			Destroy(gameObject);
		} else {
			// Get faster, smaller and change color
			transform.localScale -= new Vector3(0.1f, 0.0f, 0.1f);
			agent.speed += speedAddOnHit;
			material.color = currentHealth == 2 ? Color.yellow : Color.red;
		}
	}
}
