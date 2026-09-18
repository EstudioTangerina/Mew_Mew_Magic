using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PinkJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Animator battle_Box;

    [Header("Configurações do Pulo")]
    public Transform alvo;
    [Tooltip("Duração total do pulo em segundos")]
    public float tempoDeVoo = 1.0f;
    [Tooltip("O quão alto o arco do pulo vai subir antes de cair")]
    public float alturaDoArco = 2.0f;

    [Tooltip("Array de Audio para SoundEffects")]
    public AudioClip[] soundEffects;

    GeradorDeGotas gotas;

    public GameObject liquido_Roxo;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        gotas = GetComponent<GeradorDeGotas>();
        
        if (alvo != null)
        {
            StartCoroutine(RotinaDePulo());
        }
    }

    private IEnumerator RotinaDePulo()
    {
        // 1. ESPERAR A ANIMAÇÃO DO BATTLE BOX ACABAR
        // Enquanto o tempo da animação for menor que 1 (100%), a corotina pausa e espera o próximo frame
        while (battle_Box.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; 
        }

        // 2. CONFIGURAR O PULO (A partir daqui a animação do battle_box já acabou)
        Vector2 posInicial = transform.position;
        Vector2 posFinal = alvo.position;
        float tempoDecorrido = 0f;

        // 3. INICIAR ANIMAÇÃO DO NPC (Apenas uma vez, antes do loop de movimento começar)
        anim.Play("Spin Jump");

        // 4. EXECUTAR O MOVIMENTO
        while (tempoDecorrido < tempoDeVoo)
        {
            tempoDecorrido += Time.deltaTime;
            float progresso = tempoDecorrido / tempoDeVoo; // Vai de 0 a 1

            // Calcula a posição
            Vector2 posAtual = Vector2.Lerp(posInicial, posFinal, progresso);
            float arcoY = Mathf.Sin(progresso * Mathf.PI) * alturaDoArco;
            posAtual.y += arcoY;

            // Move o NPC
            rb.MovePosition(posAtual);

            // Espera até o próximo frame para continuar o movimento
            yield return null; 
        }
        
        // 5. FINALIZAR O PULO
        // Garante que o NPC vai cravar exatamente na posição final
        rb.MovePosition(posFinal);
        
        // Retorna para a animação padrão
        //anim.Play("Idle");
        StartCoroutine(Fushia());

    }

    private IEnumerator Fushia()
    {
        anim.Play("Laugh w/ Tea");
        gameObject.GetComponent<AudioSource>().enabled = true;
        gameObject.GetComponent<AudioSource>().PlayOneShot(soundEffects[0]);
        
        yield return new WaitForSeconds(5);
        gameObject.transform.DOLocalMoveX(1.32f, 1f).SetEase(Ease.OutCubic);
        gameObject.GetComponent<AudioSource>().PlayOneShot(soundEffects[2]);

        yield return new WaitForSeconds(3);
        gotas.IniciarGotas();
        
        yield return new WaitForSeconds(4);
        if(gotas.estaDerramando == false)
        {
            liquido_Roxo.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
             yield return new WaitForSeconds(3);
            anim.Play("Shocked");
            gameObject.transform.DOShakeScale(1f, 0.5f);
            gameObject.GetComponent<AudioSource>().PlayOneShot(soundEffects[1]);
            yield return null;
        }
       /* */
    }
}