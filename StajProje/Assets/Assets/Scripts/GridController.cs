using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    public List<Cell> controlList = new List<Cell>(); //List of objects to check the neighbours..
    public HashSet<Cell> activeCellList = new HashSet<Cell>(); //List of Xs...
    public int controlIndex = 0; // Index of the cell that should check the neighbours..
    
    public int gridSize; // 5x5,4x4,3x3...
    public List<Cell> cells=new List<Cell>(); //List of all cells that we created...
    public void Start()
    {
        instance = this;
    }

    public void AddCellToList(Cell cell)
    {
        cells.Add(cell);
        Button button = cell.GetComponent<Button>();
        button.onClick.AddListener(()=> cell.ShowX());

        //3,0
        int x = (cells.Count-1) % gridSize;
        int y = (cells.Count-1) / gridSize;
        cell.SetCoordinates(x,y);
    }

    public Cell GetCell(int x, int y)
    {
        int index = (y * gridSize) + x;
        if (index >= 0 && index < cells.Count)
        {
            return cells[index];
        }
        else
        {
            return null;
        }
    }

       public void CheckCell(Cell cell) //Control start function...
    {
        controlList.Clear();
        activeCellList.Clear();
        controlIndex = 0;
        
        controlList.Add(cell);
        CheckGrid(); //Keep checking...
    }

    public void CheckGrid()
    {
        Cell cell = controlList[controlIndex];
        CheckNeighbours(cell);

        // The cell finished its checking...
        controlIndex++; // Go to the next cell
        if (controlIndex < controlList.Count) 
        {
            CheckGrid();
        }


        if (activeCellList.Count >= 3)
        {
            StartCoroutine(waitThenLoad());// Giving delay for seeing all the Xs
            
        }
    }
     public IEnumerator waitThenLoad(){
        yield return new WaitForSeconds(0.1f);    
         foreach (Cell activeCell in activeCellList)
            {
            
                activeCell.HideX();
            }

    }
  
    
    public void CheckNeighbours(Cell cell) //Checking neighbours...
    {
        if (!activeCellList.Contains(cell))
        {
            activeCellList.Add(cell);
        }
        
        for (int i = -1; i <= 1; i=i+2)
        {
            int targetPosX = cell.posX + i;
            Cell targetCell = GetCell(targetPosX, cell.posY);
            if (targetCell != null && targetCell.isActive)
            {
                
                activeCellList.Add(targetCell);
                

                if (!controlList.Contains(targetCell))
                {
                    controlList.Add(targetCell);
                }
            }
        }

        for (int i = -1; i <= 1; i=i+2)
        {
            int targetPosY = cell.posY + i; 
            Cell targetCell = GetCell(cell.posX, targetPosY);
            if (targetCell != null && targetCell.isActive)
            {
                 activeCellList.Add(targetCell);
                
                if (!controlList.Contains(targetCell))
                {
                    controlList.Add(targetCell);
                }
            }
        }

    }
    
   
}
