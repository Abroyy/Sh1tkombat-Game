using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahnedegistir : MonoBehaviour


{   
    ReklamManager reklam;
    

    void Start()
    {
        reklam = GetComponent<ReklamManager>();
    }
    
    
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void KarakterSecimSahnesi()
    {
        SceneManager.LoadScene(2);
    }

    
    public void More()
    {
        SceneManager.LoadScene(3);
    }

    
    public void Sahne1()
    {
        SceneManager.LoadScene(4);
        
    }

    public void Restart()
    {
        
        SceneManager.LoadScene(4);
        reklam.ShowRewardedAd();
        

    }

    public void Champions()
    {
        SceneManager.LoadScene(5);
    }
    
    public void CreateGame()
    {
        SceneManager.LoadScene(6);
    }

    public void Store()
    {
        SceneManager.LoadScene(7);
    }

    public void Quit()
    {
        Application.Quit();
        
    }
}
