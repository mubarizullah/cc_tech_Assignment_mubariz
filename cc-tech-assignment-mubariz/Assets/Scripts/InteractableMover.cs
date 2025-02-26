using System.Collections;
using UnityEngine;

public class InteractableMover : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float durationForObjMove = 1f;

    GameObject currentInteractable;

    private void Start()
    {
        RayCaster.OnRayCastObject += RayCaster_OnRayCastObject;
        RayCaster.OnNotRayCastingObject += RayCaster_OnNotRayCastingObject;
    }

    private void RayCaster_OnNotRayCastingObject(object sender, RayCaster.OnRayCastObjectClass e)
    {
        currentInteractable = e.gb;
    }

    private void RayCaster_OnRayCastObject(object sender, RayCaster.OnRayCastObjectClass e)
    {
        currentInteractable = e.gb;
    }

    private void Update()
    {        
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log("Object found and button pressed");
            StartCoroutine(MoveObjectToTarget(currentInteractable.transform, targetTransform));
        }
    }
    private IEnumerator MoveObjectToTarget(Transform objectToMove, Transform target)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = objectToMove.position;
        Quaternion startRotation = objectToMove.rotation;
        float duration = 1f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Smooth movement
            objectToMove.position = Vector3.Lerp(startPosition, target.position, t);
            objectToMove.rotation = Quaternion.Slerp(startRotation, target.rotation, t);

            yield return null;
        }

        // Ensure final position & rotation match exactly
        objectToMove.position = target.position;
        objectToMove.rotation = target.rotation;
    }
}
