using UnityEngine;

public class ShieldNotifier : MonoBehaviour
{
    public delegate void ShieldHandler();
    public event ShieldHandler OnShieldPointerEnter;
    public event ShieldHandler OnShieldPointerStay; 
    public CameraPointer cameraPointer;
    private bool isPointed = false;
    private Renderer originalRenderer;
    private Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraPointer.OnRaycastEnter += HandleRaycastEnter;
        cameraPointer.OnRaycastExit += HandleRaycastExit;
        originalRenderer = GetComponentInChildren<Renderer>();
        originalColor = new Color(originalRenderer.material.color.r, originalRenderer.material.color.g, originalRenderer.material.color.b, originalRenderer.material.color.a); 
    }

    // Update is called once per frame
    void Update()
    {
       if (isPointed)
       {
            OnShieldPointerStay();
       } 
    }

    void HandleRaycastEnter(Ray ray, Collider hitObject)
    {
        if (hitObject.gameObject != gameObject)
            return;
        Debug.Log("Raycast entered on ShieldNotifier");
        OnShieldPointerEnter();
        GetComponentInChildren<Renderer>().material.color = Color.blue;
        isPointed = true;
    }

    void HandleRaycastExit(Ray ray, Collider hitObject)
    {
        if (hitObject.gameObject != gameObject)
            return;
        Debug.Log("Raycast exited on ShieldNotifier");
        GetComponentInChildren<Renderer>().material.color = originalColor;
        isPointed = false;
    }
}
