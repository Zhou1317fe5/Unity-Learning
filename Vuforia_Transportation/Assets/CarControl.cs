using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        transform.Translate(Vector3.forward * 5 * Time.deltaTime);//每秒向前走5m
       

    }
}
