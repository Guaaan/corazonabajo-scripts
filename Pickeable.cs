using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickeable : MonoBehaviour
{
    [System.Flags]
    public enum EjesRotacion
    {
        Ninguno = 0,
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2
    }

    [Header("Rotación")]
    public EjesRotacion ejesRotacion = EjesRotacion.Y;
    public float velocidadRotacionX = 70f;
    public float velocidadRotacionY = 40f;
    public float velocidadRotacionZ = 0f;

    [Header("Movimiento Vertical")]
    //public float alturaMaxima = 1.5f; // Altura máxima a la que subirá el objeto
    public float velocidadSubidaBajada = 0.2f; // Velocidad de subida y bajada en unidades por segundo

    [Header("Opciones Avanzadas")]
    public bool rotarHijos = false;

    void Update()
    {
        float rotacionX = ((ejesRotacion & EjesRotacion.X) != 0) ? velocidadRotacionX * Time.deltaTime : 0f;
        float rotacionY = ((ejesRotacion & EjesRotacion.Y) != 0) ? velocidadRotacionY * Time.deltaTime : 0f;
        float rotacionZ = ((ejesRotacion & EjesRotacion.Z) != 0) ? velocidadRotacionZ * Time.deltaTime : 0f;
        Vector3 rotacion = new Vector3(rotacionX, rotacionY, rotacionZ);
        transform.Rotate(rotacion);

        // Rotar todos los hijos directos igual que el objeto principal (opcional)
        if (rotarHijos)
        {
            foreach (Transform hijo in transform)
            {
                hijo.Rotate(rotacion);
            }
        }

        // Movimiento vertical (subir y bajar)
        //float nuevaAltura = Mathf.PingPong(Time.time * velocidadSubidaBajada, alturaMaxima);
        //transform.position = new Vector3(transform.position.x, nuevaAltura, transform.position.z);
    }
}
