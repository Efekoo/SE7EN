using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public GameObject platformPrefab;


    public int numberOfPlatforms = 100;


    public float levelWidth = 2.5f;


    public float minY = 0.5f;
    public float maxY = 1.5f;


    void Start()
    {

        Vector3 spawnPosition = new Vector3();


        spawnPosition.x = 0;
        spawnPosition.y = 0;
        spawnPosition.z = 0;


        for (int i = 0; i < numberOfPlatforms; i = i + 1)
        {

            float rastgeleYukseklik = Random.Range(minY, maxY);

            spawnPosition.y = spawnPosition.y + rastgeleYukseklik;


            float rastgeleX = Random.Range(-levelWidth, levelWidth);

            spawnPosition.x = rastgeleX;


            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        }

    }

}