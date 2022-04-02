using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCat : MonoBehaviour, IDrag
{
    public bool isDragged = false;

    public void onEndDrag()
    {
        isDragged = false;
    }

    public void onStartDrag()
    {
        isDragged = true;
    }
}
