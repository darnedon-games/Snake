//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Food foodPrefab;
    private ObjectPool<Food> foodPool;

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
            int randX = Random.Range(-15, 15);
            int randY = Random.Range(-7, 7);
            //Vector3 randomPoint = new Vector3(randX + 0.5f, randY + 0.5f, 0f);
            Vector3 randomPoint = new Vector3(randX, randY, 0f);
            Food copy = foodPool.Get();
            copy.transform.position = randomPoint;
            yield return new WaitForSeconds(2f);
        }

    }
}
