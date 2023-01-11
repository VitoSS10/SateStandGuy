using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorGenerator : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRandomColor()
    {
        sprite.color = new Color(Random.Range(0.0f,1f),Random.Range(0.0f,1f),Random.Range(0.0f,1f));
    }
}
