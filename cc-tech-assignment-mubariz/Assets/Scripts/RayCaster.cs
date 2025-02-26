using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCaster : MonoBehaviour
{
    public static event Action<bool> OnShowInteractUI;

    [Header("Raycast Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;

    public class OnRayCastObjectClass : EventArgs
    {
        public GameObject gb;
    }

    public static event EventHandler<OnRayCastObjectClass> OnRayCastObject;
    public static event EventHandler<OnRayCastObjectClass> OnNotRayCastingObject;

    private void Update()
    {
        HandleInteractionRaycast();
    }

    private void HandleInteractionRaycast()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        // Draw the ray in the Scene view
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            OnShowInteractUI?.Invoke(true);
            OnRayCastObject?.Invoke(this,new OnRayCastObjectClass { gb = hit.transform.gameObject });
        }
        else
        {
            OnShowInteractUI?.Invoke(false);
            OnRayCastObject?.Invoke(this, new OnRayCastObjectClass { gb = null });

        }
    }

}
