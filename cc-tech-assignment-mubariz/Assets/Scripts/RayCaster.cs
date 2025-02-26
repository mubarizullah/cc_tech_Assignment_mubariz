using System;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public static event Action<bool> OnShowInteractUI;

    [Header("Raycast Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;

    private void Update()
    {
        HandleInteractionRaycast();
    }

    private void HandleInteractionRaycast()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, cameraTransform.forward, Color.green,10000);
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            OnShowInteractUI?.Invoke(true);
            Debug.DrawLine(ray.origin, hit.point, Color.green, 0.1f);
            Debug.Log("interactable found");
        }
        else
        {
            OnShowInteractUI?.Invoke(false);
        }
    }
}
