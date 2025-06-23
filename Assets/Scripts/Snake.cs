using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float moveRate = 1f; // Tiempo entre movimientos (velocidad)
    [SerializeField] private Transform segmentPrefab;

    private Vector2 direction;
    private Vector2 nextDirection;
    private float moveTimer;
    private List<Transform> segments;
    private Vector2 currentGridPos;

    void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        
        moveTimer = moveRate;

        int randX = Random.Range(-15, 15);
        int randY = Random.Range(-7, 7);
        currentGridPos = new Vector2(randX + 0.5f, randY + 0.5f);
        transform.position = currentGridPos;
    }

    void Update()
    {
        // Capturar la dirección pero sin actualizarla
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector2.down)
            nextDirection = Vector2.up;
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector2.up)
            nextDirection = Vector2.down;
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector2.right)
            nextDirection = Vector2.left;
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector2.left)
            nextDirection = Vector2.right;
    }

    void FixedUpdate()
    {
        moveTimer -= Time.fixedDeltaTime;
        if (moveTimer > 0f) return;

        moveTimer = moveRate; // Reset timer
        direction = nextDirection;

        // Mover cuerpo de atrás hacia adelante
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Mover cabeza
        currentGridPos += direction;
        transform.position = new Vector3(currentGridPos.x, currentGridPos.y, 0f);
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
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Food"))
        {
            Grow();
        }
    }
}
