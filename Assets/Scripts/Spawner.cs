using UnityEngine;

public class Spawner : MonoBehaviour{
	[Header("Ennemie Spawn Settings")]
	[SerializeField] private GameObject	ennemiePrefab;
	[SerializeField] private float		spawnRate;

	[HideInInspector] public float	spawnRandom;

	private Transform	player;
	private Transform	parent;

	private void	Awake(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if (player == null){
			Debug.LogError("Player not found");
			Destroy(gameObject);
		}
		parent = GameObject.FindGameObjectWithTag("InstantiateLayer").transform;
	}

	private void	OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag("Player"))
			player.GetComponent<PlayerStats>().heal--;
	}

	private void Update() {
		if (Time.time >= spawnRandom){
			Instantiate(
				ennemiePrefab, transform.position, Quaternion.identity, parent
			);
			spawnRandom = Time.time + spawnRate;
		}
	}
}
