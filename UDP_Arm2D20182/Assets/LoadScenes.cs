using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 使用LoadScene时需要引入
public class LoadScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConfigToGame()
    { 
        SceneManager.LoadScene("GameScene"); 
    }
    public void GameToConfig()
    { 
        SceneManager.LoadScene("UDPConfig"); 
    }
}
