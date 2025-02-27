
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image interactImage;
    [SerializeField] TextMeshProUGUI itemCountText;
    [SerializeField] Image healthImage;
    public float playerHealth;

    private int itemCount;
    private void Start()
    {
        Interactable.OnObjectGathered += Interactable_OnObjectGathered;
        playerHealth = 80f;
        UpdateHealthUI();
        itemCount = 0;        
    }

    private void Interactable_OnObjectGathered()
    {
        itemCount++;
        UpdateItemCount_UI();
    }

    public void UpdateHealth(float damage)
    {
        playerHealth = Mathf.Clamp(playerHealth-damage, 0, 100); // Ensuring health stays between 0 and 100
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthImage != null)
        {
            healthImage.fillAmount = playerHealth / 100f; // Normalize to 0 - 1 range
        }
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

    public void Exit()
    {
        Application.Quit();
    }


}
