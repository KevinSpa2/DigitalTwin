using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    private string[] shelfArray;

    public int ArraySize = 10;

    private void Start()
    {
        shelfArray = new string[ArraySize];
    }

    // Ddd a value to the array, check for duplicates first
    public void AddValue(string valueToAdd)
    {

        if (SearchValueFromString(valueToAdd))
        {
            // Debug.Log($"Value '{valueToAdd}' already exists in the array. Not adding duplicate.");
            return;
        }

        for (int i = 0; i < shelfArray.Length; i++)
        {
            if (shelfArray[i] == null) 
            {
                shelfArray[i] = valueToAdd;
                // Debug.Log($"Added value '{valueToAdd}' at index {i}.");
                return; 
            }
        }
    }

    // Remove a value from the array
    public void RemoveValue(string valueToRemove)
    {
        for (int i = 0; i < shelfArray.Length; i++)
        {
            if (shelfArray[i] == valueToRemove)
            {
                shelfArray[i] = null; 
                return;
            }
        }
    }

    // Display all values in the array
    public void DisplayValues()
    {
        // Debug.Log("Values in the array:");
        for (int i = 0; i < shelfArray.Length; i++)
        {
            if (shelfArray[i] != null)
            {
                // Debug.Log($"Index {i}: {shelfArray[i]}");
            }
        }
    }

    public bool SearchValueFromString(string value) 
    {
        for (int i = 0; i < shelfArray.Length; i++)
        {
            if (shelfArray[i] == value)
            {
                return true;
            }
        }
        return false;
    }

    // Search for a specific value in the array
    public bool SearchValue(int column, int level)
    {
        string columnValue = string.Empty;

        switch (column)
        {
            case 1:
                columnValue = "A";
                break;
            case 2:
                columnValue = "B";
                break;
            case 3:
                columnValue = "C";
                break;
        }

        string valueToSearch = columnValue + "" + level;
        // Debug.Log("SEARCHING FOR: " + valueToSearch);

        for (int i = 0; i < shelfArray.Length; i++)
        {
            if (shelfArray[i] == valueToSearch)
            {
                // Debug.Log(valueToSearch + " FOUND!");
                return true;
            }
        }
        return false;
    }
}
