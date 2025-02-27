using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerHealth;
    public float Damage;
    public UIHandler ui_Handler;

    private void Start()
    {
        EnemyBehaviour.OnPlayerGetsDamage += EnemyBehaviour_OnPlayerGetsDamage;
    }

    private void EnemyBehaviour_OnPlayerGetsDamage()
    {
        ui_Handler.UpdateHealth(Damage);
    }
}
