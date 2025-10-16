using UnityEngine;

[ExecuteAlways]
public class AlwaysUpCameraTarget : MonoBehaviour
{
    [Tooltip("Altura sobre la posición del padre (en metros).")]
    public float height = 1.8f;

    [Tooltip("Si true, mantiene el yaw (ángulo Y) del padre. Si false, no hereda ninguna rotación del padre.")]
    public bool keepParentYaw = false;

    [Header("Opcional: suavizado de posición")]
    public bool smoothPosition = false;
    [Tooltip("Velocidad de interpolación para el suavizado.")]
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (transform.parent == null) return;

        // Posición objetivo: misma XZ del padre + altura en Y (global)
        Vector3 targetPos = transform.parent.position + Vector3.up * height;

        if (smoothPosition)
            transform.position = Vector3.Lerp(transform.position, targetPos, Mathf.Clamp01(Time.deltaTime * smoothSpeed));
        else
            transform.position = targetPos;

        // Rotación: alineamos el eje 'up' con el up global.
        // Opciones:
        // - keepParentYaw == true -> mantén el yaw (Y) del padre, pero elimina pitch/roll.
        // - keepParentYaw == false -> no hereda rotación, queda "orientación neutra" con up global.
        if (keepParentYaw)
        {
            float parentYaw = transform.parent.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, parentYaw, 0f);
        }
        else
        {
            // Rotación global neutra (arriba = Vector3.up)
            transform.rotation = Quaternion.identity;
        }
    }

    // Para que en el editor el objeto se mantenga en la altura correcta al mover el padre
    #if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (transform.parent != null)
                transform.position = transform.parent.position + Vector3.up * height;
        }
    }
    #endif
}
