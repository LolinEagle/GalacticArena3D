using UnityEngine;

public class shotBehaviour : MonoBehaviour{
	[SerializeField] private float	speed = 5f;
	private Rigidbody				rb;

	void	Start(){
		rb = GetComponent<Rigidbody>();
	}

	void	Update(){
		rb.MovePosition(rb.position + transform.right * speed * Time.deltaTime);
	}

	private void	OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Enemy"))
			other.gameObject.GetComponent<EnemyAi>().TakeDamage();
		if (!other.gameObject.CompareTag("Player"))
			Destroy(gameObject);
	}
}
