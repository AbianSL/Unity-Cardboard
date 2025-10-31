using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool enableGazeMovement = true;
    public bool requireButtonForGaze = true;

    void Start()
    {
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0.05f || Mathf.Abs(vertical) > 0.05f)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            forward.y = 0; 
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * vertical + right * horizontal).normalized * moveSpeed * Time.deltaTime;
        }
        else if (enableGazeMovement)
        {
            bool moveByGaze = !requireButtonForGaze || Input.GetMouseButton(0) || Input.touchCount > 0;
            if (moveByGaze)
            {
                Vector3 forward = transform.forward;
                forward.y = 0;
                forward.Normalize();
                
                moveDirection = forward * moveSpeed * Time.deltaTime;
            }
        }

        transform.position += moveDirection;
    }
}
