using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int posX, posY;
    public GameObject xObject;
    public bool isActive
    {
        get
        {
            return xObject.activeSelf; //Active or Passive ? 
        }
    }

    public void SetCoordinates(int x, int y)
    {
        posX = x;
        posY = y;
        gameObject.name = "(" + x + ") (" + y + ")";
    }

    public void ShowX(){
        xObject.SetActive(true);
        GridController.instance.CheckCell(this);
    }

    public void HideX()
    {
       xObject.SetActive(false);  
    }
   

}
