using UnityEngine;
using UnityEngine.UI;

public class ButonGeriSayim : MonoBehaviour
{
    public Button buton;
    public Text geriSayimMetni;
    public float geriSayimSuresi = 4f; 
    public Image image1;
    
    private float kalanSure;
    private bool geriSayimBasladi = false;

    void Start()
    {
        
        buton.onClick.AddListener(OnButonTiklandi);
        image1.gameObject.SetActive(true);
        geriSayimMetni.gameObject.SetActive(false);

    }

    void Update()
    {
        if (geriSayimBasladi)
        {
            if (kalanSure > 0)
            {
                kalanSure -= Time.deltaTime;
                geriSayimMetni.text = Mathf.CeilToInt(kalanSure).ToString();  // Geri sayım metnini güncelle
            }
            else
            {
                 image1.gameObject.SetActive(true);
                geriSayimBasladi = false;
                geriSayimMetni.gameObject.SetActive(false);
            }
        }
    }

    public void OnButonTiklandi()
    {
        if (!geriSayimBasladi)
        {
            // Butona tıklandığında ve geri sayım başlamadıysa başlat
            kalanSure = geriSayimSuresi;
            geriSayimMetni.text = kalanSure.ToString();  // Metin alanına başlangıç değerini yazın
            geriSayimBasladi = true;

            image1.gameObject.SetActive(false);
            geriSayimMetni.gameObject.SetActive(true);
            
        }
    }
}


