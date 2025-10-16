using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_mode : MonoBehaviour
{
    public GameObject[] huesosArr;
    public Juego_Recolectar juegoRecolectar;

    void Start()
    {
        // Inicializa tu array de huesos u otras configuraciones si es necesario
    }

    void Update()
    {
        // Verifica la cantidad de recolección del jugador y actualiza la visibilidad de los huesos
        ActualizarVisibilidadHuesos();
    }

    void ActualizarVisibilidadHuesos()
    {
        int cantRecoleccion = juegoRecolectar.cantRecoleccion;

        // Itera a través de los huesos y habilita/deshabilita según la cantidad de recolección
        for (int i = 0; i < huesosArr.Length; i++)
        {
            if (i < cantRecoleccion)
            {
                huesosArr[i].SetActive(true); // Habilita el hueso si la posición es menor que la cantidad de recolección
            }
            else
            {
                huesosArr[i].SetActive(false); // Deshabilita el hueso si la posición es mayor o igual que la cantidad de recolección
            }
        }
    }
}
