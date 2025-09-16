using UnityEngine;

public class TransparenciaEdificio : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color colorOriginal = Color.white;
    public Color colorTransparente = new Color(1f, 1f, 1f, 0.4f); // 40% opacidad

    void Start()
    {
        spriteRenderer.color = colorOriginal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = colorTransparente;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = colorOriginal;
        }
    }
}
