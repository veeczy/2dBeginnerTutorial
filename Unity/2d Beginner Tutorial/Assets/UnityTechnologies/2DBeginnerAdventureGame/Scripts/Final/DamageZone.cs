using UnityEngine;

/// <summary>
/// This class will apply continuous damage to the Player as long as it stay inside the trigger on the same object
/// </summary>
public class DamageZone : MonoBehaviour
{
    public int amount = -1;
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Object that entered the trigger: " + other);
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(amount);
        }
    }
}
