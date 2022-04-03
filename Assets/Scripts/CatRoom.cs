using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRoom : MonoBehaviour
{
    public List<CatController> catsInThisRoom;

    private List<CatController> _pickedUpCats;
    void Awake()
    {
        _pickedUpCats = new List<CatController>();
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
            HandleOnEnter(cat);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // Make sure the object is a cat
        CatController cat = other.GetComponent<CatController>();
        if (cat != null)
        {
            if (_pickedUpCats.Contains(cat))
            {
                if (cat.GetCurrentState().ToString() != "PickedUp")
                {
                    if (!catsInThisRoom.Contains(cat))
                    {
                        catsInThisRoom.Add(cat);
                        HandleOnEnter(cat);
                    }
                    _pickedUpCats.Remove(cat);
                }
            }
            else if (cat.GetCurrentState().ToString() == "PickedUp")
            {
                if (!_pickedUpCats.Contains(cat))
                {
                    _pickedUpCats.Add(cat);
                }
                if (catsInThisRoom.Contains(cat))
                {
                    catsInThisRoom.Remove(cat);
                    HandleOnExit(cat);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Make sure the object is a cat
        CatController cat = other.GetComponent<CatController>();
        if (cat != null)
        {
            HandleOnExit(cat);
        }
    }

    private void HandleOnExit(CatController cat)
    {

        // Remove this cat as an adversary for other cats
        foreach (CatController otherCat in catsInThisRoom)
        {
            otherCat.RemoveAdversary(cat);
        }
        Debug.Log(cat.catName + " left the room");
        catsInThisRoom.Remove(cat);
        _pickedUpCats.Remove(cat);
        cat.adversary = null;
    }
    private void HandleOnEnter(CatController cat)
    {

        if (cat.GetCurrentState().ToString() == "PickedUp")
        {
            // If we're holding the cat, store it for later checks
            _pickedUpCats.Add(cat);
        }
        else
        {
            Debug.Log(cat.catName + " entered the room");
            foreach (CatController roomCat in catsInThisRoom)
            {
                // Check if the new cat is NOT friends with this room cat
                if (!cat.IsFriends(roomCat))
                {
                    cat.AddAdversary(roomCat);
                }
                // Check if this roomCat is not friends with the new cat
                if (!roomCat.IsFriends(cat))
                    roomCat.AddAdversary(cat);
            }
            catsInThisRoom.Add(cat);
        }
    }
}