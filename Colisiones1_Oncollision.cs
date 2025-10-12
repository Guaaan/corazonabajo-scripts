using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnCollisionEnter()
    {
        print("Objecto en contacto!");

    }
    public void OnCollisionStay()
    {
        print("Mantiene contacto!");
    }
    public void OnCollisionExit()
    {
        print("salio del contacto!");
    }

    // Update is called once per frame
    void Update()
    {


    }
}
