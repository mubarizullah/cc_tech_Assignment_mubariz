using System;
using UnityEngine;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image interactImage;

    private void OnEnable()
    {
        RayCaster.OnShowInteractUI += HandleInteractUI;
    }

    private void OnDisable()
    {
        RayCaster.OnShowInteractUI -= HandleInteractUI;
    }

    private void HandleInteractUI(bool isActive)
    {
        if (interactImage != null)
        {
            interactImage.gameObject.SetActive(isActive);
        }
    }
}
