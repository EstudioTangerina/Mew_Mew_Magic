using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconHoverEffect : MonoBehaviour
{
    // O componente Image do próprio ícone
    public Image iconImage;
    // O componente TextMeshPro que está desabilitado
    public TextMeshProUGUI iconText;
    // As cores para o ícone
    public Color defaultColor = Color.gray;
    public Color hoverColor = Color.white;

    // Método para inicializar o estado do ícone
    public void Initialize()
    {
        // Garante que o texto comece desativado e o ícone com a cor padrão
        if (iconText != null)
        {
            iconText.gameObject.SetActive(false);
        }
        if (iconImage != null)
        {
            iconImage.color = defaultColor;
        }
    }

    public void Select()
    {
        if (iconImage != null)
        {
            iconImage.color = hoverColor;
        }
        if (iconText != null)
        {
            iconText.gameObject.SetActive(true);
        }
    }

    public void Deselect()
    {
        if (iconImage != null)
        {
            iconImage.color = defaultColor;
        }
        if (iconText != null)
        {
            iconText.gameObject.SetActive(false);
        }
    }
}