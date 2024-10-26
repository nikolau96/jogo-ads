using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreature : MonoBehaviour
{

    [SerializeField] private GameObject[] creatures; // Array de prefab de criaturas
    [SerializeField] private float mobsPerMinute = 10.0f;
    [SerializeField] private PathFollow2D pathFollow2D; // Supondo que PathFollow2D seja um script que você criou

    private float cooldown = 0.0f;

    private void Update()
    {
        if (GameManager.isGameOver)
            return;

        cooldown -= Time.deltaTime;
        if (cooldown > 0)
            return;

        float interval = 60.0f / mobsPerMinute;
        cooldown = interval;

        Vector2 point = GetPoint();
        if (IsPointOccupied(point))
            return;

        int index = Random.Range(0, creatures.Length);
        GameObject creature = Instantiate(creatures[index], point, Quaternion.identity);
        creature.transform.SetParent(transform);
    }

    private Vector2 GetPoint()
    {
        float progressRatio = Random.value; // Gera um valor entre 0 e 1
        pathFollow2D.ProgressRatio = progressRatio; // Supondo que você tenha uma propriedade ProgressRatio no script PathFollow2D
        return pathFollow2D.GlobalPosition; // Supondo que você tenha uma propriedade GlobalPosition em PathFollow2D
    }

    private bool IsPointOccupied(Vector2 point)
    {
        // Implementar lógica de verificação de ponto ocupado
        // Por exemplo, usando Physics2D.OverlapPoint
        return Physics2D.OverlapPoint(point, LayerMask.GetMask("YourLayer")) != null;
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
