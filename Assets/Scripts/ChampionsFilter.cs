using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChampionsFilter : MonoBehaviour
{
    public List<GameObject> paraBabasiBabaları;
    public List<GameObject> garibanCocuklar;
    public List<GameObject> sokakCocukları;
    public List<GameObject> tumCocuklar;
    public RectTransform panelRectTransform; // Panelin RectTransform bileşeni

    public float paraBabasiBoyut = 1020f;
    public float sokakCocuklariBoyut = 1020f;
    public float garibanBoyut = 450f;
    public float tumCocuklarBoyut = 3307f;

    public void ParaBabasiSecildi()
    {
        AktifGrupDegistir(paraBabasiBabaları);
        AyarlaBoyut(paraBabasiBoyut);
    }

    public void GaribanSecildi()
    {
        AktifGrupDegistir(garibanCocuklar);
        AyarlaBoyut(garibanBoyut);
    }

    public void SokakCocuklariSecildi()
    {
        AktifGrupDegistir(sokakCocukları);
        AyarlaBoyut(sokakCocuklariBoyut);
    }

    public void TumCocuklarSecildi()
    {
        AktifGrupDegistir(tumCocuklar);
        AyarlaBoyut(tumCocuklarBoyut);
    }



    private void AktifGrupDegistir(List<GameObject> hedefGrup)
    {
        // Tüm grupları devre dışı bırak
        paraBabasiBabaları.ForEach(karakter => karakter.SetActive(false));
        garibanCocuklar.ForEach(karakter => karakter.SetActive(false));
        sokakCocukları.ForEach(karakter => karakter.SetActive(false));
        tumCocuklar.ForEach(karakter => karakter.SetActive(false));

        // Hedef grubu etkinleştir
        hedefGrup.ForEach(karakter => karakter.SetActive(true));
    }

    private void AyarlaBoyut(float boyut)
    {
        panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, boyut);
    }
}

