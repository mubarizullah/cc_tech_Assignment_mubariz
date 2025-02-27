using UnityEngine;
using System;
public class Interactable : MonoBehaviour
{
    public static event Action OnObjectGathered;
    [SerializeField] UIHandler UIHandler;

    private void OnDisable()
    {
       OnObjectGathered?.Invoke();
    }

}
