using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public int money = 1000; 
    public Text goldText;

  


    void Start()
   {
    
      money = PlayerPrefs.GetInt("GoldAmount", 1000);

      UpdateGoldText(); 
   }

    
    public void SpendGold(int amount)
    {
        if (money >= amount)
        {
          money -= amount;
          UpdateGoldText(); 

        
          PlayerPrefs.SetInt("GoldAmount", money);
        }

        else
        {
          Debug.Log("Yetersiz altın!");
        }
    }

    

    private void UpdateGoldText()
    {
        goldText.text = money.ToString();
    }

}
