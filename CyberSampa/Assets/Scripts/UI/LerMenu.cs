using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LerMenu : MonoBehaviour
{
    [SerializeField]
    GameObject telaOpcoes;
    [SerializeField]
    GameObject telaCreditos;
    [SerializeField]
    Text titulo;
    [SerializeField]
    Button jogar;
    [SerializeField]
    Button opcao;
    [SerializeField]
    Button creditos;
    [SerializeField]
    Button voltar;

    [SerializeField]
    EventSystem selecao;

    private bool estaAtivo = true;
   
    public void CarregarCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void PainelOpcao()
    {
        if (estaAtivo)
        { 
        jogar.gameObject.SetActive(false);
        creditos.gameObject.SetActive(false);
        opcao.gameObject.SetActive(false);
        titulo.gameObject.SetActive(false);
        
            estaAtivo = false;
        }

        voltar.gameObject.SetActive(true);
        telaOpcoes.SetActive(true);
        selecao.SetSelectedGameObject(voltar.gameObject);
        
    }

    public void PainelCreditos()
    {
        if (estaAtivo)
        {
            jogar.gameObject.SetActive(false);
            creditos.gameObject.SetActive(false);
            opcao.gameObject.SetActive(false);
            titulo.gameObject.SetActive(false);

            estaAtivo = false;
        }

        voltar.gameObject.SetActive(true);
        telaCreditos.SetActive(true);
        selecao.SetSelectedGameObject(voltar.gameObject);
        
    }

    public void Voltar()
    {
        if (estaAtivo == false)
        {
            telaOpcoes.SetActive(false);
            telaCreditos.SetActive(false);
            voltar.gameObject.SetActive(false);

            estaAtivo = true;

        }

        jogar.gameObject.SetActive(true);
        creditos.gameObject.SetActive(true);
        opcao.gameObject.SetActive(true);
        titulo.gameObject.SetActive(true);
        selecao.SetSelectedGameObject(jogar.gameObject);
    }
}
