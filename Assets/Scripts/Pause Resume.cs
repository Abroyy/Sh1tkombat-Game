using UnityEngine;

public class PauseResume : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pause;

    
    private void OnDisable()
    {
        
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0; 
        pausePanel.SetActive(true); 
        pause.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; 
        pausePanel.SetActive(false); 
        pause.SetActive(true);
    }
}
