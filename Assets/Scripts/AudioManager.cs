using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioSource;
    private bool isMusicPlaying = true;
    public GameObject button1;
    public GameObject button2;

    private const string MusicStatusKey = "MusicStatus";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            
            // PlayerPrefs'ten müzik durumunu kontrol et ve varsayılan olarak true (çalınıyor) varsayalım.
            isMusicPlaying = PlayerPrefs.GetInt(MusicStatusKey, 1) == 1;

            // Müziği duruma göre başlatın veya duraklatın.
            if (isMusicPlaying)
            {
                PlayMusic();
            }
            else
            {
                PauseMusic();
            }
        }
    }

    public void PlayMusic()
    {
        audioSource.Play();
        isMusicPlaying = true;
        button1.SetActive(true);
        button2.SetActive(false);


        // Müzik durumunu PlayerPrefs'e kaydet.
        PlayerPrefs.SetInt(MusicStatusKey, 1);
        PlayerPrefs.Save(); // PlayerPrefs verilerini kaydetmek için Save() yöntemini çağırın.
    }

    public void PauseMusic()
    {
        audioSource.Pause();
        isMusicPlaying = false;
        button1.SetActive(false);
        button2.SetActive(true);

        // Müzik durumunu PlayerPrefs'e kaydet.
        PlayerPrefs.SetInt(MusicStatusKey, 0);
        PlayerPrefs.Save(); // PlayerPrefs verilerini kaydetmek için Save() yöntemini çağırın.
    }
}
