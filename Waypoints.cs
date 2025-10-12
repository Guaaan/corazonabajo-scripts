using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Range(0f,2f)] 
    [SerializeField] private float waypointSize = 1f;
    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }
        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            //dibujo una line entre cada hijo de waypoints
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        //dibujo una linea entre el primero y el último
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GettNextWaypoint(Transform currentWaypoint)
    {
        // si el waypoint actual es nulo vamos al primer waypoint
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);

        }
        // si el  waypoint actual es el indice del hermano menos 1
        if(currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        // vamos de nuevo al primer waypoint
        else
        {
            return transform.GetChild(0);
        }
    }
}
