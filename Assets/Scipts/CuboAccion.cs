using UnityEngine;

public class CuboAccion : MonoBehaviour
{
    public enum TipoAccion { Disparar, Borrar, DisparoAleatorio }
    public TipoAccion accion; // El tipo de acci�n que realiza este cubo

    private Disparo disparo;
    private DisparoAleatorio disparoAleatorio;
    private BorrarBalas borrarBalas;

    private void Start()
    {
        disparo = FindObjectOfType<Disparo>();
        disparoAleatorio = FindObjectOfType<DisparoAleatorio>();
        borrarBalas = FindObjectOfType<BorrarBalas>();
    }

    private void OnMouseDown()
    {
        // Detectar el tipo de acci�n y ejecutar la funci�n correspondiente
        switch (accion)
        {
            case TipoAccion.Disparar:
                if (disparo != null) disparo.Disparar();
                break;

            case TipoAccion.Borrar:
                if (borrarBalas != null) borrarBalas.Borrar();
                break;

            case TipoAccion.DisparoAleatorio:
                if (disparoAleatorio != null) disparoAleatorio.DispararAleatorio();  // Cambi� Disparar a DispararAleatorio
                break;
        }
    }
}