using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerBasicBehaviour : MonoBehaviour{
	[Header("References")]
	[SerializeField] private GameObject	shotPrefab;
	[SerializeField] private Transform	cannon;

	[Header("Property")]
	[SerializeField] private float	speed;
	[SerializeField] private float	shotsPerSecond;

	private static PlayerBasicBehaviour	instance = null;// Singleton pattern

	// Component
	private Rigidbody	rb;
	private PlayerAudio	playerAudio;
	private PlayerStats	playerStats;
	private Transform	playerCamera;

	Vector3				moveInput;		// Movement direction
	private Quaternion	targetRotation;	// Store rotation for FixedUpdate
	private float		nextFireTime;	// Timestamp of the time player can fire

	private void	Awake(){
		if (instance != null && instance != this){
			Debug.LogError("Multiple instances of Player detected");
			Destroy(this.gameObject);
		} else {
			instance = this;
		}

		rb = GetComponent<Rigidbody>();
		playerAudio = GetComponent<PlayerAudio>();
		playerStats = GetComponent<PlayerStats>();
	}

	private void	Start(){
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
		targetRotation = Quaternion.Euler(0f, 0f, 0f);
		nextFireTime = 0f;
	}

	private void	Update(){
		// Move
		moveInput = Vector3.zero;
		if (Keyboard.current.wKey.isPressed) moveInput += Vector3.forward;
		if (Keyboard.current.sKey.isPressed) moveInput -= Vector3.forward;
		if (Keyboard.current.aKey.isPressed) moveInput -= Vector3.right;
		if (Keyboard.current.dKey.isPressed) moveInput += Vector3.right;

		// Rotate
		Mouse	mb = Mouse.current;
		Vector2	mousePos = mb.position.ReadValue();
		Vector2	center = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Vector2	direction = mousePos - center;
		float	angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		targetRotation = Quaternion.Euler(0f, -angle, 0f);

		// Shot
		if (mb.leftButton.isPressed && Time.time >= nextFireTime){
			if (playerStats.multishot > 0){
				Shot(12f);
				Shot(-12f);
				playerStats.multishot--;
			}
			Shot();
			nextFireTime = Time.time + (1f / shotsPerSecond);
		}

		// Teleportation
		if (mb.rightButton.wasPressedThisFrame && playerStats.CanTp()){
			// Create a ray from the camera through the mouse position
			Ray			ray = Camera.main.ScreenPointToRay(mousePos);
			RaycastHit	hit;

			// Cast the ray to see where it hits the world
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				NavMeshHit	navHit;// Check if that position is on the NavMesh
				float		maxDistance = 1.0f;

				// Returns true if a valid point is found
				if (NavMesh.SamplePosition(
					hit.point, out navHit, maxDistance, NavMesh.AllAreas
				)){
					Vector3	tmp = navHit.position;
					tmp.y = transform.position.y;
					transform.position = tmp;

					playerStats.Tp();
					playerAudio.PlayTeleportation();
				}
			}
		}
	}

	private void	LateUpdate(){
		playerCamera.position = transform.position + new Vector3(0, 8f, 0);
	}

	private void	FixedUpdate(){
		// Move
		if (moveInput != Vector3.zero)
			MovePlayer(moveInput.normalized);

		// Apply rotation
		rb.MoveRotation(targetRotation);
	}

	private void	Shot(float offset = 0f){
		Transform	t = Instantiate(shotPrefab).transform;
		t.position = cannon.position;
		t.rotation = transform.rotation * Quaternion.Euler(0f, offset, 0f);
		playerAudio.PlayShot();
	}

	private void	MovePlayer(Vector3 direction){
		rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}
}
