using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characters;
    public GoldManager goldManager;

    void Start()
    {

     
        UpdateCharacterButtonsInteractivity();
    }

    public void UnlockCharacter(int characterIndex)
    {
        CharacterData character = characters[characterIndex];

        if (!character.isUnlocked && goldManager.money >= character.cost)
        {
            goldManager.SpendGold(character.cost);
            Debug.Log("Karakter açıldı: " + character.characterName);

            
            PlayerPrefs.SetInt("CharacterUnlocked_" + characterIndex, 1);

            
            character.Button.gameObject.SetActive(false);
            character.kilit.enabled = false;
            character.panel.SetActive(false);
            PlayerPrefs.SetInt("ButtonVisible_" + characterIndex, 0);
            PlayerPrefs.SetInt("KilitEnabled_" + characterIndex, 0);
            PlayerPrefs.SetInt("PanelActive_" + characterIndex, 0);

            PlayerPrefs.Save(); 

            UpdateCharacterButtonsInteractivity();
        }
        else
        {
            Debug.Log("Karakter zaten açık veya yeterli para yok.");
        }
    }

    private void UpdateCharacterButtonsInteractivity()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            int isUnlocked = PlayerPrefs.GetInt("CharacterUnlocked_" + i, 0);
            characters[i].isUnlocked = (isUnlocked == 1);

            
            int buttonVisible = PlayerPrefs.GetInt("ButtonVisible_" + i, 1);
            characters[i].Button.gameObject.SetActive(buttonVisible == 1);

            int kilitEnabled = PlayerPrefs.GetInt("KilitEnabled_" + i, 1);
            characters[i].kilit.enabled = (kilitEnabled == 1);

            int panelActive = PlayerPrefs.GetInt("PanelActive_" + i, 1);
            characters[i].panel.SetActive(panelActive == 1);
        }
    }
}
