using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonController : MonoBehaviour
{
    public int characterIndex;  
    public Button characterButton;

    void Start()
    {
        CheckCharacterUnlockStatus();
    }

    void CheckCharacterUnlockStatus()
    {
        
        bool isUnlocked = PlayerPrefs.GetInt("CharacterUnlocked_" + characterIndex, 0) == 1;

        
        characterButton.interactable = isUnlocked;
    }
}
