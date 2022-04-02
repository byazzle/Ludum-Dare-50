using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public GameObject catsContainer;
    public GameObject catPrefab;
    public GameObject catIconPrefab;

    void Awake()
    {
        // Add some initial cats
        AddCat(new Vector3(3.5f, 2.8f, 0f));
        AddCat();
    }

    public void AddCat()
    {
        AddCat(new Vector3(0f, -4f, 0f)); // Default position is the front door
    }

    public void AddCat(Vector3 position)
    {
        // Add the cat
        GameObject newCat = Instantiate(catPrefab, position, Quaternion.identity);
        newCat.transform.parent = catsContainer.transform;

        // Add an icon to this gameobject
        GameObject newCatIcon = Instantiate(catIconPrefab, Vector3.zero, Quaternion.identity);
        newCatIcon.transform.parent = transform;

    }

    void Update()
    {

    }
}
