using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{

    public Transform target;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(target.position.x, -31f, 22f);
        float y = Mathf.Clamp(target.position.y, -14f, 20f);

        transform.position = new Vector3(x, y, -10f);

    }
}
