using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AnimationControl : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;
    public float velocidadCaminar = 3.0f;
    public float velocidadRotacion = 100.0f;
    public float fuerzaSalto = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float translacion = Input.GetAxis("Vertical") * velocidadCaminar;
        float rotacion = Input.GetAxis("Horizontal") * velocidadRotacion;

        Vector3 movimiento = new Vector3(0, 0, translacion);
        movimiento = transform.TransformDirection(movimiento);

        characterController.Move(movimiento * Time.deltaTime);

        transform.Rotate(0, rotacion * Time.deltaTime, 0);

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            anim.SetTrigger("isJump");
            AplicarFuerzaSalto();
        }

        if (translacion != 0)
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", true);
        }
    }

    void AplicarFuerzaSalto()
    {
        Vector3 salto = new Vector3(0, fuerzaSalto, 0);
        characterController.Move(salto * Time.deltaTime);
    }
}
