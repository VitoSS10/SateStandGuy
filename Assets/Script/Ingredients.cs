using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredients : MonoBehaviour
{
    MenuManager mm;
    public Ingredient[] ingredients;
    public Text[] ingredientCostText;
    
    // Start is called before the first frame update
    void Start()
    {
        mm = MenuManager.instance;
        ShowIngredientsCost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(int idx)
    {
        mm.AddIngredients(ingredients[idx]);
        // if(ingredients[idx].isAdded == true)
        // {
        //     return;
        // }
        // else if(ingredients[idx].isAdded == false)
        // {
        //     ingredients[idx].isAdded = true;
        // }
    }

    public void ShowIngredientsCost()
    {
        for(int i=0;i<ingredients.Length;i++)
        {
            ingredientCostText[i].text = ingredients[i].cost.ToString();
        }
    }

    public void DeleteAllIngredients()
    {
        for(int i=0;i<ingredients.Length;i++)
        {
            ingredients[i].isAdded = false;
        }
    }
}
