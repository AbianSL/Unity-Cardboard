using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CameraPointer cameraPointer;
    public ShieldNotifier shieldNotifier;
    public float moveSpeed = 2f;
    private bool isCollected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraPointer.OnRaycastEnter += HandleRaycastEnter;
        cameraPointer.OnRaycastExit += HandleRaycastExit;
        shieldNotifier.OnShieldPointerEnter += MoveTowardsCamera;
        shieldNotifier.OnShieldPointerStay += MoveTowardsCamera;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void HandleRaycastEnter(Ray ray, Collider hitObject)
    {
        if (hitObject.gameObject != gameObject || isCollected)
        {
            return;
        }
        Debug.Log("Raycast entered on " + gameObject.name);
        GetComponentInChildren<Renderer>().material.color = Color.red;
        isCollected = true;
    }

    void HandleRaycastExit(Ray ray, Collider hitObject)
    {
    }

    void MoveTowardsCamera()
    {
        if (isCollected)
        {
            if (Vector3.Distance(transform.position, cameraPointer.gameObject.transform.position) < 0.5f)
            {
                gameObject.SetActive(false);
                return;
            }
            Vector3 directionToCamera = cameraPointer.gameObject.transform.position - transform.position;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, cameraPointer.gameObject.transform.position, step);
            transform.LookAt(cameraPointer.gameObject.transform); 
        }
    }
}
