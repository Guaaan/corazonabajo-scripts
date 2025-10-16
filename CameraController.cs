using System;
using UnityEngine;

/// <summary>
/// Controlador único de cámara. Maneja el seguimiento suavizado de un target
/// y notifica a un handler de Cinemachine si existe. También dispara un ParticleSystem
/// opcional cuando se cambia de objetivo.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset & smoothing")]
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public float positionSmoothing = 5f; // suavizado para movimiento de cámara
    public float maxVerticalDeltaPerSecond = 20f; // limita cambio vertical por segundo

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Effects")]
    public ParticleSystem switchParticles;

    [Header("Handlers")]
    public CinemachineHandler cinemachineHandler; // opcional

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Construir posición objetivo basada en offset
        Vector3 desired = new Vector3(
            target.position.x - offset.x,
            target.position.y - offset.y,
            target.position.z - offset.z);

        // Limitar cambio vertical por frame para evitar oscilaciones bruscas
        float maxDelta = maxVerticalDeltaPerSecond * Time.deltaTime;
        desired.y = Mathf.MoveTowards(transform.position.y, desired.y, maxDelta);

        // Suavizar movimiento general
        Vector3 smooth = Vector3.Lerp(transform.position, desired, positionSmoothing * Time.deltaTime);
        transform.position = smooth;

        // Mantener mirada hacia el objetivo
        transform.LookAt(target);
    }

    /// <summary>
    /// Cambia el objetivo seguido por la cámara de forma segura.
    /// Si hay un target previo, reposiciona el nuevo target a la posición del anterior
    /// para que el jugador nuevo "reemplace" al anterior en escena.
    /// </summary>
    public void SetTarget(Transform newTarget, bool playParticle = true)
    {
        if (newTarget == null) return;

        // Si hay un objetivo previo, colocar al nuevo en su posición
        if (target != null)
        {
            try
            {
                newTarget.position = target.position;
            }
            catch (Exception)
            {
                // seguridad por si el transform es incontrolable
            }
        }

        target = newTarget;

        if (playParticle && switchParticles != null)
        {
            switchParticles.Play();
        }

        // Notificar a Cinemachine si existe
        if (cinemachineHandler != null)
        {
            cinemachineHandler.UpdateVirtualCameraTarget(newTarget);
        }
    }
}
