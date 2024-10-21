using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarStone : MonoBehaviour
{
    
    public GameObject ga;
    public Color newColor;
    
    private void Start()
    {
        
        ga.SetActive(false);
    }

    private void Update()
    {
        if (ga == null)
        {
            ChangeColor();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ga != null)
                ga.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(ga!=null)
            ga.SetActive(false);
        }
    }

    void ChangeColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = newColor;
    }

    //public void FPressed()
    //{
    //    Destroy(ga.gameObject);
    //}







}
