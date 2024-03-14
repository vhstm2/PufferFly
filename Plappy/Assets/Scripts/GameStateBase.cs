using System;
using UnityEngine;


public class GameStateBase
{
    protected StateMachine machine;
   
    public GameStateBase(StateMachine machine)
    {
        this.machine = machine;
    }

    public virtual void Enter()    {}
    public virtual void UpdateBase()    {}
    public virtual void End()    {}
}