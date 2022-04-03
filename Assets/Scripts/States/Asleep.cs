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
        _controller.animator.SetBool("isAsleep", true);
    }

    public void OnExit()
    {
        _controller.sleepiness = 0;
        _controller.animator.SetBool("isAsleep", false);
    }


}