using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform jugador;
    public Vector2 margen; // Margen horizontal y vertical antes de mover la cámara
    public float suavizado = 5f;

    private Vector3 destino;

    void LateUpdate()
    {
        Vector3 posicionCamara = transform.position;
        Vector3 posicionJugador = jugador.position;

        // Comprobamos si el jugador se sale del margen horizontal
        if (Mathf.Abs(posicionJugador.x - posicionCamara.x) > margen.x)
        {
            posicionCamara.x = posicionJugador.x - Mathf.Sign(posicionJugador.x - posicionCamara.x) * margen.x;
        }

        // Comprobamos si el jugador se sale del margen vertical
        if (Mathf.Abs(posicionJugador.y - posicionCamara.y) > margen.y)
        {
            posicionCamara.y = posicionJugador.y - Mathf.Sign(posicionJugador.y - posicionCamara.y) * margen.y;
        }

        // Aplicamos suavizado al movimiento
        destino = new Vector3(posicionCamara.x, posicionCamara.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * suavizado);
    }
}
