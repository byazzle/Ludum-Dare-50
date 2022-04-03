using System.Linq;
using UnityEngine;

internal class PickedUp : IState
{
    private readonly CatController _controller;


    public PickedUp(CatController _controller)
    {
        this._controller = _controller;
    }

    public void Tick()
    {
    }

    public void OnEnter()
    {
        _controller.animator.SetBool("isPickedUp", true);
        _controller.renderer.sortingOrder = 20;

        // Disable our rigid body
        _controller.rb.isKinematic = true;
    }

    public void OnExit()
    {
        _controller.animator.SetBool("isPickedUp", false);
        _controller.renderer.sortingOrder = 0;

        // Reenable our rigid body
        _controller.rb.isKinematic = false;

    }


}