using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para usar TextMeshPro, se você estiver utilizando

public class GameOverUI : Canvas
{
    [SerializeField] private TextMeshProUGUI timeLabel; // Usar TextMeshProUGUI para o label do tempo
    [SerializeField] private TextMeshProUGUI monstersLabel; // Usar TextMeshProUGUI para o label dos monstros
    [SerializeField] private float restartDelay = 5.0f; // Tempo de espera para reiniciar
    private float restartCooldown;

    // Start is called before the first frame update
    void Start()
    {
        timeLabel.text = GameManager.TimeElapsedString; // Atualiza o texto do label do tempo
        monstersLabel.text = GameManager.MonstersDefeatedCounter.ToString(); // Atualiza o texto do label dos monstros
        restartCooldown = restartDelay; // Inicia o cooldown de reinício
    }

    // Update is called once per frame
    void Update()
    {
        restartCooldown -= Time.deltaTime; // Diminui o cooldown
        if (restartCooldown <= 0.0f)
        {
            RestartGame(); // Reinicia o jogo se o cooldown expirar
        }
    }

    private void RestartGame()
    {
        GameManager.Reset(); // Reseta o game manager
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // Reinicia a cena atual
    }
}
