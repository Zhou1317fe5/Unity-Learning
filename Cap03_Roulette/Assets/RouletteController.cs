using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RouletteController : MonoBehaviour
{
    //Ðý×ªËÙ¶È
    float rotSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("game is begin!");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 10;
        }
        transform.Rotate(0, 0, this.rotSpeed *=0.999f);
        if (Input.GetMouseButtonDown(1))
        {
            this.rotSpeed = 0;
        }
    }
}