using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMove : MonoBehaviour
{
   
    public int pipeNumber;
   private float speed = 2.3f;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == false) return;

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x <= -10.0f)
        {
            gameObject.SetActive(false);

        // ComponentManager.instance.pipePooling.Setpipe(pipeNumber,gameObject);
        }

    }
}
