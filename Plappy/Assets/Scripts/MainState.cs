using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainState : GameStateBase
{
    public MainState(StateMachine machine) : base(machine)    {}
    
    public override void Enter()
    {
        Debug.Log("Main Scene Start");
        ComponentManager.instance.gameManager.GameState = State.Main;
        ComponentManager.instance.gameManager.GamePanelOnOff(State.Main);
        //  ComponentManager.instance.admobManager.LoadBannerAd();
    }

    public override void End()
    {
        Debug.Log("Main Scene End");
    }   
}

