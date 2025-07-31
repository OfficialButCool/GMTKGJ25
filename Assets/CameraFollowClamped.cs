using UnityEngine;

public class CameraFollowHalfScreen : MonoBehaviour
{
    public Transform target; // Assign the player this camera follows
    public float followSpeed = 5f;

    public Vector2 offset = new Vector2(0f, 1.5f); // Offset from target position
    public bool isLeftSide = true; // Is this the left or right half of the screen?

    public float horizontalAdjust = 0f; // Manual tweak to perfect centering

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component is missing!");
        }
    }

    void LateUpdate()
    {
        if (target == null || cam == null) return;

        // Calculate how wide half the screen is in world units
        float screenHalfOffset = cam.orthographicSize * cam.aspect * 0.5f;

        // Adjust the camera offset depending on whether it's left or right screen
        float xOffset = (isLeftSide ? screenHalfOffset : -screenHalfOffset) + horizontalAdjust;

        // Calculate desired position
        Vector3 desiredPosition = new Vector3(
            target.position.x + xOffset + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
    }
}
