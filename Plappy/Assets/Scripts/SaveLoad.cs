using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SaveLoad : MonoBehaviour {
    
    public void SaveUpLoad(long data , TextMeshProUGUI text)
    {
        if(ES3.FileExists("myFile.es3"))
        {
            var longData = ES3.Load<long>("ReaderBoard");
            //새로 얻은 데이터와 저장되어있는 데이터와 비교 
            if(data <= longData)
            {
                return;
            }
            else
            {
                ES3.Save<long>("ReaderBoard",data);
               
                text.text = data.ToString();
                return;
            }
        }
        
        ES3.Save<long>("ReaderBoard",data);
       
        text.text = data.ToString();
    }

    public long LoadUpLoad()
    {
        if(ES3.KeyExists("ReaderBoard")) { return ES3.Load<long>("ReaderBoard"); }
        
        return 0;
    }


   
}
