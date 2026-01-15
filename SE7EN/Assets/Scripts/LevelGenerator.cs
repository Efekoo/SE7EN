using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] normalPlatforms;
    public GameObject[] movingPlatforms;
    public GameObject[] collapsePlatforms;


    public GameObject soulPrefab; 
    public GameObject heartPrefab;


    public int numberOfPlatforms = 100;


    public float levelWidth = 2.5f;


    public float minY = 0.5f;
    public float maxY = 1.5f;


    public int soulChance = 50;
    public float soulHeight = 1.0f; 
    public int heartChance = 15;
    public float heartHeight = 1.0f;
    public float heartXOffsetRange = 0.6f;


    void Start()
    {

        if (normalPlatforms == null || normalPlatforms.Length == 0)
        {
            return;
        }

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


           
            GameObject yeniPlatform = null;
            int sans = Random.Range(0, 100);


            if (sans < 10 && movingPlatforms != null && movingPlatforms.Length > 0)
            {
                GameObject movingPlatform = movingPlatforms[Random.Range(0, movingPlatforms.Length)];
                if (movingPlatform != null)
                {
                    yeniPlatform = Instantiate(movingPlatform, spawnPosition, Quaternion.identity);
                }
            }
            else if (sans < 20 && collapsePlatforms != null && collapsePlatforms.Length > 0)
            {
                GameObject collapsePlatform = collapsePlatforms[Random.Range(0, collapsePlatforms.Length)];
                if (collapsePlatform != null)
                {
                    yeniPlatform = Instantiate(collapsePlatform, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                int index = Random.Range(0, normalPlatforms.Length);
                GameObject normalPlatform = normalPlatforms[index];
                if (normalPlatform != null)
                {
                    yeniPlatform = Instantiate(normalPlatform, spawnPosition, Quaternion.identity);
                }
            }

            if (yeniPlatform == null)
            {
                continue;
            }


      
            int soulSans = Random.Range(0, 100);

            bool soulSpawned = false;
            if (soulPrefab != null && soulSans < soulChance)
            {

                Vector3 soulPozisyonu = spawnPosition;

                soulPozisyonu.y = soulPozisyonu.y + soulHeight;


                GameObject yeniSoul = Instantiate(soulPrefab, soulPozisyonu, Quaternion.identity);


              
                yeniSoul.transform.SetParent(yeniPlatform.transform);
                soulSpawned = true;

            }

            int heartSans = Random.Range(0, 100);

            if (!soulSpawned && heartPrefab != null && heartSans < heartChance)
            {
                Vector3 heartPozisyonu = spawnPosition;
                heartPozisyonu.x = heartPozisyonu.x + Random.Range(-heartXOffsetRange, heartXOffsetRange);
                heartPozisyonu.y = heartPozisyonu.y + heartHeight;
                heartPozisyonu.z = -0.01f;

                GameObject yeniHeart = Instantiate(heartPrefab, heartPozisyonu, Quaternion.identity);
                yeniHeart.transform.SetParent(yeniPlatform.transform);
            }

        }

    }

}
