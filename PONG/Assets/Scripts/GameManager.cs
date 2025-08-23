using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private Transform ballTransform;

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
        // Asegurarse de que solo haya una instancia
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Paddle1Scored()
    {
        paddle1Score++;
        paddle1ScoreText.text = paddle1Score.ToString();
        StartCoroutine(ResetAndLaunchBall());
    }

    public void Paddle2Scored()
    {
        paddle2Score++;
        paddle2ScoreText.text = paddle2Score.ToString();
        StartCoroutine(ResetAndLaunchBall());
    }

    private IEnumerator ResetAndLaunchBall()
    {
        // Reiniciar posiciones
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);
        ballTransform.position = Vector2.zero;

        // Detener la bola
        Rigidbody2D ballRb = ballTransform.GetComponent<Rigidbody2D>();
        ballRb.linearVelocity = Vector2.zero;

        // Esperar medio segundo
        yield return new WaitForSeconds(0.5f);

        // Lanzar la bola
        Ball ballScript = ballTransform.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.Launch();
        }
    }
}
