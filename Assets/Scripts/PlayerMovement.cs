using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input (WASD or Arrow keys for now)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Movement direction
        Vector3 move = new Vector3(moveX, 0, moveZ);

        // Apply movement
        rb.MovePosition(transform.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}