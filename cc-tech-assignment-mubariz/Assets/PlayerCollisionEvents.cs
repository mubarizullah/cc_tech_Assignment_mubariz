using UnityEngine;

public class PlayerCollisionEvents : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Player hit enemy");
        }
    }
}
