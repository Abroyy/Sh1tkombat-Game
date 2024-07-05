using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaButton : MonoBehaviour
{
    // Sadece arena prefabını tutmak için alan
    public GameObject arenaPrefab;

    // Düğme tıklamasıyla çağrılacak fonksiyon
    public void OnButtonClick()
    {
        // Seçilen arena prefabını sakla
        SelectionManager.Instance.SetSelectedArenaPrefab(arenaPrefab);

    }
}


