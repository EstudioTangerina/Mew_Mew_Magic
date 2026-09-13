using UnityEngine;

public class IconSelectorController : MonoBehaviour
{
    // Um array de todos os scripts IconHoverEffect
    public IconHoverEffect[] iconHoverEffects;

    private int selectedIndex = 0;

    void Start()
    {
        // 1. Inicializa todos os ícones para o estado padrão
        foreach (IconHoverEffect effect in iconHoverEffects)
        {
            effect.Initialize();
        }

        // 2. Define o primeiro ícone como o selecionado
        if (iconHoverEffects.Length > 0)
        {
            SelectIcon(0);
        }
    }

    void Update()
    {
        // Detecta a entrada do teclado
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelection(1);
        }
    }

    private void ChangeSelection(int direction)
    {
        // Deseleciona o ícone atual
        if (selectedIndex >= 0 && selectedIndex < iconHoverEffects.Length)
        {
            iconHoverEffects[selectedIndex].Deselect();
        }

        // Calcula o novo índice
        selectedIndex += direction;
        if (selectedIndex < 0)
        {
            selectedIndex = iconHoverEffects.Length - 1;
        }
        else if (selectedIndex >= iconHoverEffects.Length)
        {
            selectedIndex = 0;
        }

        // Seleciona o novo ícone
        SelectIcon(selectedIndex);
    }

    private void SelectIcon(int index)
    {
        if (index >= 0 && index < iconHoverEffects.Length)
        {
            iconHoverEffects[index].Select();
        }
    }
}