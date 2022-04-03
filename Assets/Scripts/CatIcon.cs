using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIcon : MonoBehaviour
{
    public CatController cat;
    public SpriteRenderer catFace;
    public TMPro.TextMeshProUGUI catName;
    public TMPro.TextMeshProUGUI catNameShadow;
    public GameObject friends;

    // We could do this on Update(), but there's no real need since it doesn't change
    public void UpdateRender()
    {
        catFace.color = cat.catColor;
        catName.text = cat.catName;
        catNameShadow.text = cat.catName;
    }

    public void AddFriendIcon(CatFriendIcon friendIcon)
    {

        friendIcon.GetComponent<CatFriendIcon>().thisCat = cat;
        Vector3 iconPos = Vector3.zero;
        iconPos.x += .6f * (friends.transform.childCount - 1);
        friendIcon.transform.parent = friends.transform;
        friendIcon.transform.localPosition = iconPos;
        friendIcon.transform.localScale = Vector3.one;
    }
}
