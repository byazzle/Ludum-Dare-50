using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VariableManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI fightText;
    public TMPro.TextMeshProUGUI fightTextShadow;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI textShadow;
    void Start()
    {
        string score = "You had " + CatManager.catCount.ToString() + " cats!";
        text.text = score;
        textShadow.text = score;

        string fight = CatManager.cat1 + " and " + CatManager.cat2 + " ";

        int rand = (int)Math.Ceiling(Random.Range(0f, 5f));
        if (rand == 1)
        {
            fight += "got into a fight";
        }
        else if (rand == 2)
        {
            fight += "were naughty cats";
        }
        else if (rand == 3)
        {
            fight += "had an altercation";
        }
        else if (rand == 3)
        {
            fight += "didn't get along well";
        }
        else if (rand == 4)
        {
            fight += "should chill out a bit";
        }
        else if (rand == 5)
        {
            fight += "need to relax";
        }
        else
        {
            fight += "can do better";
        }
        fightText.text = fight;
        fightTextShadow.text = fight;
    }
}
