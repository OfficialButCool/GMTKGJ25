using UnityEngine;

public class DynamicCameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;
    public Vector2 offset = new Vector2(0, 1.5f);
    public float lookAheadDistance = 2f;
    public float lookAheadSpeed = 3f;

    private Vector3 currentVelocity;
    private Vector3 lookAheadOffset;

    private float lastTargetX;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Camera target not assigned!");
            enabled = false;
            return;
        }

        lastTargetX = target.position.x;
    }

    void LateUpdate()
    {
        float deltaX = target.position.x - lastTargetX;

        // Smooth look-ahead based on movement direction
        float lookAheadX = Mathf.Lerp(lookAheadOffset.x, Mathf.Sign(deltaX) * lookAheadDistance, Time.deltaTime * lookAheadSpeed);
        lookAheadOffset = new Vector3(lookAheadX, 0, 0);

        Vector3 desiredPosition = target.position + (Vector3)offset + lookAheadOffset;
        desiredPosition.z = transform.position.z; // Preserve original Z

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1f / followSpeed);

        lastTargetX = target.position.x;
    }
}

