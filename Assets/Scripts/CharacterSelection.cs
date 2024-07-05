using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Button[] characterButtons; // Karakter butonları (tıklanabilirlik için kullanılır)
    public Image[] characterImages; // Karakter görselleri (aktif/pasif durumlarını kontrol etmek için kullanılır)

    private int selectedCharacterIndex = -1; // Başlangıçta hiçbir karakter seçilmemiştir.

    private void Start()
    {
        // Başlangıçta tüm karakter görsellerini pasif yapın
        for (int i = 0; i < characterImages.Length; i++)
        {
            characterImages[i].gameObject.SetActive(false);
        }

        // Her karakter butonuna tıklanabilirlik işlevi ekleyin
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int characterIndex = i; // Döngü değişkenini korumak için
            characterButtons[i].onClick.AddListener(() => ToggleCharacter(characterIndex));
        }
    }

  private void ToggleCharacter(int characterIndex)
{
    // Eğer seçilen karakter aynı karakterse, işaretleme işlemi yapma
    if (selectedCharacterIndex == characterIndex)
    {
        return;
    }

    // Önceki karakterin görselini pasif yapın (eğer varsa)
    if (selectedCharacterIndex != -1)
    {
        characterImages[selectedCharacterIndex].gameObject.SetActive(false);
    }

    // Yeni karakteri aktif hale getirin
    characterImages[characterIndex].gameObject.SetActive(true);

    // Seçilen karakteri saklayın
    selectedCharacterIndex = characterIndex;
}
}


