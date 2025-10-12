using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;
    private Transform currentWaypoint;

    void Start()
    {
        currentWaypoint = waypoints.GettNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;
        currentWaypoint = waypoints.GettNextWaypoint(currentWaypoint);
    }

    void Update()
    {
        MoveTowardsWaypoint();
        RotateTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GettNextWaypoint(currentWaypoint);
        }
    }

    void RotateTowardsWaypoint()
    {
        Vector3 directionToTarget = currentWaypoint.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
