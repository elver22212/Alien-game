using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool moveRigh;
    [SerializeField] GameManager gm;
    [SerializeField] int Life;

    public bool lento = false;

    float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxX = esquinaInfDer.x;
        minX = esquinaInfIzq.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRigh)
        {
            Vector2 movimiento = new Vector2(speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        else
        {
            Vector2 movimiento = new Vector2(-speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        

        if(transform.position.x >= maxX)
        {
            moveRigh = false;
        }
        else if(transform.position.x <= minX)
        {
            moveRigh = true;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Disparo") )
        {

               Daño();
                if (gm.lento == true)
                {
                    Life = 1;
                    Destroy(this.gameObject);
                    gm.ReducirNumEnemigos();
                }
                else if (Life < 1)
                    {
                     Destroy(this.gameObject);
                     gm.ReducirNumEnemigos();
                    }
                
        }
        else if(collision.gameObject.CompareTag("DisparoRafaga"))
        {
            Daño();
            if(gm.lento == true)
            {
                Life = 1;
                Destroy(this.gameObject);
                gm.ReducirNumEnemigos();
            }
            else if (Life < 1)
            {
                Destroy(this.gameObject);
                gm.ReducirNumEnemigos();
            }
        }
    }
    void Daño()

    {
        Life = Life - 1;
    }

}