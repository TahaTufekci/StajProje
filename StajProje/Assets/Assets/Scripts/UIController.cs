using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public InputField inputField; // matris boyutu...
    public Button submitButton; // onay butonu...
    
    public GridLayoutGroup gridLayout; //hücrelerin içerisinde olduğu grid...
    public GameObject cellPrefab;
    public void CreateGrid()
    {
        if (int.TryParse(inputField.text, out int gridSize) && gridSize>1) //Kullanıcıdan alınan grid boyutu
        {
            
            GridController.instance.gridSize = gridSize;
            float gridLayoutSize = ((RectTransform)gridLayout.transform).sizeDelta.x; //Grid boyutu 1024...
            float cellSize = gridLayoutSize / gridSize; 
            gridLayout.cellSize =new Vector2(cellSize,cellSize);
            for (int i = 0; i < Mathf.Pow(gridSize,2); i++)
            {
                
                GameObject cellObject= Instantiate(cellPrefab, gridLayout.transform);
                InitializeCell(cellObject);
            }
            ShowGridLayout();
        }
    }


    public void InitializeCell(GameObject cellObject) //Cell e özellikler ver...
    {
        Cell cell = cellObject.GetComponent<Cell>();
        GridController.instance.AddCellToList(cell);
    }

    public void ShowGridLayout()
    {
        inputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        gridLayout.gameObject.SetActive(true);
    }
    
}
