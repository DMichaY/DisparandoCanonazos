using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject prefabBala;  // Prefab de la bala
    public Transform puntoCanon;   // El punto desde donde sale la bala
    private GameManager gameManager; // Referencia al GameManager

    public float fuerzaDisparo = 15f;  // Fuerza predeterminada, ajustable en el Inspector

    private void Start()
    {
        // Obtener el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Disparar()
    {
        // Obtener todas las dianas activas con el tag "Diana"
        GameObject[] dianas = GameObject.FindGameObjectsWithTag("Diana");

        Transform objetivo = null;

        // Verificar si hay alguna diana activa
        if (dianas.Length > 0)
        {
            // Si hay dianas, tomar la primera
            objetivo = dianas[0].transform;
        }
        else
        {
            // Si no hay dianas, disparar en una direccion aleatoria
            Vector3 direccionAleatoria = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            objetivo = new GameObject("ObjetivoAleatorio").transform;
            objetivo.position = direccionAleatoria;
        }

        // Mantener la rotacion inicial del PuntoCanon pero asegurarnos de que apunte hacia el objetivo
        Vector3 direccion = (objetivo.position - puntoCanon.position).normalized;

        // Instanciar la bala en la posicion del PuntoCanon
        GameObject bala = Instantiate(prefabBala, puntoCanon.position, Quaternion.identity);

        // Asegurar que el GameManager incremente el contador de balas
        if (gameManager != null)
        {
            gameManager.IncrementarBalas();  // Incrementar las balas cuando se dispara una nueva
        }

        Rigidbody rb = bala.GetComponent<Rigidbody>();

        // Aplicar fuerza a la bala para dispararla hacia la diana o en direccion aleatoria
        rb.AddForce(direccion * fuerzaDisparo, ForceMode.Impulse);

        // Si se disparo hacia una direccion aleatoria, eliminar el objeto temporal "ObjetivoAleatorio"
        if (objetivo.name == "ObjetivoAleatorio")
        {
            Destroy(objetivo.gameObject);
        }
    }

}
