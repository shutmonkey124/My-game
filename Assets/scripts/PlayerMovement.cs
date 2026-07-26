using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool dialogueIsOpen =
            DialogueUI.Instance != null &&
            DialogueUI.Instance.IsOpen;

        bool notebookIsOpen =
            NotebookUI.Instance != null &&
            NotebookUI.Instance.IsOpen;

        if (dialogueIsOpen || notebookIsOpen)
        {
            movement = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position +
            movement * speed * Time.fixedDeltaTime
        );
    }
}