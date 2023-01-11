using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    MenuManager mm;
    public Sprite[] customersSprite;
    public Image orderImage;
    public Image customerSprite;
    public float waitingTime;
    CustomerSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<CustomerSpawner>();
        mm = MenuManager.instance;
        int randIndex = Random.Range(0,customersSprite.Length);
        customerSprite.sprite = customersSprite[randIndex];
        GenerateRandomOrder();
        StartCoroutine(waitTime(waitingTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRandomOrder()
    {
        int randomIndex = Random.Range(0,mm.menus.Length);

        int customerIndex = spawner.randomIndex;
        // Debug.Log(randomIndex);
        Debug.Log(customerIndex);
        orderImage.sprite = mm.menus[randomIndex].menuImage;
        mm.availableOrderID[customerIndex] = mm.menus[randomIndex].id;

        if(customerIndex < 2)
        {
            customerIndex++;
        }
        else if(customerIndex == 2)
        {
            customerIndex = 0;
        }
    }

    IEnumerator waitTime(float time)
    {
        Debug.Log("Waiting....");
        yield return new WaitForSeconds(time);
        Debug.Log("Wait For Too Long!");
        Destroy(this.gameObject);
    }
}
