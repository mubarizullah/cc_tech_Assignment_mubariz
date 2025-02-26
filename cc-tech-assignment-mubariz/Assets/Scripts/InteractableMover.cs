using System;
using System.Collections;
using UnityEngine;

public class InteractableMover : MonoBehaviour
{
    [SerializeField] private Transform targetTransform; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float durationForObjMove = 1f;
    [SerializeField] GameInput gameInput;
    public static event Action OnObjectGathered;

    GameObject currentInteractable;
    bool isMoving;

    private void Start()
    {
        RayCaster.OnRayCastObject += RayCaster_OnRayCastObject;
        RayCaster.OnNotRayCastingObject += RayCaster_OnNotRayCastingObject;
    }

    private void RayCaster_OnNotRayCastingObject(object sender, RayCaster.OnRayCastObjectClass e)
    {
        if (!isMoving)
            currentInteractable = e.gb;
    }

    private void RayCaster_OnRayCastObject(object sender, RayCaster.OnRayCastObjectClass e)
    {
        if (!isMoving)
            currentInteractable = e.gb;
    }

    private void Update()
    {        
        if (currentInteractable != null && gameInput.InteractButtonPressed()) 
        {
            Debug.Log("Object found and button pressed");            
            StartCoroutine(MoveObjectToTarget(currentInteractable.transform, targetTransform));
        }
    }
    private IEnumerator MoveObjectToTarget(Transform objectToMove, Transform target)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = objectToMove.position;
        Quaternion startRotation = objectToMove.rotation;
        float duration = 1f;


        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            //movement and rotation through lerp
            objectToMove.position = Vector3.Lerp(startPosition, target.position, t);
            objectToMove.rotation = Quaternion.Slerp(startRotation, target.rotation, t);

            yield return null;
        }

        objectToMove.position = target.position;
        objectToMove.rotation = target.rotation;

        // Ensure event is triggered only once when movement completes
        if (currentInteractable != null)
        {
            OnObjectGathered?.Invoke();
            currentInteractable.SetActive(false);
            currentInteractable = null; // Prevent multiple interactions
        }
    }
}
