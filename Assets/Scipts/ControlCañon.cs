using UnityEngine;
using TMPro;
using System.Collections;

public class ControlCanon : MonoBehaviour
{
    [Header("Configuración del Cañón")]
    [SerializeField] private GameObject balaPrefab; // Prefab de la bala
    [SerializeField] private Transform puntoCanon; // Punto de salida de las balas
    [SerializeField] private Renderer canonRenderer; // Renderer del cañón para cambiar color

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoBalas; // Texto del contador de balas

    [Header("Diana")]
    [SerializeField] private GameObject diana; // Objeto Diana

    private int impactosDiana = 0; // Contador de impactos en la diana

    private void Start()
    {
        // Verificar referencias asignadas en el Inspector
        if (balaPrefab == null) Debug.LogError("Prefab 'Bala' no asignado. Arrástralo al campo 'Bala Prefab' en el Inspector.");
        if (puntoCanon == null) Debug.LogError("'PuntoCanon' no asignado. Arrástralo al campo 'Punto Canon' en el Inspector.");
        if (textoBalas == null) Debug.LogError("'TextoBalas' no asignado. Arrástralo al campo 'Texto Balas' en el Inspector.");
        if (diana == null) Debug.LogError("'Diana' no asignada. Arrástrala al campo 'Diana' en el Inspector.");
        if (canonRenderer == null) Debug.LogError("'Renderer del cañón' no asignado. Arrástralo al campo 'Canon Renderer' en el Inspector.");
    }

    private void Update()
    {
        // Actualizar el contador de balas en la escena
        int numBalas = GameObject.FindGameObjectsWithTag("Bala").Length;
        if (textoBalas != null)
        {
            textoBalas.text = "Balas en escena: " + numBalas;
        }
    }

    public void DispararCanonazo()
    {
        Debug.Log("Botón Verde presionado - Disparando cañonazo");

        if (balaPrefab != null && puntoCanon != null)
        {
            // Instanciar la bala
            GameObject bala = Instantiate(balaPrefab, puntoCanon.position, puntoCanon.rotation);

            // Configurar Rigidbody para movimiento
            Rigidbody rb = bala.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(puntoCanon.forward * 500f);

            // Cambiar color temporal del cañón
            StartCoroutine(CambiarColorCanonTemporal(bala));
        }
        else
        {
            Debug.LogError("Referencia nula en balaPrefab o puntoCanon.");
        }
    }

    public void DisparoAleatorio()
    {
        Debug.Log("Botón Blanco presionado - Disparando con valores aleatorios");

        if (balaPrefab != null && puntoCanon != null)
        {
            // Instanciar la bala
            GameObject bala = Instantiate(balaPrefab, puntoCanon.position, puntoCanon.rotation);

            // Configurar Rigidbody
            Rigidbody rb = bala.AddComponent<Rigidbody>();
            rb.useGravity = true;

            // Tamaño aleatorio
            float escala = Random.Range(0.5f, 2f);
            bala.transform.localScale = Vector3.one * escala;

            // Fuerza aleatoria
            float fuerza = Random.Range(300f, 700f);
            rb.AddForce(puntoCanon.forward * fuerza);

            // Color aleatorio
            Renderer renderer = bala.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color[] colores = { Color.red, Color.green, Color.blue, Color.yellow, Color.white };
                renderer.material.color = colores[Random.Range(0, colores.Length)];
            }

            StartCoroutine(CambiarColorCanonTemporal(bala));
        }
        else
        {
            Debug.LogError("Referencia nula en balaPrefab o puntoCanon.");
        }
    }

    public void EliminarBalas()
    {
        Debug.Log("Botón Rojo presionado - Eliminando balas");

        // Buscar todas las balas por su tag y eliminarlas
        GameObject[] balas = GameObject.FindGameObjectsWithTag("Bala");
        foreach (GameObject bala in balas)
        {
            Destroy(bala);
        }
    }

    private IEnumerator CambiarColorCanonTemporal(GameObject bala)
    {
        if (canonRenderer == null) yield break;

        Color colorOriginal = canonRenderer.material.color;

        // Cambiar el color mientras la bala esté cerca
        while (bala != null && Vector3.Distance(bala.transform.position, puntoCanon.position) < 5f)
        {
            canonRenderer.material.color = Color.yellow;
            yield return null;
        }

        // Restaurar el color original
        canonRenderer.material.color = colorOriginal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            impactosDiana++;
            Debug.Log("Impacto en Diana: " + impactosDiana);

            if (impactosDiana == 1)
            {
                diana.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (impactosDiana == 2)
            {
                StartCoroutine(RotarDiana());
            }
            else if (impactosDiana == 3)
            {
                Destroy(diana);
            }
        }
    }

    private IEnumerator RotarDiana()
    {
        while (diana != null)
        {
            diana.transform.Rotate(Vector3.up * 50 * Time.deltaTime);
            yield return null;
        }
    }
}
