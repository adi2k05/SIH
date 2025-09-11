using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bl_Joystick joystick;
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}