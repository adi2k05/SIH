using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // assign Player here
    public Vector3 offset = new Vector3(0, 10, -10);

    void LateUpdate()
    {
        if (target == null) return;

        // Follow target position + offset
        transform.position = target.position + offset;
    }
}