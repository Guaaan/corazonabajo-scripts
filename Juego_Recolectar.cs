using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using Cinemachine;

public class Juego_Recolectar : MonoBehaviour
{
    public int cantRecoleccion = 0;
    public GameObject jugadorUno;  // Asigna el objeto del jugador uno desde el Inspector
    public GameObject jugadorDos;  // Asigna el objeto del jugador dos desde el Inspector
    // Referencia opcional al script que sigue al jugador (si existe en la cámara o en un objeto)
    public FollowCharacter followScript;
    public AudioClip[] restarCantidadSounds;
    public AudioClip[] sumarCantidadSounds;
    private AudioSource fuenteAudio;
    private int nextCollisionSoundIndex = 0; // Índice para rastrear el siguiente sonido de colisión


    // Referencia a la cámara virtual de Cinemachine
    // public CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        print("Objetos recolectados: " + cantRecoleccion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SumarCantidad") && cantRecoleccion >= 0)
        {
            cantRecoleccion = cantRecoleccion + 1;
            print("Objetos recolectados: " + cantRecoleccion);
            Destroy(other.gameObject);

            if (cantRecoleccion >= 8)
            {
                CambiarJugador();
            }
            if (sumarCantidadSounds.Length > 0)
            {
                fuenteAudio.clip = sumarCantidadSounds[nextCollisionSoundIndex];
                fuenteAudio.Play();
                // Actualizar el índice para reproducir el siguiente sonido
                nextCollisionSoundIndex = (nextCollisionSoundIndex + 1) % sumarCantidadSounds.Length;
            }
        }
        else if (other.gameObject.CompareTag("RestarCantidad") && cantRecoleccion >= 0)
        {
            cantRecoleccion = cantRecoleccion - 1;
            print("Objetos recolectados: " + cantRecoleccion);
            if (restarCantidadSounds.Length > 0)
            {
                fuenteAudio.clip = restarCantidadSounds[nextCollisionSoundIndex];
                fuenteAudio.Play();
                // Actualizar el índice para reproducir el siguiente sonido
                nextCollisionSoundIndex = (nextCollisionSoundIndex + 1) % restarCantidadSounds.Length;
            }
            // No destruimos el objeto en este caso
        }
        else
        {
            print("Juego Terminado");
        }
    }

    void CambiarJugador()
    {
        // Obtener la posición actual del jugadorUno
        Vector3 posicionActual = jugadorUno.transform.position;

        // Destruir el jugadorUno
        jugadorDos.SetActive(true);
        jugadorDos.transform.position = posicionActual;

        // Reasignar referencia en el script que sigue al jugador, si está presente
        if (followScript != null && jugadorDos != null)
        {
            // Si followScript apuntaba a jugadorUno o está vacío, reemplazamos por jugadorDos
            if (followScript.player == null || (followScript.player != null && followScript.player.gameObject == jugadorUno))
            {
                followScript.player = jugadorDos.transform;
                followScript.secondPlayer = null;
            }
        }

        // Destruir el jugadorUno con un pequeño delay para dar tiempo a que otros componentes actualicen referencias
        Destroy(jugadorUno, 0.1f);
        // // Actualizar la cámara virtual de Cinemachine para seguir y mirar al jugadorDos
        // if (virtualCamera != null)
        // {
        //     virtualCamera.Follow = jugadorDos.transform;
        //     virtualCamera.LookAt = jugadorDos.transform;
        // }

        print("Cambiando a Jugador Dos");
        print("Posición del Jugador Dos: " + jugadorDos.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (cantRecoleccion < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
