using UnityEngine;

public class DovusSahnesi : MonoBehaviour
{   
    public GameObject arenaSpawnPoint;
    public GameObject characterSpawnPoint;
    public GameObject aiCharacterSpawnPoint;
    
    public GameObject[] aiCharacters;

    private void Start()
    {


        string selectedArenaPrefabName = PlayerPrefs.GetString("SelectedArenaPrefab"); // Önceki sahneden gelen seçilen karakter ve arena prefab isimlerini al
        string selectedCharacterPrefabName = PlayerPrefs.GetString("SelectedCharacterPrefab");
        
        GameObject selectedArenaPrefab = Resources.Load<GameObject>(selectedArenaPrefabName);

        if (selectedArenaPrefab != null)
        {
            // Arenayı arenaSpawnPoint üzerinde oluştur
            GameObject arena = Instantiate(selectedArenaPrefab, arenaSpawnPoint.transform.position, Quaternion.identity);
            
        }
        else
        {
            Debug.LogError("Seçilen arena prefabı bulunamadı!");

        }
        
        // Karakter prefabını yükle
        GameObject selectedCharacterPrefab = Resources.Load<GameObject>(selectedCharacterPrefabName);

        if (selectedCharacterPrefab != null)
        {
            
            GameObject character = Instantiate(selectedCharacterPrefab, characterSpawnPoint.transform.position, Quaternion.identity);
           
        }
        else
        {
            Debug.LogError("Seçilen karakter prefabı bulunamadı!");
        }
        

        int randomIndex = Random.Range(0, aiCharacters.Length);
        GameObject selectedAI = aiCharacters[randomIndex];

        Instantiate(selectedAI, aiCharacterSpawnPoint.transform.position, Quaternion.identity);
    }
}
