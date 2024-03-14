using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class VersionChecker : MonoBehaviour
{
    private const string url = "https://docs.google.com/spreadsheets/d/1xyQEw9lsj1sQq7wcJlKmk8m9koryZbbFGUckpn4EMJ0/export?format=tsv&range=A2";

    public TextMeshProUGUI StoreVersionText;
    public TextMeshProUGUI CurrentVersionText;

    [SerializeField] string currentVersion; //현재버전       0.25   
    [SerializeField] string storeVersion;   //스토어버전     0.25



    IEnumerator onplay()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        storeVersion = www.downloadHandler.text;
        currentVersion = Application.version;


        print(storeVersion);

        if (currentVersion.Contains(storeVersion))
        {
            print("같은버전");
        }
        else
        {
            print("다른버전");
            currentVersion += string.Format("     업데이트 버전이 있습니다.");
        }
        CurrentVersionText.text = "현재 버전 : " + currentVersion;
        StoreVersionText.text = "스토어 버전 : " + storeVersion;
    }

     void Start()
    {
        
    }


    public void OpenUrl(string GameStoreURL)
    {
        Application.OpenURL(GameStoreURL);
    }
    
}
