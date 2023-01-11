using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int maxCustomer;
    private int customerCount;
    public int servedCustomer;
    public float timeInterval;
    private float lastSpawnedTime = 0.0f;
    public int randomIndex;
    MenuManager mm;


    // Start is called before the first frame update
    void Start()
    {
        mm = MenuManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawnedTime+=Time.deltaTime;

        if(lastSpawnedTime >= timeInterval && customerCount < maxCustomer)
        {
            SpawnCustomer();
        }

        if(servedCustomer == maxCustomer)
        {
            Debug.Log("You Win");
        }
    }

    void SpawnCustomer()
    {
        int count=0;
        randomIndex = Random.Range(0,mm.customersContainer.Length);
        
        lastSpawnedTime = 0;

        for(int i=0;i<mm.customersContainer.Length;i++)
        {
            if(mm.customers[i] != null)
            {
                count++;
            }
        }

        if(count == 3)
        {
            Debug.Log("Full");
            return;
        }
        else if(count < 3)
        {
            if(mm.customers[randomIndex] == null)
            {
                customerCount++;
                Instantiate(customer,mm.customersContainer[randomIndex].transform);
            }
            else if(mm.customers[randomIndex] != null)
            {
                Debug.Log("There Is Customer There");
                randomIndex = Random.Range(0,mm.customersContainer.Length);
                SpawnCustomer();
            }
        }

        mm.SearchCustomer();
    }
}
