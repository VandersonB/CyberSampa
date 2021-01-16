using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemyAll : MonoBehaviour
{
    public string sceneNewLv, sceneMainMenuGame;
    void Start()
    {
        //Congelando tela game
        Time.timeScale = 0;   
    }

    public void btnContinuaHistoria()
    {
        //Descongelando tela game
        Time.timeScale = 1;
        //new_scene
        StartCoroutine(sceneNewLv);
    }

    public void btnExit()
    {   
        //evitar congelamento desnecessario
        Time.timeScale = 1;
        //new_scene
        StartCoroutine(sceneMainMenuGame);
    }
}
