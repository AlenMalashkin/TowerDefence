using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake() => 
        mainCamera = Camera.main;

    private void Update()
    {
        Quaternion rotation = mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
        
}
