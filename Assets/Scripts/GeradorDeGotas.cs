using System.Collections;
using UnityEngine;

public class GeradorDeGotas : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject gotaPrefab;
    public Transform pontoDeSpawn;
    
    [Tooltip("Tempo em segundos entre uma gota e outra")]
    public float intervaloEntreGotas = 0.1f; 

    [Header("Física (O Arco da Gota)")]
    [Tooltip("Força empurrando a gota. Ex: X negativo empurra para a esquerda, Y positivo joga para cima")]
    public Vector2 forcaInicial = new Vector2(-2f, 1f); 
    
    // Variável de controle
    public bool estaDerramando = false;

    public GameObject Box;

    // Função que você pode chamar por botão, código ou Evento de Animação
    public void IniciarGotas()
    {
        if (!estaDerramando)
        {
            estaDerramando = true;
            StartCoroutine(RotinaCriarGotas());
            Box.SetActive(true);
        }
    }

    public void PararGotas()
    {
        estaDerramando = false;
    }

    private IEnumerator RotinaCriarGotas()
    {
        while (estaDerramando)
        {
            // 1. Cria a gota na posição do PontoDeSpawn
            GameObject novaGota = Instantiate(gotaPrefab, pontoDeSpawn.position, Quaternion.identity);

            // 2. Pega a física da gota para arremessá-la
            Rigidbody2D rbGota = novaGota.GetComponent<Rigidbody2D>();
            if (rbGota != null)
            {
                // Adiciona uma minúscula variação aleatória no X para a água não parecer "robótica" demais
                float variacaoX = Random.Range(-0.3f, 0.3f);
                Vector2 forcaFinal = new Vector2(forcaInicial.x + variacaoX, forcaInicial.y);
                
                // Aplica o "empurrão" na gota
                rbGota.AddForce(forcaFinal, ForceMode2D.Impulse);
            }

            // 3. Espera o tempo definido antes de criar a próxima gota do loop
            yield return new WaitForSeconds(intervaloEntreGotas);
        }
    }
}