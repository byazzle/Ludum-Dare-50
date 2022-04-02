using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicGridGenerator : MonoBehaviour
{
    public RectTransform panelRow;
    public GameObject gridCell;

    public Transform grid;

    public int rowSize = 10;
    public int columnSize = 1;

    public void ClearGrid()
    {
        for (int count = 0; count < grid.childCount; count++)
        {
            Destroy(grid.GetChild(count).gameObject);
        }
    }

    public void GenerateGrid()
    {

        ClearGrid();

        GameObject cellInputField;
        RectTransform rowParent;
        for (int rowIndex = 0; rowIndex < rowSize; rowIndex++)
        {
            rowParent = (RectTransform)Instantiate(panelRow);
            rowParent.transform.SetParent(grid);
            rowParent.transform.localScale = Vector3.one;
            for (int colIndex = 0; colIndex < columnSize; colIndex++)
            {
                cellInputField = (GameObject)Instantiate(gridCell);
                cellInputField.transform.SetParent(rowParent);
                // cellInputField.GetComponent().localScale = Vector3.one;        
            }
        }
    }
}