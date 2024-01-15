using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float runSpeed = 9;

    [Header("Running")]
    [SerializeField]
    private KeyCode runningKey = KeyCode.LeftShift;
    public bool IsRunning { get; private set; }
    public KeyCode crouch = KeyCode.LeftControl;
    public float targetMovingSpeed;

    new Rigidbody rigidbody;

    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed = 180f;


    public Animator AnimatorController;


    void Start()
    {
        GameManager.Instance.Player = gameObject.transform;
        rigidbody = GetComponent<Rigidbody>();
        if (TryGetComponent<Animator>(out Animator AnimatorController))
        {
            this.AnimatorController = AnimatorController;
        }
        else
        {
            Debug.Log("PlayerMovement not found 'Animator'");
        }
    }

    void FixedUpdate()
    {
        // Get targetMovingSpeed.
        if (!Input.GetKey(crouch))
            targetMovingSpeed = Input.GetKey(runningKey) ? runSpeed : speed;

        // Get targetVelocity from input.
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * targetMovingSpeed;

        // Apply movement.
        rigidbody.velocity = new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.z);

        // Rotate the player towards the movement direction.
        if (targetVelocity.magnitude > 0.01f)  // Check if there is actual movement.
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetVelocity, Vector3.up);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        AnimatorController.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);
        if (!Input.GetKey(crouch))
            targetMovingSpeed = Input.GetKey(runningKey) ? runSpeed : speed;
    }
}