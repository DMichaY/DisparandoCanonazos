using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textoBalas; // Texto TMP para mostrar las balas
    private int balasEnEscena = 0; // Contador de balas
    private List<Transform> dianas;  // Lista de dianas activas en la escena

    void Start()
    {
        // Verificar que el textoBalas este asignado correctamente en el Inspector
        if (textoBalas == null)
        {
            Debug.LogError("¡No se ha asignado el texto de balas en GameManager!");
        }

        // Inicializamos las dianas
        dianas = new List<Transform>(GameObject.FindGameObjectsWithTag("Diana").Length);
        foreach (var dianaObj in GameObject.FindGameObjectsWithTag("Diana"))
        {
            dianas.Add(dianaObj.transform);  // Agregar las dianas encontradas en la escena
        }

        // Actualizar el texto de balas
        ActualizarTextoBalas();
    }

    public void IncrementarBalas()
    {
        balasEnEscena++;
        ActualizarTextoBalas(); // Actualizar el texto de balas
    }

    public void DecrementarBalas()
    {
        balasEnEscena--;
        ActualizarTextoBalas(); // Actualizar el texto de balas
    }

    private void ActualizarTextoBalas()
    {
        if (textoBalas != null)
        {
            textoBalas.text = "Balas en escena: " + balasEnEscena;
        }
        else
        {
            Debug.LogError("¡Texto de balas no asignado!");
        }
    }

    public void EliminarBalas()
    {
        balasEnEscena = 0; // Resetear el contador de balas
        ActualizarTextoBalas(); // Actualizar el texto de balas
    }

    public void EliminarDiana(Transform diana)
    {
        if (dianas.Contains(diana))
        {
            dianas.Remove(diana);  // Eliminar la diana de la lista
        }
    }
}
