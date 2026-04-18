using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour{
	[Header("References")]
	[SerializeField] private GameObject			shotPrefab;
	[SerializeField] private InteractBehaviour	interactBehaviour;
	[SerializeField] private Transform			cannon;

	[Header("Property")]
	[SerializeField] private float	speed = 5f;
	[SerializeField] private float	shotsPerSecond;

	private float		nextFireTime;
	private Transform	playerCamera;
	private Rigidbody	rb;
	private PlayerStats	playerStats;
	private Quaternion	targetRotation;// Store rotation for FixedUpdate

	void	Start(){
		nextFireTime = 0f;
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
		rb = GetComponent<Rigidbody>();
		playerStats = GetComponent<PlayerStats>();
		targetRotation = Quaternion.Euler(0f, 0f, 0f);
	}

	void	Update(){
		// Rotate
		Vector2	mousePos = Mouse.current.position.ReadValue();
		Vector2	center = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Vector2	direction = mousePos - center;
		float	angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		targetRotation = Quaternion.Euler(0f, -angle, 0f);

		// Shot
		if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime){
			if (playerStats.multishot > 0){
				Shot(12f);
				Shot(-12f);
				playerStats.multishot--;
			}
			Shot();
			nextFireTime = Time.time + (1f / shotsPerSecond);
		}

		// Teleportation
		if (Mouse.current.rightButton.isPressed && playerStats.CanTp()){
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
					interactBehaviour.PlayTeleportation();
				}
			}
		}
	}

	void	LateUpdate(){
		playerCamera.position = transform.position + new Vector3(0, 8f, 0);
	}

	void	FixedUpdate(){
		// Move
		Vector3	moveInput = Vector3.zero;

		if (Keyboard.current.wKey.isPressed) moveInput += Vector3.forward;
		if (Keyboard.current.sKey.isPressed) moveInput -= Vector3.forward;
		if (Keyboard.current.aKey.isPressed) moveInput -= Vector3.right;
		if (Keyboard.current.dKey.isPressed) moveInput += Vector3.right;
		if (moveInput != Vector3.zero)
			MovePlayer(moveInput.normalized);

		// Apply rotation
		rb.MoveRotation(targetRotation);
	}

	void	Shot(float offset = 0f){
		GameObject	shot = Instantiate(shotPrefab);
		shot.transform.position = cannon.position;
		shot.transform.rotation = transform.rotation * Quaternion.Euler(0f, offset, 0f);
		interactBehaviour.PlayShot();
	}

	void	MovePlayer(Vector3 direction){
		rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}
}
