using System.Collections;
using UnityEngine;

public class BattleBoxScript : MonoBehaviour
{
    public GameObject RAW_Camera;
    public Animator battle_Box;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    { 
            StartCoroutine(OpenBox());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator OpenBox()
    {
        while (battle_Box.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        RAW_Camera.SetActive(true);
    }
}
