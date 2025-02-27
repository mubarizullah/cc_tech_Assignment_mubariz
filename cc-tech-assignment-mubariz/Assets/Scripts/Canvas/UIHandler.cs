
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image interactImage;
    [SerializeField] TextMeshProUGUI itemCountText;
    [SerializeField] Image healthImage;
    float playerHealth;

    private int itemCount;
    private void Start()
    {
        EnemyBehaviour.OnPlayerGetsDamage += EnemyBehaviour_OnPlayerGetsDamage;
        playerHealth = 80f;
        UpdateHealthUI();
        itemCount = 0;
        InteractableMover.OnObjectGathered += InteractableMover_OnObjectGathered;
    }

    private void EnemyBehaviour_OnPlayerGetsDamage()
    {
        UpdateHealth(5f);
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
