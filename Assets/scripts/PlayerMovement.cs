using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementIsometric : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Pas besoin de gravité pour un jeu 2D isométrique
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Envoie les directions au blend tree
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);

        // Détection continue du déplacement
        bool isWalking = movement.sqrMagnitude > 0.01f; // sqrMagnitude évite les erreurs de flottants
        animator.SetBool("IsWalking", isWalking);

        // Debug
        Debug.Log("Input reçu : " + movement + " | IsWalking : " + isWalking);
    }

    void FixedUpdate()
    {
        // Applique le mouvement
        rb.linearVelocity = movement.normalized * moveSpeed;

        Debug.Log("Vitesse appliquée : " + rb.linearVelocity);
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("OnMovement appelé, valeur : " + movement);
    }
}
