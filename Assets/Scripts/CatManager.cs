using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatManager : MonoBehaviour
{
    public GameObject catsContainer;
    public GameObject catIconsContainer;
    public GameObject catPrefab;
    public GameObject catIconPrefab;
    public GameObject friendIconPrefab;

    public static int catCount;
    public static string cat1;
    public static string cat2;

    void Awake()
    {
        catCount = 0;
        cat1 = "";
        cat2 = "";
        // Add some initial cats
        AddCat(new Vector3(2.6f, 2.8f, 0f));

        // Wait a second at the start of the game for the first cat
        StartCoroutine("DeliverFirstCat");
    }

    IEnumerator DeliverFirstCat()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.Play("DoorBell");
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.Play("Mum" + Math.Ceiling(Random.Range(0f, 1f)).ToString());

        yield return new WaitForSeconds(1f);
        AddCat();
    }

    IEnumerator DeliverCat()
    {
        SoundManager.instance.Play("DoorBell");
        yield return new WaitForSeconds(1f);
        AddCat();
    }

    public void AddCat()
    {
        AddCat(new Vector3(0.65f, -4f, 0f)); // Default position is the front door
    }

    public void AddCat(Vector3 position)
    {
        // Add the cat
        GameObject newCat = Instantiate(catPrefab, position, Quaternion.identity);
        CatController newCatController = newCat.GetComponent<CatController>();
        newCat.transform.parent = catsContainer.transform;

        // Add an icon to this gameobject
        GameObject newCatIconGO = Instantiate(catIconPrefab, Vector3.zero, Quaternion.identity);
        newCatIconGO.transform.parent = catIconsContainer.transform;
        Vector3 newPosition = Vector3.zero;
        newPosition.y -= 1f * catIconsContainer.transform.childCount;
        newCatIconGO.transform.localPosition = newPosition;

        // Set the attributes for the icon
        // A bit hacky, but we'll find the stuff by name
        CatIcon newCatIcon = newCatIconGO.GetComponent<CatIcon>();
        newCatIcon.cat = newCat.GetComponent<CatController>();
        newCatIcon.UpdateRender();

        // Add all the existing cats as friends for the new one
        foreach (Transform catIconT in catIconsContainer.transform)
        {
            CatIcon catIcon = catIconT.GetComponent<CatIcon>();
            if (catIcon.cat != newCatController)
            {
                GameObject friendIcon = Instantiate(friendIconPrefab, Vector3.zero, Quaternion.identity);
                friendIcon.GetComponent<CatFriendIcon>().otherCat = catIcon.cat;
                friendIcon.GetComponent<CatFriendIcon>().catManager = this;
                newCatIcon.AddFriendIcon(friendIcon.GetComponent<CatFriendIcon>());
            }
        }

        foreach (Transform catIconT in catIconsContainer.transform)
        {
            CatIcon catIcon = catIconT.GetComponent<CatIcon>();
            if (catIcon.cat != newCatController)
            {
                // Create the new friend icon
                GameObject friendIcon = Instantiate(friendIconPrefab, Vector3.zero, Quaternion.identity);
                friendIcon.GetComponent<CatFriendIcon>().otherCat = newCatController;
                friendIcon.GetComponent<CatFriendIcon>().catManager = this;
                catIcon.AddFriendIcon(friendIcon.GetComponent<CatFriendIcon>());
            }
        }

        // Increment our counter
        catCount++;
    }


    private float newWaveIn = 10f;

    void Update()
    {
        newWaveIn -= Time.deltaTime;
        if (newWaveIn <= 0)
        {
            StartCoroutine("DeliverCat");
            newWaveIn = 10f;
        }
    }
}
