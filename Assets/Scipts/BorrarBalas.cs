using UnityEngine;

public class BorrarBalas : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Borrar()
    {
        GameObject[] balas = GameObject.FindGameObjectsWithTag("Bala");

        foreach (GameObject bala in balas)
        {
            Destroy(bala);
        }

        // Actualizar el contador de balas en el GameManager
        gameManager.EliminarBalas();
    }
}
