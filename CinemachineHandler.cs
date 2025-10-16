using UnityEngine;
#if CINEMACHINE_INSTALLED
using Cinemachine;
#endif

/// <summary>
/// Handler opcional para integrar con Cinemachine sin forzar la dependencia.
/// Si Cinemachine está presente, reasigna la virtual camera; si no, el handler
/// simplemente no hace nada.
/// </summary>
public class CinemachineHandler : MonoBehaviour
{
#if CINEMACHINE_INSTALLED
    public CinemachineVirtualCamera virtualCamera;

    public void UpdateVirtualCameraTarget(Transform target)
    {
        if (virtualCamera == null || target == null) return;
        virtualCamera.Follow = target;
        virtualCamera.LookAt = target;
    }
#else
    public void UpdateVirtualCameraTarget(Transform target)
    {
        // Cinemachine no está instalado o la define no está presente; no hacemos nada.
    }
#endif
}
