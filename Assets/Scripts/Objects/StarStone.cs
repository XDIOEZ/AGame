using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarStone : MonoBehaviour
{
    
    public GameObject ga;
    
    
    private void Start()
    {
        
        ga.SetActive(false);
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

    //public void FPressed()
    //{
    //    Destroy(ga.gameObject);
    //}







}
