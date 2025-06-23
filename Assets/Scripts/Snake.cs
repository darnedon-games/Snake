using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform segmentPrefab;
    private Vector2 direction;
    private Vector2 lastDirection;
    private List<Transform> segments;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        lastDirection = direction;

        for (int i = segments.Count - 1; i > 0; i--) 
        {
            segments[i].position = segments[i - 1].position;
        }
        transform.Translate(new Vector2(direction.x,direction.y).normalized * speed * Time.deltaTime);
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
