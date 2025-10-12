using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnTriggerEnter()
    {
        print("Enter!");
    }
    public void OnTriggerStay()
    {
        print("Stay!");
    }
    public void OnTriggerExit()
    {
        print("Exit!");
    }

    // Update is called once per frame
    void Update()
    {


    }
}
