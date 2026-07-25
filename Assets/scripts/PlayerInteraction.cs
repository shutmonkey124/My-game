using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1.5f;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(
            transform.position,
            interactionRadius
        );

        foreach (Collider2D nearbyCollider in nearbyColliders)
        {
            Interactable interactable =
                nearbyCollider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}