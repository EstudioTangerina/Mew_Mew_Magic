using UnityEngine;

public class MecanicaEncher : MonoBehaviour
{
    [Header("Configurações de Enchimento")]
    [Tooltip("O quanto a água sobe a cada gota recebida")]
    public float taxaPorGota = 0.2f; 
    
    [Tooltip("A altura máxima (escala Y) que o líquido pode alcançar na caixa")]
    public float limiteMaximoY = 5.0f;

    [Header("Comunicação")]
    [Tooltip("Arraste o NPC (que tem o GeradorDeGotas) para cá")]
    public GeradorDeGotas npcGerador;

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Gota"))
        {
            Destroy(colisao.gameObject);
            SubirNivelDoLiquido();
        }
    }

    private void SubirNivelDoLiquido()
    {
        Vector3 escalaAtual = transform.localScale;

        // Só tenta crescer se ainda não chegou no limite
        if (escalaAtual.y < limiteMaximoY)
        {
            escalaAtual.y += taxaPorGota;
            escalaAtual.y = Mathf.Min(escalaAtual.y, limiteMaximoY); // Trava no limite exato
            
            transform.localScale = escalaAtual;

            // NOVA PARTE: Verifica se acabou de encher com essa gota
            if (escalaAtual.y >= limiteMaximoY)
            {
                // Se sim, manda o NPC parar de criar gotas
                if (npcGerador != null)
                {
                    npcGerador.PararGotas();
                }
            }
        }
    }
}