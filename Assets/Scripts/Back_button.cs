using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void Return_back()
    {
        SceneManager.LoadScene("StartButtonScene1");
    }
}
