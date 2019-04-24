using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

    //Instancia de clase statica de tipo Singleton donde es creada una unica vez y puede ser accesible desde cualquier parte, ante un cambio de escena sigue existiendo la misma instancia, no se destruye

    public GameObject player;

    public LevelController levelController;
    public UIController uiController;
    public GameObject optionMenu;
    public static GameController instance;
    private int m_coins = 0;

    public void StartGame()
    {
        levelController.Initialize();
    }

    private void Awake()
    {
        instance = this;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void Dead()
    {
        optionMenu.SetActive(true);
    }

	
	void Update () {
		
	}
    
    public void AddCoins()
    {
        m_coins++;
        uiController.CoinsUpdate(m_coins);
    }
    
}
