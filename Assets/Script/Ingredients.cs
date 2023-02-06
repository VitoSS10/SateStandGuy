using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    MenuManager mm;
    public Ingredient[] ingredients;
    
    // Start is called before the first frame update
    void Start()
    {
        mm = MenuManager.instance;
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

    public void DeleteAllIngredients()
    {
        for(int i=0;i<ingredients.Length;i++)
        {
            ingredients[i].isAdded = false;
        }
    }
}
