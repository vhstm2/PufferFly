using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;

public class GoogleLogin : MonoBehaviour
{
    private void Start()
    {
        GoogleLg();
    }

    public void GoogleLg()
    {
        //1번째 X 
        Social.localUser.Authenticate((success) =>
        { 
            // handle results
            if (success)
            {
                Debug.Log("로그인 성공");
            }
            else
            {
                Debug.Log("로그인 실패");
            }

        });

    }

    public void LeaderboardHub()
    {
        ComponentManager.instance.soundManager.SoundOneShot("buttonclick");

        if (Social.localUser.authenticated)
            Social.ShowLeaderboardUI();
        else
        {
            Social.localUser.Authenticate((result) => 
            {
               if (result)
               {
                   //로그인됨.
                   Social.ShowLeaderboardUI();
               }

           });
        }
    }

    public static void LeaderBoardScoreUpLoad()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(
                ComponentManager.instance.gameManager.GameScoreCount, GPGSIds.leaderboard_putterply_score,
                (sussess) =>
                {
                    if (sussess)
                    {
                        Debug.Log("리더보드에 점수업로드");
                    }
                });
        }
    }

   

    public void OnLogOut()
    {
        //로그아웃..
        //Social..SignOut();
           
    }
}
