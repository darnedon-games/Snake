using UnityEngine;
using UnityEngine.Pool;

public class Food : MonoBehaviour
{
    public ObjectPool<Food> MyFoodPool { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            MyFoodPool.Release(this);
        }
    }
}
