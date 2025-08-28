//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Food foodPrefab;
    private ObjectPool<Food> foodPool;
    private Vector2 randomPoint;
    private Collider2D foodCollider; // Collider antes de spawnear comida

    private void Awake()
    {
        foodPool = new ObjectPool<Food>(CreateFood, TakeFood, LeaveFood);
    }

    private Food CreateFood()
    {
        Food foodCopy = Instantiate(foodPrefab);
        foodCopy.MyFoodPool = foodPool;
        return foodCopy;
    }

    private void TakeFood(Food food)
    {
        food.gameObject.SetActive(true);
    }

    private void LeaveFood(Food food)
    {
        food.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            int randX = Random.Range(-16, 16);
            int randY = Random.Range(-8, 8);
            //Vector3 randomPoint = new Vector3(randX + 0.5f, randY + 0.5f, 0f);
            randomPoint = new Vector2(randX, randY);
            
            foodCollider = ThrowCheck();
            
            if (!foodCollider)
            {
                Food copy = foodPool.Get();
                copy.transform.position = randomPoint;
                yield return new WaitForSeconds(2f);
            }
        }

    }

    private Collider2D ThrowCheck()
    {
        return Physics2D.OverlapCircle(randomPoint, 0.35f);
    }
}
