using UnityEngine;
using UnityEngine.UI;

public class CharacterIndex : MonoBehaviour
{
    public int characterIndex; 

    public void OnButtonClick()
    {
        CharacterManager characterManager = FindObjectOfType<CharacterManager>(); 
        characterManager.UnlockCharacter(characterIndex); 

    }

}