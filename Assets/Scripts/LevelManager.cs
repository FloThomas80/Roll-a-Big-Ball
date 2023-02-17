using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ball")
        {
            WinSequence();
        }
    }


    private void WinSequence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
