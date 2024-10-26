using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour
{
    [SerializeField] private Canvas gameUI; // Referência ao Canvas do UI do jogo
    [SerializeField] private GameObject gameOverUITemplate; // Prefab do UI de game over

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GameOver += TriggerGameOver; // Assinatura do evento de game over
    }

    private void TriggerGameOver()
    {
        if (gameUI != null)
        {
            Destroy(gameUI.gameObject); // Remove o Canvas do jogo
            gameUI = null;
        }

        GameObject gameOverUI = Instantiate(gameOverUITemplate); // Instancia o UI de game over
        gameOverUI.transform.SetParent(transform); // Define o pai para manter a hierarquia
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
