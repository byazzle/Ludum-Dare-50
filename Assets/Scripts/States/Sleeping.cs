using System.Linq;
using UnityEngine;

internal class Asleep : IState
{
    private readonly CatController _controller;

    public Asleep(CatController _controller)
    {
        this._controller = _controller;
    }

    public void Tick()
    {
        _controller.sleepiness -= Time.deltaTime;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        _controller.sleepiness = 0;
    }


}