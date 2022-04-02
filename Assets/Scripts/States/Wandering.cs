using System.Linq;
using UnityEngine;

internal class Wandering : IState
{

    private readonly CatController _controller;
    public Wandering(CatController _controller)
    {
        this._controller = _controller;
    }

    public void Tick()
    {
        _controller.sleepiness += Time.deltaTime;
        _controller.hunger += Time.deltaTime;
    }


    public void OnEnter()
    {
        _controller._wanderingDestinationSetter.enabled = true;
    }

    public void OnExit()
    {
        _controller._wanderingDestinationSetter.enabled = false;

    }


}