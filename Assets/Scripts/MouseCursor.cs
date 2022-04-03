using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public Sprite mouseDown;
    public Sprite mouseUp;

    void Start()
    {
        Cursor.visible = false;
        _renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        // Some old input shit
        if (Input.GetMouseButtonDown(0))
        {
            _renderer.sprite = mouseDown;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _renderer.sprite = mouseUp;
        }
    }
}
