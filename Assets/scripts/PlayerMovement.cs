using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementIsometric : MonoBehaviour
{
    public float moveSpeed = 5f;

    private PlayerControls controls;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        // Blind de l'action Evasion
        controls.Player.Evade.performed += ContextMenu => Evasion();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();  
    }

    void Evasion()
    {
        Debug.Log("Evasion activée !");
        StartCoroutine(EvasionTemporaire());
    }

    IEnumerator EvasionTemporaire()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
        // Ajoute un effet visuel ou un champs
        yield return new WaitForSeconds(1.5f);
        col.isTrigger = false;
    }
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
        Debug.Log("Input actif ?" + controls.Player.Move.enabled);
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
