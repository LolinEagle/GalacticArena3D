using UnityEngine;

public class InteractBehaviour : MonoBehaviour{
	[SerializeField] private AudioSource	audioSource;
	[SerializeField] private AudioClip		shotSound;
	[SerializeField] private AudioClip		teleportationSound;
	[SerializeField] private AudioClip		dieSound;

	public void	PlayShot(){
		audioSource.PlayOneShot(shotSound);
	}

	public void	PlayTeleportation(){
		audioSource.PlayOneShot(teleportationSound);
	}

	public void	PlayDie(){
		audioSource.PlayOneShot(dieSound);
	}
}
