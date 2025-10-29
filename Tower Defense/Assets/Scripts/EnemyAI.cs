using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 2f;
    public float distanciaAtaque = 1.5f;
    public int dano = 10;

    private Animator animator;
    private bool atacando = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector2.Distance(transform.position, objetivo.position);

        if (distancia > distanciaAtaque)
        {
            // Moverse hacia la torre
            Vector2 direccion = (objetivo.position - transform.position).normalized;
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;

            if (animator) animator.SetBool("Caminando", true);
        }
        /*else
        {
            // Atacar
            if (!atacando)
            {
                atacando = true;
                if (animator) animator.SetTrigger("Atacar");
                Invoke(nameof(RealizarAtaque), 0.5f); // tiempo de animación
            }
        }*/
    }

    /*void RealizarAtaque()
    {
        // Aquí puedes reducir vida de la torre
        TorreVida torre = objetivo.GetComponent<TorreVida>();
        if (torre != null) torre.RecibirDano(dano);

        atacando = false;
    }*/

    public void SetObjetivo(Transform nuevoObjetivo)
    {
        objetivo = nuevoObjetivo;
    }
}

