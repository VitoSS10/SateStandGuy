using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int maxCustomer;
    private int customerCount;
    public int servedCustomer;
    public int customerLoss;
    public int maxCustomerLoss;
    int targetCustomer;
    public int customerWaitingTime;
    public float timeInterval;
    private float lastSpawnedTime = 2.0f;
    public int randomIndex;
    bool isWin;
    public Text servedText;
    public Text lossText;
    MenuManager mm;

    // Start is called before the first frame update
    void Start()
    {
        mm = MenuManager.instance;
        targetCustomer = maxCustomer - maxCustomerLoss;
        UpdateServedText();
        UpdateLossText();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWin)
        {
            return;
        }

        lastSpawnedTime+=Time.deltaTime;

        if(lastSpawnedTime >= timeInterval && customerCount < maxCustomer)
        {
            SpawnCustomer();
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

    public void CheckServedCustomer()
    {
        // if(customerCount == maxCustomer)
        // {
        //     if(servedCustomer == maxCustomer)
        //     {
        //         Debug.Log("You Win");
        //         isWin = true;
        //     }

        //     if(servedCustomer < maxCustomer )
        //     {
        //         Debug.Log("You Lose");
        //         isWin = false;
        //         return;
        //     }
        // }

        if(customerLoss == maxCustomerLoss)
        {
            StartCoroutine(Lose());
        }
        
        if(servedCustomer == targetCustomer)
        {
            StartCoroutine(Win());
        }
    }

    public void UpdateServedText()
    {
        servedText.text = servedCustomer.ToString() + " / " + targetCustomer.ToString();
    }

    public void UpdateLossText()
    {
        lossText.text = customerLoss.ToString() + " / " + maxCustomerLoss.ToString();
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("You Win");
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("You Lose");
        yield return new WaitForSeconds(1.0F);
        Time.timeScale = 0;
    }
}
