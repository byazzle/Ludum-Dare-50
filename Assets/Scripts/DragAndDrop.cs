using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;
    private float _mouseDragSpeed = .01f;

    private Camera _cam;
    private Vector2 velocity;
    void Awake()
    {
        _cam = Camera.main;
    }
    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }
    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<IDrag>() != null)
        {
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.GetComponent<IDrag>().onStartDrag();
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        // Disable the rigid body if it has one
        if (rb != null)
            rb.isKinematic = true;

        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            // if (rb != null)
            // {
            // Vector2 direction = clickedObject.transform.position;
            // rb.velocity = direction * .1f;
            // yield return new WaitForFixedUpdate();
            // }
            // else
            // {
            clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, ray.GetPoint(clickedObject.transform.position.z), ref velocity, _mouseDragSpeed);
            yield return null;
            // }
        }
        clickedObject.GetComponent<IDrag>().onEndDrag();
        // Re-enable the rigid body
        if (rb != null)
            rb.isKinematic = false;
    }
}
