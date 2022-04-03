using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatFriendIcon : MonoBehaviour
{
    public CatManager catManager;
    public CatController thisCat;
    public CatController otherCat;

    public Image catFace;
    public Image bar;

    public Color fullBarColor;

    private bool _barIsFull = false;

    // Update is called once per frame
    void Update()
    {
        // Set the cat's colour
        if (otherCat != null && catFace != null)
        {
            catFace.color = otherCat.catColor;
        }

        // Update the fill bar
        if (!_barIsFull)
        {
            if (thisCat.friends.ContainsKey(otherCat))
            {
                bar.fillAmount = thisCat.friends[otherCat] / 10;
                if (bar.fillAmount >= 1)
                {
                    SetBarFull();
                }
            }
            else
            {
                bar.fillAmount = 0;
            }
        }

    }
    private void SetBarFull()
    {
        _barIsFull = true;
        StartCoroutine("PulseBar");

    }

    IEnumerator PulseBar()
    {
        Vector3 startScale = new Vector3(.6f, .6f, .6f);
        Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        float progress = 0;

        while (progress <= .2f)
        {
            bar.transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        bar.color = fullBarColor;

        progress = .5f;
        while (progress >= 0)
        {
            bar.transform.localScale = Vector3.Lerp(targetScale, startScale, progress);
            progress -= Time.deltaTime;
            yield return null;
        }
        bar.transform.localScale = startScale;
    }
}
