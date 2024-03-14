using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slicebada : MonoBehaviour
{
    
    void Update()
    {
      //  if(ComponentManager.instance.gameManager.GameState == State.Game)
        {
            transform.Translate(Vector3.left * Time.deltaTime);

            if(transform.position.x < -15.12f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + (15.12f)*3,transform.localPosition.y,0);
            }

        }
    }
}
