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

    // Reemplaza el $SELECTION_PLACEHOLDER$ por este código

    [SerializeField] float moveForce = 1000f;
    [SerializeField] float maxSpeed = 8f;

    void Update()
    {
        // Mantener salto en Update para capturar GetKeyDown correctamente
        salto();
        // Opcional: depuración mínima
        // print(countSalto);
    }

    void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Entrada más suave y que incluye flechas y WASD
        float h = Input.GetAxis("Horizontal"); // A/D, <- ->
        float v = Input.GetAxis("Vertical");   // W/S, ^ v

        // Dirección relativa a la cámara (ignora componente Y)
        Transform cam = Camera.main != null ? Camera.main.transform : null;
        Vector3 moveDir;
        if (cam != null)
        {
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;
            moveDir = camRight * h + camForward * v;
            if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();
        }
        else
        {
            // Fallback: usar eje del mundo
            moveDir = new Vector3(v, 0, h);
        }

        // Aplicar fuerza de movimiento (manteniendo sistema físico)
        rb.AddForce(moveDir * moveForce * Time.fixedDeltaTime, ForceMode.Acceleration);

        // Fuerza extra de "caída" cuando está en salto doble (mantener lógica existente)
        if (countSalto > 1 && countSalto < 4)
            rb.AddForce(Vector3.down * 20f * Time.fixedDeltaTime, ForceMode.Acceleration);

        // Limitar velocidad horizontal para mejorar control
        Vector3 horizVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizVel.magnitude > maxSpeed)
        {
            Vector3 limited = horizVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z);
        }
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
