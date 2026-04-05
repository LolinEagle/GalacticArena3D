using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour{
	[Header("Stats")]
	[SerializeField] private int	maxHealth;
	[SerializeField] private float	speedAddOnHit;

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
		currentHealth = maxHealth;
		agent = GetComponent<NavMeshAgent>();
		material = GetComponent<Renderer>().material;
	}

	void			Update(){
		agent.SetDestination(player.position);
	}

	private void	OnCollisionStay(Collision collision){
		if (collision.gameObject.CompareTag("Player"))
			player.GetComponent<PlayerStats>().heal--;
	}

	public void		TakeDamage(){
		if (--currentHealth <= 0){
			// Die
			player.GetComponent<InteractBehaviour>().PlayDie();
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
