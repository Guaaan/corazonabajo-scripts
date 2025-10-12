using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraContainer : MonoBehaviour
{
    public Transform playerTransform; // Referencia al transform del jugador principal
    public Transform secondPlayerTransform; // Referencia al transform del segundo jugador
    public Transform componenteAMover; // Componente que quieres mover

    void Update()
    {
        // Si el jugador principal existe, mover el componente hacia su posición
        if (playerTransform != null)
        {
            MoverComponenteHaciaJugador(playerTransform);
        }
        // Si el segundo jugador existe, mover el componente hacia su posición
        else if (secondPlayerTransform != null)
        {
            MoverComponenteHaciaJugador(secondPlayerTransform);
        }
    }

    // Método para mover el componente hacia la posición del jugador dado
    private void MoverComponenteHaciaJugador(Transform jugadorTransform)
    {
        // Obtener la posición del jugador
        Vector3 posicionJugador = jugadorTransform.position;

        // Mover el componente al mismo lugar que el jugador
        if (componenteAMover != null)
        {
            componenteAMover.position = posicionJugador;
        }
    }
}
