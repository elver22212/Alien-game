using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nave : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject balaRafaga;
    [SerializeField] GameObject disparador;
    [SerializeField] float fire;


    float minX, maxX, minY, maxY;
    float nextFire = 0;
    float nextRafaga = 0;
    bool cambiarBala = true;

    public bool gamePaused = false;
    public bool lento = false;


    // Start is called before the first frame update
    void Start()
    {

        Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 puntoMinParaY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.7f));

        maxX = esquinaSupDer.x - 0.7f;
        maxY = esquinaSupDer.y - 0.7f;
        minX = puntoMinParaY.x + 0.7f;
        minY = puntoMinParaY.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            MoverNave();
            if (cambiarBala)
                Disparar();
            else
                DispararRafaga();


            if (Input.GetKeyDown(KeyCode.Z))
                cambiarBala = cambiarBala ? false : true;
        }


    }

    void DispararRafaga()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextRafaga)
        {
            Instantiate(balaRafaga, disparador.transform.position, transform.rotation);
            nextRafaga = Time.time + (fire / 3);
        }

    }

    void Disparar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            Instantiate(bala, disparador.transform.position, transform.rotation);
            nextFire = Time.time + fire;
        }
    }

    void MoverNave()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed);

    
        transform.Translate(movimiento);

    
        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(maxX, transform.position.y);

        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }

        if (transform.position.y > maxY)
        {    
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

}