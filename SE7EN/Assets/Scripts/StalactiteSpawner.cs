using UnityEngine;
using UnityEngine.SceneManagement;

public class StalactiteSpawner : MonoBehaviour
{
    public GameObject stalactitePrefab;
    public float spawnRate = 2f; // Saniyede kaç tane düşeceği
    public float spawnAreaWidth = 10f;
    public float spawnHeightOffset = 10f; // Ekranın ne kadar üstünden başlayacağı

    private float nextSpawnTime;

    void Update()
    {
        // Sadece "Level2" sahnesinde çalışsın
        if (SceneManager.GetActiveScene().name != "Level2")
        {
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnStalactite();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnStalactite()
    {
        if (stalactitePrefab == null)
        {
            Debug.LogError("Stalactite Prefab'ı atanmamış!");
            return;
        }

        // Kameranın görüş alanının üstünde bir yer belirle
        float spawnX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector3 spawnPosition = new Vector3(spawnX, Camera.main.transform.position.y + spawnHeightOffset, 0);

        Instantiate(stalactitePrefab, spawnPosition, Quaternion.identity);
    }
}