using UnityEngine;
using System;
public class Interactable : MonoBehaviour
{
    public static event Action OnObjectGathered;

    private void OnDisable()
    {
       OnObjectGathered?.Invoke();
    }

}
