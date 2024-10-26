using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDigit : MonoBehaviour
{
    public int value = 0; // O valor do dano
    private Text damageText; // Refer�ncia ao componente de texto
    // Start is called before the first frame update
    void Start()
    {
        // Obt�m a refer�ncia ao componente Text
        damageText = GetComponent<Text>();

        // Define o texto para mostrar o valor do dano
        if (damageText != null)
        {
            damageText.text = value.ToString();
        }

        // Destroi o objeto ap�s 1 segundo
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
