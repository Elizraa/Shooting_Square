using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        level = int.Parse(this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoLevel()
    {
        SceneManager.LoadScene(level);
    }
}
