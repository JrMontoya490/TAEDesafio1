using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;
    [SerializeField] private float minYVelocity = 0.5f; // para evitar que se vaya recto

    private Rigidbody2D ballRb;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        Launch();
    }

    // Hacer público para que el GameManager pueda llamar después de un punto
    public void Launch()
    {
        float xVelocity = Random.value < 0.5f ? 1f : -1f;
        float yVelocity = Random.value < 0.5f ? 1f : -1f;

        ballRb.linearVelocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Aumentar la velocidad
            ballRb.linearVelocity *= velocityMultiplier;

            // Evitar que la bola quede demasiado plana en el eje Y
            if (Mathf.Abs(ballRb.linearVelocity.y) < minYVelocity)
            {
                float newY = ballRb.linearVelocity.y > 0 ? minYVelocity : -minYVelocity;
                ballRb.linearVelocity = new Vector2(ballRb.linearVelocity.x, newY).normalized * ballRb.linearVelocity.magnitude;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal1"))
        {
            // Punto para jugador 2
            GameManager.Instance.Paddle2Scored();
        }
        else if (collision.gameObject.CompareTag("Goal2"))
        {
            // Punto para jugador 1
            GameManager.Instance.Paddle1Scored();
        }
        // No lanzamos la bola aquí; el GameManager se encarga de reiniciar y relanzar
    }
}
