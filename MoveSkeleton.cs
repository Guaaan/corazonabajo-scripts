using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkeleton : MonoBehaviour
{
    //clase predefinida para los movimientos
    public CharacterController controller;
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }
     
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Jump");

        //print(x);
        //print(z);

        Vector3 movement = transform.right * x + transform.forward * z + transform.up * y;

        controller.Move(movement * speed * Time.deltaTime);
    }
}
