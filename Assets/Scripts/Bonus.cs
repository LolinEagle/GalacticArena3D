using UnityEngine;

public class Bonus : MonoBehaviour{
	private int							typeBonus;	// Type of the bonus
	private Material					material;	// Material of the sphere
	[SerializeField] private GameObject	model;		// Child plane model
	[SerializeField] private Material[]	materials;	// List for child materials

	private void	Start(){
		typeBonus = Random.Range(0, 3);
		material = GetComponent<Renderer>().material;
		switch (typeBonus){
			case 0:
				material.color = new Color(1f, 0f, 0f, material.color.a);
				break;
			case 1:
				material.color = new Color(0f, 1f, 0f, material.color.a);
				break;
		}
		model.GetComponent<Renderer>().material = materials[typeBonus];
	}

	private void	OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Player")){
			PlayerStats	stats = other.gameObject.GetComponent<PlayerStats>();
			switch (typeBonus){
				case 0:
					stats.multishot += 20;
					break;
				case 1:
					stats.heal += 25f;
					break;
				case 2:
					stats.heal += 10f;
					stats.BonusTp();
					break;
			}
			Destroy(gameObject);
		}
	}
}
