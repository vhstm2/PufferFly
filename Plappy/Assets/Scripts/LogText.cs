using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class LogText : MonoBehaviour
{
    public IObjectPool<LogText> Ipool;

    public TextMeshProUGUI text;

    public void On(string st)
    {
        text.text = st;

        transform.position = new Vector3(6 , 3);

        transform.DOMove(new Vector3(-6 , 3) , 2).OnComplete(() => { Release(); });
        
    }


    private void Release()
    { 
        Ipool.Release(this);
    }






}
