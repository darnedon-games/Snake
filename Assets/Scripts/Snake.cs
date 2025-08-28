using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float moveRate; // Tiempo entre movimientos (velocidad)
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] GameManager gameManager;
    [SerializeField] private Transform teletransportTop;
    [SerializeField] private Transform teletransportBottom;
    [SerializeField] private Transform teletransportLeft;
    [SerializeField] private Transform teletransportRight;
    [SerializeField] private AudioClip foodSound;
    [SerializeField] private AudioClip hitSound;

    private Vector2 direction;
    private Vector2 nextDirection;
    private float moveTimer;
    private List<Transform> segments;
    private Vector2 currentGridPos;
    private AudioSource sound;

    public float MoveRate { get => moveRate; set => moveRate = value; }

    void Start()
    {
        sound = this.GetComponent<AudioSource>();

        segments = new List<Transform>();
        segments.Add(this.transform);
        
        moveTimer = MoveRate;

        //int randX = Random.Range(-15, 15);
        //int randY = Random.Range(-7, 7);
        //currentGridPos = new Vector2(randX + 0.5f, randY + 0.5f);
        //currentGridPos = new Vector2(0 + 0.5f, 0 + 0.5f);
        currentGridPos = new Vector2(0, 0);
        transform.position = currentGridPos;
    }

    void Update()
    {
        // Capturar la dirección pero sin actualizarla
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector2.down)
        {
            nextDirection = Vector2.up;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector2.up)
        {
            nextDirection = Vector2.down;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector2.right)
        {
            nextDirection = Vector2.left; 
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector2.left)
        {
            nextDirection = Vector2.right; 
        }
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

        // Mover cuerpo de atrás hacia adelante
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
            sound.PlayOneShot(hitSound, 1f);
            gameManager.SetGameOver();
            //Destroy(this.gameObject);
        }
        else if (other.CompareTag("Food"))
        {
            sound.PlayOneShot(foodSound, 0.8f);
            Grow();
            gameManager.AddScore(1);
        }
        else if (other.CompareTag("Teletransport"))
        {
            if (other.transform == teletransportTop)
            {
                transform.position = teletransportBottom.position + (new Vector3(0,1f,0));
                currentGridPos = teletransportBottom.position + (new Vector3(0, 1f, 0));
            }
            else if (other.transform == teletransportBottom)
            {
                transform.position = teletransportTop.position - (new Vector3(0, 1f, 0));
                currentGridPos = teletransportTop.position - (new Vector3(0, 1f, 0));
            }
            else if (other.transform == teletransportLeft)
            {
                transform.position = teletransportRight.position - (new Vector3(1f, 0, 0));
                currentGridPos = teletransportRight.position - (new Vector3(1f, 0, 0));
            }
            else if (other.transform == teletransportRight)
            {
                transform.position = teletransportLeft.position + (new Vector3(1f, 0, 0));
                currentGridPos = teletransportLeft.position + (new Vector3(1f, 0, 0));
            }
        }
    }
}
