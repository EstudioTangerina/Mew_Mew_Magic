using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PinkActions : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Configurações do Pulo")]
    public Transform alvo;
    [Tooltip("Duração total do pulo em segundos")]
    public float tempoDeVoo = 1.0f;
    [Tooltip("O quão alto o arco do pulo vai subir antes de cair")]
    public float alturaDoArco = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (alvo != null)
        {
            StartCoroutine(RotinaDePulo());
        }
    }

    private IEnumerator RotinaDePulo()
    {
        Vector2 posInicial = transform.position;
        Vector2 posFinal = alvo.position;
        float tempoDecorrido = 0f;

        // O loop roda enquanto o tempo que passou for menor que o tempo de voo definido
        while (tempoDecorrido < tempoDeVoo)
        {
            tempoDecorrido += Time.deltaTime;
            float progresso = tempoDecorrido / tempoDeVoo; // Vai de 0 a 1

            // Calcula a posição em linha reta do ponto A ao ponto B
            Vector2 posAtual = Vector2.Lerp(posInicial, posFinal, progresso);

            // Calcula a curva (arco) do pulo usando a função Seno da matemática
            float arcoY = Mathf.Sin(progresso * Mathf.PI) * alturaDoArco;

            // Adiciona a curva vertical à posição
            posAtual.y += arcoY;

            // Move o NPC
            rb.MovePosition(posAtual);

            // Espera até o próximo frame para continuar
            yield return null; 
        }

        // Garante que o NPC vai cravar exatamente na posição final quando o tempo acabar
        rb.MovePosition(posFinal);
    }
}
