using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main?.transform;
    }

    private void Update()
    {
        // Hacer que el Canvas mire hacia la cámara
        transform.LookAt(cameraTransform);
        // Corregir la orientación para que no esté "al revés"
        transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
    }
}