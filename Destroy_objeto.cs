using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_objeto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public GameObject caja1;
    public GameObject caja2;
    public GameObject caja3;

    public int cantidad = 0;
    public int vida = 100;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == caja1/*Objeto 1 a colisionar*/)
        {
            Destroy(caja1);
            cantidad += 1;
        }
        if (collision.gameObject == caja2/*Objeto 1 a colisionar*/)
        {
            Destroy(caja2);
            cantidad += 1;
        }
        //mona
        if (collision.gameObject == caja3/*Objeto 1 a colisionar*/)
        {
            //Destroy(caja3);
            vida -= 20;
        }
    }


    // Update is called once per frame
    void Update()
    {
    {
        
    }




}
}
