using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed = 1f;

    private Enemy enemy;
    private AnimatedSprite sprite;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        sprite = enemy.GetComponentInChildren<AnimatedSprite>();
    }

    void FixedUpdate()
    {
        if (GameManager.isGameOver)
            return;

        Vector3 playerPosition = GameManager.playerPosition;
        Vector3 difference = playerPosition - enemy.transform.position;
        Vector3 inputVector = difference.normalized;

        enemy.velocity = inputVector * speed * 100.0f;
        enemy.MoveAndSlide();

        if (inputVector.x > 0)
            sprite.flipH = false;
        else if (inputVector.x < 0)
            sprite.flipH = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
