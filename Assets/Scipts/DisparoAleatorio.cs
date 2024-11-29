using UnityEngine;

public class DisparoAleatorio : MonoBehaviour
{
    public GameObject prefabBala;  // Prefab de la bala
    public Transform puntoCanon;   // El punto desde donde sale la bala
    private GameManager gameManager; // Referencia al GameManager
    private Color[] colores = { Color.red, Color.green, Color.blue, Color.yellow, Color.white };

    public float fuerzaMinima = 5f;   // Fuerza manima para la bala
    public float fuerzaMaxima = 20f;  // Fuerza maxima para la bala
    public float escalaMinima = 0.5f; // Tamaño manimo de la bala
    public float escalaMaxima = 2f;   // Tamaño maximo de la bala

    private void Start()
    {
        // Obtener el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("¡GameManager no encontrado! Asegurate de que este en la escena.");
        }
    }

    public void DispararAleatorio()
    {
        // Obtener todas las dianas activas con el tag "Diana"
        GameObject[] dianas = GameObject.FindGameObjectsWithTag("Diana");

        // Si hay dianas, disparar a la primera; si no, disparar en una direccion aleatoria
        Transform objetivo = null;

        if (dianas.Length > 0)
        {
            objetivo = dianas[0].transform;  // Tomar la primera diana encontrada
            Debug.Log($"Disparando a la diana en la posicion {objetivo.position}");
        }
        else
        {
            // No hay dianas, disparar en una direccion aleatoria
            // Podemos elegir un punto aleatorio en el espacio, o en un plano especofico
            Vector3 direccionAleatoria = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            objetivo = new GameObject("ObjetivoAleatorio").transform;
            objetivo.position = direccionAleatoria;
            Debug.Log($"No hay dianas. Disparando en direccion aleatoria: {direccionAleatoria}");
        }

        // Asegurar que el puntoCanon apunte al objetivo (diana o direccion aleatoria)
        puntoCanon.LookAt(objetivo);

        // Instanciar la bala en la posicion del canon (puntoCanon)
        GameObject bala = Instantiate(prefabBala, puntoCanon.position, Quaternion.identity);
        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("¡La bala no tiene un Rigidbody! Asegurate de que el prefab de la bala lo tiene.");
            return;
        }

        // Generar valores aleatorios para fuerza, escala y color
        float fuerzaAleatoria = Random.Range(fuerzaMinima, fuerzaMaxima);
        float escalaAleatoria = Random.Range(escalaMinima, escalaMaxima);
        Color colorAleatorio = colores[Random.Range(0, colores.Length)];

        // Ajustar el tamano de la bala
        bala.transform.localScale = Vector3.one * escalaAleatoria;
        Debug.Log($"Tamano de la bala ajustado a {escalaAleatoria}");

        // Cambiar el color de la bala
        Renderer renderer = bala.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = colorAleatorio;
            Debug.Log($"Color de la bala cambiado a {colorAleatorio}");
        }
        else
        {
            Debug.LogError("¡La bala no tiene un Renderer! Asegurate de que el prefab de la bala tiene un material.");
        }

        // Calcular direccion y aplicar fuerza
        Vector3 direccion = (objetivo.position - puntoCanon.position).normalized;
        rb.AddForce(direccion * fuerzaAleatoria, ForceMode.Impulse);
        Debug.Log($"Fuerza aplicada a la bala: {fuerzaAleatoria}");

        // Incrementar el contador de balas en el GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.IncrementarBalas(); // Incrementa el contador de balas
        }
    }


}
