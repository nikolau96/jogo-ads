using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;

    [Header("Sword")]
    public int swordDamage = 5;

    [Header("Life")]
    public int health = 100;
    public int maxHealth = 100;
    public GameObject deathPrefab;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D swordArea;
    private Collider2D hitboxArea;
    private UnityEngine.UI.Slider healthProgressBar;

    private Vector2 inputVector = Vector2.zero;
    private bool isRunning = false;
    private bool wasRunning = false;
    private bool isAttacking = false;
    private float attackCooldown = 0f;
    private float hitboxCooldown = 0f;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        swordArea = transform.Find("SwordArea").GetComponent<Collider2D>();
        hitboxArea = transform.Find("HitboxArea").GetComponent<Collider2D>();
        healthProgressBar = GameObject.Find("HealthProgressBar").GetComponent<UnityEngine.UI.Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player = this;
        GameManager.Instance.OnMeatCollected += (value) => { GameManager.Instance.meatCounter += 1; };
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.playerPosition = transform.position;
        ReadInput();
        UpdateAttackCooldown(Time.deltaTime);

        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }

        PlayRunIdleAnimation();

        if (!isAttacking)
        {
            RotateSprite();
        }

        UpdateHitboxDetection(Time.deltaTime);
        healthProgressBar.maxValue = maxHealth;
        healthProgressBar.value = health;
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = inputVector * speed * 100f;

        if (isAttacking)
        {
            targetVelocity *= 0.25f;
        }

        Vector2 velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, targetVelocity, 0.05f);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void UpdateAttackCooldown(float delta)
    {
        if (isAttacking)
        {
            attackCooldown -= delta;
            if (attackCooldown <= 0f)
            {
                isAttacking = false;
                isRunning = false;
                animator.Play("Idle");
            }
        }
    }

    void ReadInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        inputVector = new Vector2(moveHorizontal, moveVertical).normalized;

        float deadzone = 0.15f;
        if (Mathf.Abs(inputVector.x) < deadzone) inputVector.x = 0f;
        if (Mathf.Abs(inputVector.y) < deadzone) inputVector.y = 0f;

        wasRunning = isRunning;
        isRunning = inputVector != Vector2.zero;
    }

    void PlayRunIdleAnimation()
    {
        if (!isAttacking)
        {
            if (wasRunning != isRunning)
            {
                animator.Play(isRunning ? "Run" : "Idle");
            }
        }
    }

    void RotateSprite()
    {
        spriteRenderer.flipX = inputVector.x < 0;
    }

    void Attack()
    {
        if (isAttacking) return;

        animator.Play("AttackSide1");
        attackCooldown = 0.6f;
        isAttacking = true;
        DealDamageToEnemies();
    }

    void DealDamageToEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordArea.transform.position, swordArea.bounds.extents.x);
        foreach (var body in hitEnemies)
        {
            if (body.CompareTag("Enemy"))
            {
                Enemy enemy = body.GetComponent<Enemy>();
                Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;
                Vector2 attackDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

                if (Vector2.Dot(directionToEnemy, attackDirection) >= 0.3f)
                {
                    enemy.Damage(swordDamage);
                }
            }
        }
    }

    void UpdateHitboxDetection(float delta)
    {
        hitboxCooldown -= delta;
        if (hitboxCooldown > 0) return;

        hitboxCooldown = 0.5f;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitboxArea.transform.position, hitboxArea.bounds.extents.x);
        foreach (var body in hitEnemies)
        {
            if (body.CompareTag("Enemy"))
            {
                Enemy enemy = body.GetComponent<Enemy>();
                int damageAmount = 1;
                Damage(damageAmount);
            }
        }
    }

    public void Damage(int amount)
    {
        if (health <= 0) return;

        health -= amount;
        Debug.Log($"Player took damage of {amount}. Current health is {health}/{maxHealth}");
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ResetColor());

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Die()
    {
        GameManager.Instance.EndGame();
        if (deathPrefab)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log("Player died!");
        Destroy(gameObject);
    }

    public int Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log($"Player healed for {amount}. Current health is {health}/{maxHealth}");
        return health;
    }

}
