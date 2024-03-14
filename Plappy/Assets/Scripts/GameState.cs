using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameState : GameStateBase
{
   
    private float timer;
    private float maxTime = 1.9f;

    public GameState(StateMachine machine) : base(machine)    {}
    
    public override void Enter()
    {
        Debug.Log("Game Scene Start");

        var tex = TextLogManager.Instance.Pool.Get();
        tex.On("플레이 시작");
        //스코어 초기화 
        // 게임스테이트 변경 Game
        ComponentManager.instance.gameManager.GameScoreCount = 0;
        ComponentManager.instance.gameManager.GameState = State.Game;

        //게임스타트 패널 지우기..
        ComponentManager.instance.gameManager.GamePanelOnOff(State.Game);

        //플레이어 리지드바디 중력 작용시키기
        Rigidbody2D rd = ComponentManager.instance.pufferPlayer.PlayerRD;
        rd.bodyType = RigidbodyType2D.Dynamic;
    }
   
    public override void UpdateBase()
    {
        //타이머 계산중
        timer += Time.deltaTime;

        //쿨타임
        if(timer >= maxTime)
        {
            //파이프 가져오기 낡은파이프 / 새파이프 
            var ob =  ComponentManager.instance.pipePooling.Getpipe(UnityEngine.Random.Range(0,4));
            if(ob != null)
            {
                //가져온 파이프 세팅
                ob.gameObject.SetActive(true);
                maxTime = UnityEngine.Random.Range(1.7f,2.0f);
                timer = 0;
            }
        }
    }

    public override void End()
    {
        Debug.Log("Game Scene End");
        ComponentManager.instance.GameCount += 1;
        if(ComponentManager.instance.GameCount >= 5) 
        {
            ComponentManager.instance.GameCount = 0;
            ComponentManager.instance.admobManager.UserChoseToWatchAd();
        }
        else
        {
              //게임오버시 위치 초기화 / 엔딩사운드
            ComponentManager.instance.pufferPlayer.transform.position = ComponentManager.instance.pufferPlayer.StartPosition; 
            ComponentManager.instance.soundManager.SoundOneShot("ending");
        }
    }   
}

