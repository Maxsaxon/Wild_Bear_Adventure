using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Selector : MonoBehaviour
{
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene("level " + level.ToString()); // autofill the name of the scene based on defined var
    }
}
