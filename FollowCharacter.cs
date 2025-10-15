using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Vector3 offset;
    Vector3 newpos;
    public Transform player;
    public Transform secondPlayer; // Variable similar al jugador principal

    public float suavidad = 10f;
    // Factor de suavizado para la posición (separado del orthographicSize)
    public float positionSmoothing = 5f;
    // Limites para la cámara ortográfica y suavidad del zoom
    public float minOrthoSize = 2f;
    public float maxOrthoSize = 50f;
    public new Camera camera;

    // variable para controlar el cambio con la rueda del mouse
    public float velocidadDeCambio = 1.0f;

    private void Start()
    {
        //offset = player.transform.position - transform.position;
        offset = new Vector3(150, -80, - 130);
        // Asegúrate de asignar la cámara en el inspector
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    private void Update()
    {
        // Si player fue destruido o es null, intentar tomar secondPlayer; si no, salir
        if (!player)
        {
            if (secondPlayer)
            {
                player = secondPlayer;
                secondPlayer = null;
            }
            else
            {
                return; // nada que seguir; evita acceder a un Transform destruido
            }
        }

    // Obtener el valor de la rueda del mouse para cambiar el zoom (orthographicSize)
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    suavidad -= scroll * velocidadDeCambio;
    // Clampear el valor de suavidad/zoom para evitar valores extremos
    suavidad = Mathf.Clamp(suavidad, minOrthoSize, maxOrthoSize);

        float m_ViewPositionX = 0f, m_ViewPositionY = 0f, m_ViewWidth = 1f, m_ViewHeight = 1f;

        if (camera == null) camera = Camera.main;
        camera.enabled = true;

        if (camera)
        {
            camera.orthographic = true;
            camera.orthographicSize = Mathf.Max(0.01f, suavidad); // prevenir valores <= 0
            camera.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
        }

        // Usar player.position (player es Transform) y proteger el acceso ya hecho arriba
        newpos = transform.position;
        newpos.x = player.position.x - offset.x;
        newpos.z = player.position.z - offset.z;

        // Para el eje Y, aplicamos un suavizado separado y limitamos la velocidad de cambio en Y
        float targetY = player.position.y - offset.y;
        float maxDeltaY = 10f * Time.deltaTime; // limitar cambio vertical por frame
        newpos.y = Mathf.MoveTowards(transform.position.y, targetY, maxDeltaY);

        // Lerp con factor positionSmoothing para movimiento más estable
        transform.position = Vector3.Lerp(transform.position, newpos, positionSmoothing * Time.deltaTime);
        transform.LookAt(player);
    }

    //void FixedUpdate()
    //{
    //    if (player != null)
    //    {
    //        transform.LookAt(player);
    //    }
    //}
}

