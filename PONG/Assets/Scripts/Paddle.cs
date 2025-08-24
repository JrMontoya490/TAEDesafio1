using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Velocidad a la que se moverá la paleta
    [SerializeField] private float speed = 7f;

    // Bandera para identificar si esta paleta es la del jugador 1 (true) o la del jugador 2 (false)
    [SerializeField] private bool isPaddle1;

    // Límite en el eje Y para que la paleta no se salga de la pantalla
    private float yBound = 4.20f;

    void Update()
    {
        float movement;

        // Si es la paleta 1, se controla con el eje "Vertical" W/S
        if (isPaddle1)
        {
            movement = Input.GetAxisRaw("Vertical");
        }
        // Si es la paleta 2, se controla con el eje "Vertical2" flechas arriba/abajo
        else
        {
            movement = Input.GetAxisRaw("Vertical2");
        }

        // Guardamos la posición actual de la paleta
        Vector2 paddlePosition = transform.position;

        // Calculamos el nuevo valor en Y, aplicando el movimiento, la velocidad y el tiempo
        // Clamp asegura que la paleta no sobrepase los límites definidos en yBound
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);

        // Aplicamos la nueva posición a la paleta
        transform.position = paddlePosition; 
    }
}
