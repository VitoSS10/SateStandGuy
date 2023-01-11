using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Color selectedColor;
    public ButtonBlueprint[] buttons;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButton(int index)
    {
        buttons[index].isClicked = true;
        
        for(int i=0;i<buttons.Length;i++)
        {
            if(i != index)
            {
                buttons[i].isClicked = false;
            }

            if(buttons[i].isClicked)
            {
                buttons[i].button.GetComponent<Image>().color = selectedColor;
            }
            else if(!buttons[i].isClicked)
            {
                buttons[i].button.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void Reset()
    {
        for(int i=0;i<buttons.Length;i++)
        {
            if(i == 0)
            {
                buttons[i].isClicked = true;
            }
            else if(i > 0)
            {   
                buttons[i].isClicked = false;
            }

            if(buttons[i].isClicked)
            {
                buttons[i].button.GetComponent<Image>().color = selectedColor;
                buttons[i].target.SetActive(true);
            }
            else if(!buttons[i].isClicked)
            {
                buttons[i].button.GetComponent<Image>().color = Color.white;
                buttons[i].target.SetActive(false);
            }
        }
    }
}
