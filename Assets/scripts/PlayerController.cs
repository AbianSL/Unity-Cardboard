using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool enableGazeMovement = true;
    public bool requireButtonForGaze = false;
    public string cameraTag = "Player";
    
    private Transform camTransform;
    private bool isTouchActive = false;

    void Start()
    {
        GameObject camObject = GameObject.FindWithTag(cameraTag);
        if (camObject != null)
        {
            camTransform = camObject.transform;
            Debug.Log("Camera found: " + camObject.name);
        }
        else
        {
            Debug.LogError("Camera with tag '" + cameraTag + "' not found in the scene.");
        }
    }

    void Update()
    {
        if (camTransform == null)
            return;

        Vector3 moveDirection = Vector3.zero;

        float horizontal = 0f;
        float vertical = 0f;

        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            horizontal = stick.x;
            vertical = stick.y;
        }

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horizontal = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horizontal = 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                vertical = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                vertical = -1f;
        }

        if (Mathf.Abs(horizontal) > 0.05f || Mathf.Abs(vertical) > 0.05f)
        {
            Vector3 forward = camTransform.forward;
            Vector3 right = camTransform.right;

            forward.y = 0; 
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * vertical + right * horizontal).normalized * moveSpeed * Time.deltaTime;
        }
        else if (enableGazeMovement)
        {
            isTouchActive = false;

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            {
                isTouchActive = true;
                Debug.Log("Touch detected");
            }

            if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            {
                isTouchActive = true;
                Debug.Log("Mouse button detected");
            }

            bool moveByGaze = !requireButtonForGaze || isTouchActive;

            if (moveByGaze)
            {
                Vector3 forward = camTransform.forward;
                forward.y = 0;
                forward.Normalize();
                
                moveDirection = forward * moveSpeed * Time.deltaTime;
                
                if (isTouchActive)
                {
                    Debug.Log("Moving by gaze: " + moveDirection);
                }
            }
        }

        transform.position += moveDirection;
    }
}