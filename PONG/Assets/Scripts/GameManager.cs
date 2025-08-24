using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Los textos en pantalla para mostrar el puntaje
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    //Posiciones de las paletas y la pelota
    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private Transform ballTransform;

    // Variables internas para almacenar los puntajes
    private int paddle1Score;
    private int paddle2Score;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Cuando el jugador 1 anota
    public void Paddle1Scored()
    {
        paddle1Score++; // Aumenta el puntaje
        paddle1ScoreText.text = paddle1Score.ToString(); // Mostrar puntaje en UI
        StartCoroutine(ResetAndLaunchBall()); // Reiniciar y relanzar la bola
    }

    // Cuando el jugador 2 anota
    public void Paddle2Scored()
    {
        paddle2Score++; 
        paddle2ScoreText.text = paddle2Score.ToString(); 
        StartCoroutine(ResetAndLaunchBall());
    }

    //Para reiniciar la posición de paletas y pelota, esperar y relanzar
    private IEnumerator ResetAndLaunchBall()
    {
        // Reiniciar posiciones de paletas al centro en Y
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);

        // Colocar la bola en el centro
        ballTransform.position = Vector2.zero;

        // Detener la bola (velocidad = 0)
        Rigidbody2D ballRb = ballTransform.GetComponent<Rigidbody2D>();
        ballRb.linearVelocity = Vector2.zero;

        // Esperar medio segundo antes de relanzar
        yield return new WaitForSeconds(0.5f);

        // Relanzar la bola usando el script Ball
        Ball ballScript = ballTransform.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.Launch();
        }
    }
}
