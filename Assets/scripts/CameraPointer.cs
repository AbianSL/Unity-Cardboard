using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    public float range = 5f;
    public delegate void RaycastHandler(Ray ray, Collider hitObject);
    public event RaycastHandler OnRaycastEnter;
    public event RaycastHandler OnRaycastExit;

    private Vector3 direction;
    private Ray rayDirection;
    private Collider lastHitCollider = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        rayDirection = new Ray(transform.position, transform.TransformDirection(direction) * range);
        Debug.DrawRay(transform.position, transform.TransformDirection(direction) * range, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(rayDirection, out hit, range))
        {
            GameObject hitObject = hit.collider.transform.root.gameObject;

            if (hitObject != lastHitCollider?.transform.root.gameObject)
            {
                if (lastHitCollider != null)
                {
                    OnRaycastExit?.Invoke(rayDirection, lastHitCollider);
                }

                OnRaycastEnter?.Invoke(rayDirection, hit.collider);
                lastHitCollider = hit.collider;
            }
        }
        else
        {
            if (lastHitCollider != null)
            {
                OnRaycastExit?.Invoke(rayDirection, lastHitCollider);
                lastHitCollider = null;
            }
        }
    }
}
