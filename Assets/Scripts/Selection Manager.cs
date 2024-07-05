using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;

public class SelectionManager : MonoBehaviour
{


    public GameObject selectedCharacterPrefab;
    public GameObject selectedArenaPrefab;
    public Text messageText;
    private bool messageShown = false;

    public static SelectionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void SetSelectedCharacterPrefab(GameObject characterPrefab)
    {
        selectedCharacterPrefab = characterPrefab;
    }

    
    public void SetSelectedArenaPrefab(GameObject arenaPrefab)
    {
        selectedArenaPrefab = arenaPrefab;
    }

    
    public void StartFight()
    {
        if (selectedCharacterPrefab != null && selectedArenaPrefab != null)
        {
            
            PlayerPrefs.SetString("SelectedCharacterPrefab", selectedCharacterPrefab.name);
            PlayerPrefs.SetString("SelectedArenaPrefab", selectedArenaPrefab.name);

            
            SceneManager.LoadScene(4);


        }
        else
        {
            Debug.LogError("Lütfen bir karakter ve arena seçin!");

            if (!messageShown)
            {
                // Yazı uyarısını göster
                ShowMessage("If you haven't bought a character, go back and buy one.", 3f);
            }

        }
    }

    void ShowMessage(string message, float duration)
    {
        // Text alanına mesajı yaz
        messageText.text = message;

        // Belirtilen süre boyunca beklet ve sonra mesajı temizle
        StartCoroutine(ClearMessageAfterDelay(duration));
    }

    IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Mesajı temizle
        messageText.text = "";

        // Mesajın gösterildiği bilgisini sıfırla
        messageShown = false;
    }

}
