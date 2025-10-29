using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Oleadas")]
    public GameObject[] enemigosPrefabs;
    public int enemigosPorOleada = 5;
    public float intervaloEntreOleadas = 10f;
    public float intervaloEntreEnemigos = 1f;

    [Header("Destino")]
    public Transform objetivo; // La torre

    private int oleadaActual = 0;
    private bool lanzandoOleada = false;

    void Start()
    {
        InvokeRepeating(nameof(LanzarOleada), 2f, intervaloEntreOleadas);
    }

    void LanzarOleada()
    {
        if (!lanzandoOleada)
        {
            lanzandoOleada = true;
            StartCoroutine(SpawnOleada());
        }
    }

    System.Collections.IEnumerator SpawnOleada()
    {
        for (int i = 0; i < enemigosPorOleada; i++)
        {
            GameObject prefab = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];
            GameObject enemigo = Instantiate(prefab, transform.position, Quaternion.identity);

            // Le pasamos el objetivo al que debe dirigirse
            var ia = enemigo.GetComponent<EnemyAI>();
            if (ia != null) ia.SetObjetivo(objetivo);

            yield return new WaitForSeconds(intervaloEntreEnemigos);
        }

        oleadaActual++;
        lanzandoOleada = false;
    }
}