using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Debug.Log("There is another menu manager");
            return;
        }
    }

    public GameObject cookButton;
    public GameObject sendButton;
    public GameObject ingredientContainer;
    public GameObject cookingText;
    [SerializeField] Ingredients ingredientsScript;
    public Menu[] menus;
    public int[] availableOrderID;
    public GameObject[] customersContainer;
    public GameObject[] customers;
    private Ingredient ingredient;
    public GameObject[] ingredientsImage;
    private int count;
    private int ingredientsValue;
    private int ingredientID;
    int tempCategoryID = 0;
    int tempIndex;
    CustomerSpawner spawner;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Daging",0);
        PlayerPrefs.SetInt("Bumbu",0);

        spawner = GetComponent<CustomerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMenuID(int id)
    {
        if(!ingredient.isAdded)
        {
            if(ingredient.categoryID != tempCategoryID)
            {
                if(ingredient.categoryID == 1 || ingredient.categoryID == 2)
                {
                    if(PlayerPrefs.GetInt("Daging") != 1 || PlayerPrefs.GetInt("Bumbu") != 1)
                    {
                        ingredientsValue+=id;
                        Debug.Log(ingredientsValue);
                    }
                    else
                    {
                        if(ingredient.categoryID == 1)
                        {
                            Debug.Log("Telah Ada Daging");
                        }   
                        else if(ingredient.categoryID == 2)
                        {
                            Debug.Log("Telah Ada Bumbu");
                        } 
                    }
                }
                else 
                {
                    ingredientsValue+=id;
                    Debug.Log(ingredientsValue);
                }
                
                if(ingredient.categoryID == 1)
                {
                    PlayerPrefs.SetInt("Daging",1);
                }
                else if(ingredient.categoryID == 2)
                {
                    PlayerPrefs.SetInt("Bumbu",1);
                }
            }
            else if(ingredient.categoryID == tempCategoryID)
            {
                if(ingredient.categoryID == 1)
                {
                    Debug.Log("Telah Ada Daging");
                }   
                else if(ingredient.categoryID == 2)
                {
                    Debug.Log("Telah Ada Bumbu");
                } 
                else if(ingredient.categoryID == 3)
                {
                    Debug.Log("Telah Ada Tambahan");
                } 
                else if(ingredient.categoryID == 4)
                {
                    Debug.Log("Telah Ada Minuman");
                } 
            }
        }
    }

    public void ActivateIngredientsView(Image buttonImage)
    {
        ingredientContainer.SetActive(true);

        if(count + 1 > ingredientsImage.Length)
        {
            Debug.Log("Cannot Add Anymore Ingredients");
        }
        else
        {
            if(!ingredient.isAdded)
            {
                if(ingredient.categoryID != tempCategoryID)
                {
                    if(ingredient.categoryID == 1)
                    {
                        if(PlayerPrefs.GetInt("Daging") != 1  || PlayerPrefs.GetInt("Bumbu") != 1)
                        {
                            ingredientsImage[count].SetActive(true);
                            ingredientsImage[count].GetComponentInChildren<Image>().sprite = buttonImage.sprite;

                            ingredient.isAdded = true;
                            tempCategoryID = ingredient.categoryID;
                            count++;
                        }
                    }
                    else
                    {
                        ingredientsImage[count].SetActive(true);
                        ingredientsImage[count].GetComponentInChildren<Image>().sprite = buttonImage.sprite;

                        ingredient.isAdded = true;
                        tempCategoryID = ingredient.categoryID;
                        count++;
                    }
                }
                
            }
        }
    }

    public void CheckMenu()
    {
        // Debug.Log("ingredientsValue" + ingredientsValue);
        for(int i=0;i<menus.Length;i++)
        {
            if(ingredientsValue == menus[i].id)
            {
                // Debug.Log(menus[i].id);
                //Do Something
                // Debug.Log(menus[i].cookTime);
                // Debug.Log(menus[i].name);
                StartCoroutine(Cook(menus[i].cookTime));
                return;
            }
            else if(ingredientsValue != menus[i].id)
            {
                //Kalau tidak ada , kerjain apa 
                Debug.Log("Menu Not Found !");
            }
        }
    }

    IEnumerator Cook(float cookTime)
    {
        Debug.Log("Cooking...");
        cookButton.SetActive(false);
        count = 0;
        
        for(int i=0;i<ingredientsImage.Length;i++)
        {
            ingredientsImage[i].SetActive(false);
        }

        ingredientContainer.SetActive(false);
        cookingText.SetActive(true);
        yield return new WaitForSeconds(cookTime);
        Debug.Log("Cooking Complete");
        sendButton.SetActive(true);
        cookingText.SetActive(false);
    }

    public void DeleteIngredients()
    {
        Debug.Log("Reset");
        sendButton.SetActive(false);
        cookButton.SetActive(true);
        ingredientsValue = 0;
        count = 0;
        tempCategoryID = 0;
        PlayerPrefs.SetInt("Daging",0);
        PlayerPrefs.SetInt("Bumbu",0);
        
        for(int i=0;i<ingredientsImage.Length;i++)
        {
            ingredientsImage[i].SetActive(false);
        }

        ingredientContainer.SetActive(false);

        ingredientsScript.DeleteAllIngredients();
    }

    public void AddIngredients(Ingredient _ingredient)
    {
        ingredient = _ingredient;
    }

    public void SearchCustomer()
    {
        for(int i=0;i<customersContainer.Length;i++)
        {
            if(customersContainer[i].GetComponentInChildren<Customer>() == null)
            {
                Debug.Log("No Customer");
                customers[i] = null;
            }
            if(customersContainer[i].GetComponentInChildren<Customer>() != null)
            {
                customers[i] = customersContainer[i].transform.GetChild(0).gameObject;
            }
        }
    }

    // public void SendItem()
    // {
    //     for(int i=0;i<availableOrderID.Length;i++)
    //     {
    //         if(ingredientsValue == availableOrderID[i])
    //         {
    //             Debug.Log("Menu Found");
    //             tempIndex = i;
    //             spawner.servedCustomer++;
    //             Destroy(customers[tempIndex]);
    //             DeleteIngredients();
    //             return;
    //         }
    //     }

    //     Debug.Log("Not Found");
    // }

    public void SendItem(int index)
    {
        if(ingredientsValue == availableOrderID[index] && customers[index] != null)
        {
            Debug.Log("Menu Found"); 
            spawner.servedCustomer++;
            Destroy(customers[index]);
            spawner.UpdateServedText();
            spawner.CheckServedCustomer();
            //Get Currency
            DeleteIngredients();
        }
    }
}
