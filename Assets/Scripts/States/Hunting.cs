using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

internal class Hunting : IState
{

    private readonly CatController _controller;
    public Hunting(CatController _controller)
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
        SoundManager.instance.Play("Hiss" + Math.Ceiling(Random.Range(0f, 3f)).ToString());
        _controller.ShowWarning(true);
        _controller.targetDestinationSetter.enabled = true;
        _controller.targetDestinationSetter.target = _controller.adversary.transform;
    }

    public void OnExit()
    {
        _controller.ShowWarning(false);
        _controller.targetDestinationSetter.target = null;
        _controller.targetDestinationSetter.enabled = false;

    }


}