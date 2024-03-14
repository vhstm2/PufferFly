using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipePoolingGenarator : MonoBehaviour
{

    [SerializeField] private int length;

    public GameObject[] pipes;
  
    [Tooltip("생성 위치 X")]
    public Transform genaratorPosition;

    private List<Queue<Transform>> queueList = new List<Queue<Transform>>();
   
    public Dictionary<int , Queue<Transform>> pipeDictionary = 
                    new Dictionary<int, Queue<Transform>>();

   

    void Awake()
    {
        pipeListQueue();
        for (int i = 0; i < queueList.Count; i++)
        {
            pipeDictionary.Add(i,queueList[i]); 
        }
    }



    private void pipeListQueue()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            GameObject pipeObject = new GameObject("pipe"+ i);
            pipeObject.transform.SetParent(transform);
            Queue<Transform> queue = new Queue<Transform>();
            for (int j = 0; j < length; j++)
            {
                GameObject obj = Instantiate<GameObject>(pipes[i],pipeObject.transform);
                obj.gameObject.SetActive(false);
                queue.Enqueue(obj.transform);
            }
            queueList.Add(queue);
        }

    }



    

    private Queue<Transform> GetpipeQueue(int pipeNumber) // 0 ~ 3
    {
        return  pipeDictionary[pipeNumber];
    }

    public GameObject Getpipe(int pipeNumber)   //0 ~ 3
    {
        if(ComponentManager.instance.gameManager.GameState == State.GameEnd) return null;

        var queue = GetpipeQueue(pipeNumber);
        GameObject obj = queue.Dequeue().gameObject;
        
        obj.transform.position = 
                new Vector3(genaratorPosition.position.x , Random.Range(-1.0f,2.5f),0);
        obj.gameObject.SetActive(true); 
        queue.Enqueue(obj.transform);
        return obj; 
    }

    public void Setpipe(int pipeNumber , GameObject obj)
    {
        var queue = GetpipeQueue(pipeNumber);
        obj.SetActive(false);
        queue.Enqueue(obj.transform);
    }
}
