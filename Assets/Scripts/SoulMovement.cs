using UnityEngine;
using UnityEngine.InputSystem; // Necessário para acessar o InputValue

[RequireComponent(typeof(Rigidbody2D))]
public class SoulMovement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float moveSpeed = 5f;

    private Vector2 movementInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Opcional: Trava a rotação Z para o coração não girar ao colidir
        rb.freezeRotation = true;
    }

    // O "Send Messages" do Player Input chama essa função automaticamente quando a Action "Move" é ativada
    void OnMove(InputValue value)
    {
        // Lê o vetor 2D do teclado (WASD/Setas) ou analógico do controle
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // Move a alma aplicando o vetor de input multiplicado pela velocidade
        rb.linearVelocity = movementInput * moveSpeed;
    }
}
