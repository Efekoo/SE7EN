using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float moveSpeed = 3f;


    public float moveDistance = 5f;


    public bool isVertical = false;


    Vector3 baslangicPozisyonu;


    void Start()
    {

        baslangicPozisyonu = transform.position;

    }


    void Update()
    {

        float pingPongDegeri = Mathf.PingPong(Time.time * moveSpeed, moveDistance);


        Vector3 yeniPozisyon = baslangicPozisyonu;


        if (isVertical)
        {

            yeniPozisyon.y = baslangicPozisyonu.y + pingPongDegeri;

        }
        else
        {

            yeniPozisyon.x = baslangicPozisyonu.x + pingPongDegeri;

        }


        transform.position = yeniPozisyon;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {

       
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
      
            collision.transform.SetParent(null);
        }

    }

}