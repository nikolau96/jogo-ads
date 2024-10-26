using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public MobSpawner mobSpawner;
    public float initialSpawnRate = 30.0f;
    public float mobsIncrease = 20.0f;
    public float waveDuration = 15.0f;
    public float breakIntensity = 0.33f;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver)
            return;

        time += Time.deltaTime;

        float spawnRate = initialSpawnRate + mobsIncrease * (time / 60.0f);
        float sinWave = Mathf.Sin((time * Mathf.PI * 2) / waveDuration);
        float waveFactor = Remap(sinWave, -1.0f, 1.0f, breakIntensity, 1.0f);
        spawnRate *= waveFactor;

        mobSpawner.mobsPerMinute = spawnRate;
    }

    private float Remap(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return fromTarget + (value - fromSource) * (toTarget - fromTarget) / (toSource - fromSource);
    }
}
