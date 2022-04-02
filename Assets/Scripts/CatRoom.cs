using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRoom : MonoBehaviour
{
    public List<CatController> catsInThisRoom;
    void Awake()
    {

    }

    void Update()
    {
        // Every awake cat that's in here likes every other cat a bit more
        foreach (CatController cat in catsInThisRoom)
        {
            string currentState = cat.GetCurrentState().ToString();
            // Don't check if the cat is asleep or picked up
            if (currentState != "PickedUp" && currentState != "Sleeping")
            {
                foreach (CatController otherCat in catsInThisRoom)
                {
                    // Make sure it's not itself and the other cat isn't currently pickedup
                    if (cat != otherCat && currentState != "PickedUp")
                        cat.AddFriendliness(Time.deltaTime, otherCat);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure the object is a cat
        CatController cat = other.GetComponent<CatController>();
        if (cat != null)
        {
            catsInThisRoom.Add(cat);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Make sure the object is a cat
        CatController cat = other.GetComponent<CatController>();
        if (cat != null)
        {
            catsInThisRoom.Remove(cat);
        }
    }
}