using UnityEngine;

public class Diana : MonoBehaviour
{
    private int cantidadImpactos = 0; // Contador de impactos
    private Renderer dianaRenderer;  // Referencia al Renderer para cambiar el color
    private bool rotando = false;   // Flag para saber si debe rotar
    private bool colorCambiado = false; // Flag para controlar si ya cambió de color

    // Lista de colores aleatorios para cambiar
    private Color[] coloresAleatorios = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };

    // Velocidad de rotación
    public float velocidadRotacion = 20f;

    void Start()
    {
        dianaRenderer = GetComponent<Renderer>();  // Obtener el Renderer de la diana
    }

    void OnTriggerEnter(Collider col)  // Usamos OnTriggerEnter para que la bala atraviese el trigger
    {
        if (col.gameObject.CompareTag("Bala"))  // Si la bala colisiona con la diana
        {
            cantidadImpactos++;

            if (cantidadImpactos == 1 && !colorCambiado)
            {
                // Primer impacto: cambia el color aleatoriamente
                CambiarColorAleatorio();
            }
            else if (cantidadImpactos == 2 && colorCambiado)
            {
                // Segundo impacto: empieza a rotar la diana
                IniciarRotacion();
            }
            else if (cantidadImpactos == 3)
            {
                // Tercer impacto: destruye la diana
                DestruirDiana();
            }
        }
    }

    void CambiarColorAleatorio()
    {
        // Cambiar el color de la diana aleatoriamente
        dianaRenderer.material.color = coloresAleatorios[Random.Range(0, coloresAleatorios.Length)];
        colorCambiado = true;  // Asegurar que el color ya fue cambiado
    }

    void IniciarRotacion()
    {
        // Activar la rotación
        rotando = true;
    }

    void DestruirDiana()
    {
        // Notificar al GameManager que la diana ha sido destruida
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.EliminarDiana(transform);  // Eliminar la referencia de la diana de la lista
        }

        // Destruir la diana
        Destroy(gameObject);
    }

    void Update()
    {
        // Si la diana debe rotar, gira solo sobre el eje Y
        if (rotando)
        {
            // Mantener la rotación actual en X y Z, solo cambia la rotación en Y
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + velocidadRotacion * Time.deltaTime, transform.rotation.eulerAngles.z);
        }
    }
}
