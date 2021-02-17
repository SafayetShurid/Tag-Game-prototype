using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{

    public Transform target;


    void Start()
    {
        target = GameManager.instance.currentPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            float x = Mathf.Clamp(target.position.x, -8.6f, -1.2f);
            float y = Mathf.Clamp(target.position.y, -12f, 14.8f);
            //transform.position = new Vector3(x, y, -10f);
            Vector3 targetPos = new Vector3(x,y, -10);
            transform.position = Vector3.Lerp(transform.position, targetPos, 5 * Time.deltaTime);
        }
        else
        {
            if(GameManager.instance.currentPlayer!=null)
            {
                target = GameManager.instance.currentPlayer.transform;
            }
          
        }
       

    }
}
