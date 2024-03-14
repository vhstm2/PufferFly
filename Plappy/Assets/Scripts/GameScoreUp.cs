using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreUp : MonoBehaviour
{
    public GameObject Obj;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(ComponentManager.instance.gameManager.GameState == State.GameEnd) return;
        Obj = other.gameObject;
    }

   public void OnTriggerExit2D(Collider2D other) 
   {
        if(ComponentManager.instance.gameManager.GameState == State.GameEnd) return;
        if(Obj == null) return;

        Debug.Log(other.gameObject.name);
        Obj = null;
        ComponentManager.instance.gameManager.GameCount();
        GoogleLogin.LeaderBoardScoreUpLoad();
   }
}