using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Fisicas : MonoBehaviour
{
    int countSalto = 0;
    bool enContactoConGround = false;
    bool puedeSaltar = true;
    public float fuerzaSalto = 7500f;

    public AudioClip jumpSound;

    public AudioClip[] groundCollisionSounds;


    private AudioSource fuenteAudio;
    private int nextCollisionSoundIndex = 0; // Índice para rastrear el siguiente sonido de colisión


    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1000f * Time.deltaTime), ForceMode.Acceleration);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1000f * Time.deltaTime), ForceMode.Acceleration);
        }

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1000f * Time.deltaTime, 0, 0), ForceMode.Acceleration);
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1000f * Time.deltaTime, 0, 0), ForceMode.Acceleration);
        }

        //aplico fuerza de gravedad
        if (countSalto > 1 && countSalto < 4)
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 20f, ForceMode.Acceleration);

        salto();
        print(countSalto);

    }

    void salto()
    {
        float fuerzaSalto = 5500f;
        float fuerzaCaida = 7000f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (countSalto < 2 && puedeSaltar)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
                countSalto++;
                if (puedeSaltar)
                {
                    fuenteAudio.clip = jumpSound;
                    fuenteAudio.Play();
                    //print("split");

                }
                //print(countSalto);

            }
            else
            {
                //print(countSalto);
                //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * fuerzaCaida, ForceMode.Impulse);
                puedeSaltar = false;
                //countSalto = 0;
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        enContactoConGround = true;
        print("en contacto");
    }
    //al entrar en contacto con el suelo

    void OnCollisionEnter(Collision collision)
    {
        // Manejar colisiones con diferentes objetos
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reproducir sonido de colisión con el suelo
            AudioClip randomGroundCollisionSound = groundCollisionSounds[Random.Range(0, groundCollisionSounds.Length)];
            fuenteAudio.clip = randomGroundCollisionSound;
            if (Random.Range(1, 15) == 3)
            {
                // Aquí puedes agregar la lógica que desees cuando el número aleatorio sea igual a 3
                Debug.Log("¡El número aleatorio fue 3!");
                fuenteAudio.Play();
            }

            countSalto = 0;
            enContactoConGround = true;
            puedeSaltar = true;
        }
        //else if (collision.gameObject.CompareTag("Auto") || collision.gameObject.CompareTag("Arbol") || collision.gameObject.CompareTag("Casa"))
        //{
        //    // Reproducir sonido correspondiente al tag de la colisión
        //    if (collisionSounds.Length > 0)
        //    {
        //        fuenteAudio.clip = collisionSounds[nextCollisionSoundIndex];
        //        fuenteAudio.Play();
        //        // Actualizar el índice para reproducir el siguiente sonido
        //        nextCollisionSoundIndex = (nextCollisionSoundIndex + 1) % collisionSounds.Length;
        //    }
        //}
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enContactoConGround = false;
        }
    }
}
