using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public GameObject normalPlatform;
    public GameObject movingPlatform;
    public GameObject collapsePlatform;


    public GameObject soulPrefab; 


    public int numberOfPlatforms = 100;


    public float levelWidth = 2.5f;


    public float minY = 0.5f;
    public float maxY = 1.5f;


    public int soulChance = 50;
    public float soulHeight = 1.0f; 


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


           
            GameObject yeniPlatform = null;
            int sans = Random.Range(0, 100);


            if (sans < 10)
            {
                yeniPlatform = Instantiate(movingPlatform, spawnPosition, Quaternion.identity);
            }
            else if (sans < 20)
            {
                yeniPlatform = Instantiate(collapsePlatform, spawnPosition, Quaternion.identity);
            }
            else
            {
                yeniPlatform = Instantiate(normalPlatform, spawnPosition, Quaternion.identity);
            }


      
            int soulSans = Random.Range(0, 100);

            if (soulSans < soulChance)
            {

                Vector3 soulPozisyonu = spawnPosition;

                soulPozisyonu.y = soulPozisyonu.y + soulHeight;


                GameObject yeniSoul = Instantiate(soulPrefab, soulPozisyonu, Quaternion.identity);


              
                yeniSoul.transform.SetParent(yeniPlatform.transform);

            }

        }

    }

}