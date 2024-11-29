using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject canon;  // Referencia al objeto del canon
    private Color[] colores = { Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.cyan, Color.white };  // Colores posibles

    private void Start()
    {
        // Asegurate de que el canon tiene un Collider y que este configurado como trigger
        if (!canon.GetComponent<Collider>())
        {
            canon.AddComponent<BoxCollider>().isTrigger = true;  // Anadir Collider si no existe
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra en el collider es una bala
        if (other.CompareTag("Bala"))
        {
            CambiarColorCanon();  // Cambiar el color del canon de manera aleatoria cuando una bala entra
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si la bala sale del area de la caja del canon, se puede cambiar el color tambien si lo deseas
        if (other.CompareTag("Bala"))
        {
            CambiarColorCanon();  // Cambiar el color cuando una bala sale
        }
    }

    private void CambiarColorCanon()
    {
        // Cambiar el color del canon de manera aleatoria
        int indiceColor = Random.Range(0, colores.Length);  // Obtener un indice aleatorio
        Renderer renderer = canon.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = colores[indiceColor];  // Asignar el color aleatorio
        }
    }
}
