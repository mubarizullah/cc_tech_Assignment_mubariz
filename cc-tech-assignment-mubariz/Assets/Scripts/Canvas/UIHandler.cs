
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image interactImage;
    [SerializeField] TextMeshProUGUI itemCountText;

    private int itemCount;
    private void Start()
    {
        itemCount = 0;
        InteractableMover.OnObjectGathered += InteractableMover_OnObjectGathered;
    }

    private void InteractableMover_OnObjectGathered()
    {
        itemCount++;
        UpdateItemCount_UI();
    }
    private void UpdateItemCount_UI()
    {
        itemCountText.text = itemCount.ToString();
    }
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
