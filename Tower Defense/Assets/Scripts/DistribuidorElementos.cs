using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DistribuidorElementos : MonoBehaviour
{
    [Header("Torre principal")]
    public Transform torre;

    [Header("Campamentos enemigos")]
    public Transform[] campamentos;

    [Header("Ruinas reparables")]
    public GameObject ruinaPrefab;
    public int ruinasPorZona = 2;
    public float anchoZonaRuina = 4f;
    public float altoZonaRuina = 3f;

    [System.Serializable]
    public class Decorado
    {
        public GameObject prefab;
        public int cantidad;
    }

    [Header("Decorados generales")]
    public List<Decorado> decorados;
    public Vector2 areaDecoradoMin = new Vector2(-20, -20);
    public Vector2 areaDecoradoMax = new Vector2(20, 20);
    public float distanciaMinimaEntreElementos = 1.5f;

    [Header("NavMesh")]
    public NavMeshSurface navMeshSurface;

    private List<Vector2> posicionesOcupadas = new List<Vector2>();

    void Start()
    {
        InstanciarRuinas();
        InstanciarDecorados();
        RecalcularNavMesh();
    }

    void InstanciarRuinas()
    {
        foreach (Transform campamento in campamentos)
        {
            Vector2 torre2D = new Vector2(torre.position.x, torre.position.z);
            Vector2 campamento2D = new Vector2(campamento.position.x, campamento.position.z);
            Vector2 centro = (torre2D + campamento2D) / 2;
            Vector2 min = centro - new Vector2(anchoZonaRuina, altoZonaRuina);
            Vector2 max = centro + new Vector2(anchoZonaRuina, altoZonaRuina);

            int intentos = 0;
            int colocados = 0;

            while (colocados < ruinasPorZona && intentos < ruinasPorZona * 10)
            {
                Vector2 posicion2D = GenerarPosicionAleatoria(min, max);
                if (EsPosicionValida(posicion2D))
                {
                    Vector3 posicion3D = new Vector3(posicion2D.x, 0f, posicion2D.y);
                    Instantiate(ruinaPrefab, posicion3D, Quaternion.identity);
                    posicionesOcupadas.Add(posicion2D);
                    colocados++;
                }
                intentos++;
            }
        }
    }

    void InstanciarDecorados()
    {
        foreach (var decorado in decorados)
        {
            int intentos = 0;
            int colocados = 0;

            while (colocados < decorado.cantidad && intentos < decorado.cantidad * 10)
            {
                Vector2 posicion2D = GenerarPosicionAleatoria(areaDecoradoMin, areaDecoradoMax);
                if (EsPosicionValida(posicion2D))
                {
                    Vector3 posicion3D = new Vector3(posicion2D.x, 0f, posicion2D.y);
                    Instantiate(decorado.prefab, posicion3D, Quaternion.identity);
                    posicionesOcupadas.Add(posicion2D);
                    colocados++;
                }
                intentos++;
            }
        }
    }

    Vector2 GenerarPosicionAleatoria(Vector2 min, Vector2 max)
    {
        return new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );
    }

    bool EsPosicionValida(Vector2 nuevaPos)
    {
        foreach (Vector2 pos in posicionesOcupadas)
        {
            if (Vector2.Distance(nuevaPos, pos) < distanciaMinimaEntreElementos)
                return false;
        }
        return true;
    }

    void RecalcularNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogWarning("NavMeshSurface no asignado.");
        }
    }
}
