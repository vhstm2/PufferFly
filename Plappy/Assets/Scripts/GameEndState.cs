using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEndState : GameStateBase
{
    public GameEndState(StateMachine machine) : base(machine){}
    public override void Enter()
    {
        Debug.Log("GameEnd Start");

        

        ComponentManager.instance.gameManager.GameState = State.GameEnd;
        ComponentManager.instance.gameManager.GamePanelOnOff(State.GameEnd);


        

        for (int i = 0; i < ComponentManager.instance.pipePooling.pipeDictionary.Count; i++)
        {
            var queue0 = ComponentManager.instance.pipePooling.pipeDictionary[i];
           
            foreach (var item in queue0)
                if(item.gameObject.activeSelf) item.gameObject.SetActive(false);
        }

       // var queue0 = ComponentManager.instance.pipePooling.pipeDictionary[0];
       // var queue1 = ComponentManager.instance.pipePooling.pipeDictionary[1];

        // foreach (var item in queue0)
        //     if(item.gameObject.activeSelf) item.gameObject.SetActive(false);
        
        // foreach (var item in queue1)
        //     if(item.gameObject.activeSelf) item.gameObject.SetActive(false);
        
        Rigidbody2D rd = ComponentManager.instance.pufferPlayer.PlayerRD;
        rd.bodyType = RigidbodyType2D.Kinematic;
    }
   
    public override void End()
    {
        Debug.Log("GameEnd End");
    }   
}

