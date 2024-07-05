using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterButton : MonoBehaviour
{
    
    public GameObject characterPrefab;
    
    
    public void OnButtonClick()
    {
        
        SelectionManager.Instance.SetSelectedCharacterPrefab(characterPrefab);
        
    }
}

