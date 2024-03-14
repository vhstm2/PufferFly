using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentManager : MonoSington<ComponentManager>
{
   
   public GameManager gameManager;
   public StateMachine machine;
   public Puffer pufferPlayer;
   public GameObject GameStartPanel;
   public GameObject GameEndPanel;
   public pipePoolingGenarator pipePooling;
   public Text GameScoreText;
   public TextMeshProUGUI GameEndGameScoreText;
   public TextMeshProUGUI GameBestScoreText;
   public SaveLoad gameSaveLoad;
   public SoundManager soundManager;
   public AdMob admobManager;


   public int GameCount;

    public float admobHeight;


    private void Start()
    {
        if (admobManager != null) admobManager = FindObjectOfType<AdMob>();
    }
}
