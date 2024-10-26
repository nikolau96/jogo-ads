using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent gameOver;
    public Player player;
    public Vector2 playerPosition;
    private bool isGameOver = false;
    private float timeElapsed = 0.0f;
    private string timeElapsedString;
    private int meatCounter = 0;
    public static GameManager Instance { get; private set; }
    public static int monstersDefeatedCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        timeElapsed += delta;
        int timeElapsedInSeconds = Mathf.FloorToInt(timeElapsed);
        int seconds = timeElapsedInSeconds % 60;
        int minutes = timeElapsedInSeconds / 60;
        timeElapsedString = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    public void EndGame()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        gameOver.Invoke();
    }

    public void Reset()
    {
        player = null;
        playerPosition = Vector2.zero;
        isGameOver = false;
        timeElapsed = 0.0f;
        timeElapsedString = "00:00";
        meatCounter = 0;
        monstersDefeatedCounter = 0;
        // Note: Unity's UnityEvent handles connections, no need to disconnect manually.
    }
}
