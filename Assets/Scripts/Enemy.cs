using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    [Header("Life")]
    public int health = 10;
    public GameObject deathPrefab;
    public Transform damageDigitMarker;

    private GameObject damageDigitPrefab;

    [Header("Drop")]
    public float dropChance;
    public List<GameObject> dropItems;
    public List<float> dropChances;

    void Awake()
    {
        damageDigitPrefab = Resources.Load<GameObject>("life/damage_digit");
    }

    public void Damage(int amount)
    {
        health -= amount;
        Debug.Log($"Inimigo recebeu dano de {amount}. A vida total é de {health}");
        GetComponent<SpriteRenderer>().color = Color.red;

        // Tween effect could be implemented with a coroutine or a tween library
        StartCoroutine(TweenColor(Color.white, 0.3f));

        GameObject damageDigit = Instantiate(damageDigitPrefab);
        damageDigit.GetComponent<DamageDigit>().value = amount;

        if (damageDigitMarker)
            damageDigit.transform.position = damageDigitMarker.position;
        else
            damageDigit.transform.position = transform.position;

        damageDigit.transform.SetParent(transform.parent);

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        if (deathPrefab)
        {
            GameObject deathObject = Instantiate(deathPrefab);
            deathObject.transform.position = transform.position;
            deathObject.transform.SetParent(transform.parent);
        }

        if (Random.value <= dropChance)
            DropItem();

        GameManager.monstersDefeatedCounter++;
        Destroy(gameObject);
    }

    private GameObject GetRandomDropItem()
    {
        if (dropItems.Count == 1)
            return dropItems[0];

        float maxChance = 0.0f;
        foreach (var chance in dropChances)
        {
            maxChance += chance;
        }

        float randomValue = Random.value * maxChance;
        float accumulatedChance = 0.0f;

        for (int i = 0; i < dropItems.Count; i++)
        {
            float dropChance = i < dropChances.Count ? dropChances[i] : 1;
            if (randomValue <= dropChance + accumulatedChance)
            {
                return dropItems[i];
            }
            accumulatedChance += dropChance;
        }

        return dropItems[0];
    }

    private void DropItem()
    {
        GameObject drop = Instantiate(GetRandomDropItem());
        drop.transform.position = transform.position;
        drop.transform.SetParent(transform.parent);
    }

    private IEnumerator TweenColor(Color targetColor, float duration)
    {
        Color originalColor = GetComponent<SpriteRenderer>().color;
        float time = 0;

        while (time < duration)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(originalColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().color = targetColor;
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
