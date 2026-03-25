using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour{
	[Header("Reference")]
	[SerializeField] private NavMeshAgent	agent;

	[Header("Stats")]
	[SerializeField] private int	maxHealth;
	[SerializeField] private int	currentHealth;
	[SerializeField] private float	speed;

	private Transform	player;

	private void	Awake(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		currentHealth = maxHealth;
		agent.speed = speed;
	}

	void			Update(){
		agent.SetDestination(player.position);
	}

	public void		TakeDamage(int damages = 1){
		currentHealth -= damages;
		if (currentHealth <= 0) Destroy(gameObject);
	}
}
