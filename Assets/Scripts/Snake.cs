using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float moveRate; // Tiempo entre movimientos (velocidad)
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] GameManager gameManager;

    private Vector2 direction;
    private Vector2 nextDirection;
    private float moveTimer;
    private List<Transform> segments;
    private Vector2 currentGridPos;

    public float MoveRate { get => moveRate; set => moveRate = value; }

    void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        
        moveTimer = MoveRate;

        int randX = Random.Range(-15, 15);
        int randY = Random.Range(-7, 7);
        currentGridPos = new Vector2(randX + 0.5f, randY + 0.5f);
        transform.position = currentGridPos;
    }

    void Update()
    {
        // Capturar la direcci�n pero sin actualizarla
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector2.down)
            nextDirection = Vector2.up;
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector2.up)
            nextDirection = Vector2.down;
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector2.right)
            nextDirection = Vector2.left;
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector2.left)
            nextDirection = Vector2.right;
        else if (Input.GetKey(KeyCode.P))
        {
            gameManager.PauseGame();
        }
    }

    void FixedUpdate()
    {
        moveTimer -= Time.fixedDeltaTime;
        if (moveTimer > 0f) return;

        moveTimer = MoveRate; // Reset timer
        direction = nextDirection;

        // Mover cuerpo de atr�s hacia adelante
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Mover cabeza
        currentGridPos += direction;
        transform.position = new Vector3(currentGridPos.x, currentGridPos.y, 0f);

        // Verificar si la cabeza colisiona con alguna parte del cuerpo
        for (int i = 1; i < segments.Count; i++)
        {
            if (segments[i].position == transform.position)
            {
                gameManager.SetGameOver();
                Destroy(this.gameObject);
            }
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            gameManager.SetGameOver();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Food"))
        {
            Grow();
            gameManager.AddScore(1);
        }
    }
}
