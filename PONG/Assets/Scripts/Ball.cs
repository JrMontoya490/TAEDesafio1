using UnityEngine;

public class Ball : MonoBehaviour
{
    // Velocidad inicial de la bola al comenzar
    [SerializeField] private float initialVelocity = 4f;

    // Multiplicador de velocidad al golpear una paleta (la bola se acelera poco a poco)
    [SerializeField] private float velocityMultiplier = 1.1f;

    // Velocidad mínima en Y para que la bola no se mueva en línea recta horizontal
    [SerializeField] private float minYVelocity = 0.5f; 

    // Referencia al Rigidbody2D de la bola
    private Rigidbody2D ballRb;

    void Start()
    {
        // Obtener el Rigidbody2D de la bola al iniciar
        ballRb = GetComponent<Rigidbody2D>();

        // Lanzar la bola al inicio de la partida
        Launch();
    }

    // Método público para que el GameManager pueda relanzar la bola después de un punto
    public void Launch()
    {
        // Dirección aleatoria en X,y
        float xVelocity = Random.value < 0.5f ? 1f : -1f;
        float yVelocity = Random.value < 0.5f ? 1f : -1f;

        // Asignar la velocidad inicial a la bola
        ballRb.linearVelocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    // Detecta colisiones físicascuando choca con paletas o bordes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con una paleta
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Aumentar la velocidad de la bola
            ballRb.linearVelocity *= velocityMultiplier;

            // Evitar que la bola quede demasiado horizontal (con poco movimiento en Y)
            if (Mathf.Abs(ballRb.linearVelocity.y) < minYVelocity)
            {
                // Si está muy plana, forzamos un mínimo movimiento en Y
                float newY = ballRb.linearVelocity.y > 0 ? minYVelocity : -minYVelocity;

                // Normalizamos la dirección pero mantenemos la misma magnitud (velocidad total)
                ballRb.linearVelocity = new Vector2(ballRb.linearVelocity.x, newY).normalized 
                                        * ballRb.linearVelocity.magnitude;
            }
        }
    }

    // Detecta triggers cuando entra en las zonas de gol
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal1"))
        {
            // Si entra en la portería 1, punto para el jugador 2
            GameManager.Instance.Paddle2Scored();
        }
        else if (collision.gameObject.CompareTag("Goal2"))
        {
            // Si entra en la portería 2, punto para el jugador 1
            GameManager.Instance.Paddle1Scored();
        }
    }
}
