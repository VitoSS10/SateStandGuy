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
    private float lastSpawnedTime = 0.0f;
    public int randomIndex;
    bool isWin;
    public Text servedText;
    public Text lossText;
    public GameObject winWindow;
    public GameObject loseWindow;
    MenuManager mm;
    AudioManager am;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        mm = MenuManager.instance;
        am = AudioManager.instance;
        gm = GameManager.instance;
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
                am.Play("Customer Appear");
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
            Lose();
        }
        
        if(servedCustomer == targetCustomer)
        {
            Win();
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

    void Win()
    {
        winWindow.SetActive(true);
        gm.UnlockNextLevel();
        Time.timeScale = 0;
    }

    void Lose()
    {
        loseWindow.SetActive(true);
        Time.timeScale = 0;
    }
}
