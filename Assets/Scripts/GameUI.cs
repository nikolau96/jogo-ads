using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro; // Para usar TextMeshPro, se você estiver utilizando

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        [SerializeField] TextMeshProUGUI timerLabel; // Label para o timer
        [SerializeField] TextMeshProUGUI meatLabel; // Label para a carne
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null)
        {
            meatLabel.text = GameManager.MeatCounter.ToString(); // Atualiza o texto do label de carne
            timerLabel.text = GameManager.TimeElapsedString; // Atualiza o texto do label do tempo
        }
    }
}
