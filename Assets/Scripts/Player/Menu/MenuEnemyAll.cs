using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEnemyAll : MonoBehaviour
{
    public GameObject menuObj;
    public string scenaNameLV, scenaNameMenuJogo;

    private void Start()
    {
        //Congela_GAME
        Time.timeScale = 0;
    }
    public void btnContinue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scenaNameLV);
    }

    public void btnExit()
    {
        SceneManager.LoadScene(scenaNameMenuJogo);
    }
}
