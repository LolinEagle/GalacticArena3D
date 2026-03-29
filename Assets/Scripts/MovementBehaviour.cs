using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour{
	[Header("References")]
	[SerializeField] private Transform			playerCamera;
	[SerializeField] private GameObject			shotPrefab;
	[SerializeField] private InteractBehaviour	interactBehaviour;

	[Header("Property")]
	[SerializeField] private float	speed = 5f;
    [SerializeField] private float	shotsPerSecond = 9f;
    [SerializeField] private float	nextFireTime = 0f; 

	private Rigidbody	rb;

	void	Start(){
		rb = GetComponent<Rigidbody>();
	}

	void	MovePlayer(Vector3 direction){
		rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
	}

	void	Update(){
		// Camera
		playerCamera.position = transform.position + new Vector3(0, 8f, 0);

		// Rotate
		Vector2	mousePos = Mouse.current.position.ReadValue();
		Vector2	center = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Vector2	direction = mousePos - center;
		float	angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Vector3	rotationAngle = new Vector3(0, -angle, 0);
		Quaternion	deltaRotation = Quaternion.Euler(rotationAngle);

		rb.MoveRotation(deltaRotation);

		// Shot
		if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime){
			GameObject	shot = Instantiate(shotPrefab);
			shot.transform.position = transform.position;
			shot.transform.rotation = transform.rotation;
			interactBehaviour.PlayShot();
			nextFireTime = Time.time + (1f / shotsPerSecond);
		};
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
	}
}
