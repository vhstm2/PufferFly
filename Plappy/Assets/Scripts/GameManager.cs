using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using NativeShareNamespace;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public State GameState = State.None;
    private long gameScoreCount;
    public long GameScoreCount
    {
        get{return gameScoreCount;}
        set
        {
            gameScoreCount = value;
            ComponentManager.instance.GameScoreText.text = gameScoreCount.ToString();
            ComponentManager.instance.GameEndGameScoreText.text = gameScoreCount.ToString();
        }
    }
    
    private void Awake() 
    {
        Screen.SetResolution(1080,1920,true);
        ComponentManager.instance.GameBestScoreText.text = 
        ComponentManager.instance.gameSaveLoad.LoadUpLoad().ToString();

        ComponentManager.instance.machine.ChangeState(State.Main);    
    }


    public void GameStart()
    {
        ComponentManager.instance.soundManager.SoundOneShot("buttonclick");
        ComponentManager.instance.machine.ChangeState(State.Game);
    }

    public void GameRestart()
    {
        ComponentManager.instance.soundManager.SoundOneShot("buttonclick");
        ComponentManager.instance.pufferPlayer.transform.position =
        ComponentManager.instance.pufferPlayer.StartPosition; 
        ComponentManager.instance.machine.ChangeState(State.Game);
    }

    public void Share()
    {
        ComponentManager.instance.soundManager.SoundOneShot("buttonclick");
        StartCoroutine(TakeScreenshotAndShare());
    }
    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare()
        .SetSubject("Putter Ply")
        .SetText("가볍게 즐기는 복어의 여행")
        .SetUrl("https://play.google.com/store/apps/details?id=com.BIGPIXEL.PufferFly")
        .Share();


    }


    public void GameCount()
    {
        GameScoreCount += 1;
        ComponentManager.instance.soundManager.SoundOneShot("complete");
        ComponentManager.instance.gameSaveLoad.SaveUpLoad(
            GameScoreCount,  ComponentManager.instance.GameBestScoreText
            );
    }

    public void GameScore()
    {
        /* 
            = 초기 게임 시작시 저장된 데이터가 없으면 -1 저장
            1. 저장된 점수가 없으면 지금 내점수가 베스트
            2. 저장된 점수가 있으면 지금 베스트점수와 내 점수를 비교해서 내점수가 높으면 베스트로 올림.
            3. 저장된 점수가 있으면 지금 베스트점수와 내 점수를 비교해서 내점수가 낮으면 베스트는 그대로 놔둠.
        */

    }

    public void GameOverIsMain()
    {
        ComponentManager.instance.machine.ChangeState(State.Main);
    }


    public void GamePanelOnOff(State state)
    {
        if (state == State.Main)
        {
            ComponentManager.instance.GameStartPanel.SetActive(true);
            ComponentManager.instance.GameEndPanel.SetActive(false);
        }

        if(state == State.Game)
        {
            ComponentManager.instance.GameStartPanel.SetActive(false);      
            ComponentManager.instance.GameEndPanel.SetActive(false);
        }  
        if(state == State.GameEnd)
        {
            ComponentManager.instance.GameStartPanel.SetActive(false);
            ComponentManager.instance.GameEndPanel.SetActive(true);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        //if (Input.GetKeyDown(KeyCode.Home)) { 홈버튼   }
        //if (Input.GetKeyDown(KeyCode.Menu)) { 메뉴 버튼}

        
    }



}
