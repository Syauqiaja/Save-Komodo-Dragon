
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public DataOverScene dataHolder;
    public Image avatar;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI crystal;
    public TextMeshProUGUI energy;

    private void Awake() {
        dataHolder.OnEconomyChanged += RefreshStats;
        DataPresistenceManager.OnAfterLoad += RefreshStats;
    }
    private void Start() {
        RefreshStats();
    }
    void SetGold(){
        gold.text = dataHolder.Gold.ToString();
    }
    void SetCrystal(){
        crystal.text = dataHolder.Crystal.ToString();
    }
    private void RefreshStats() {
        if(!dataHolder.IsLoaded) return;
        playerName.text = dataHolder.PlayerName;
        gold.text = dataHolder.Gold.ToString();
        crystal.text = dataHolder.Crystal.ToString();
        energy.text = dataHolder.Energy.ToString();
    }
    private void OnDestroy() {
        dataHolder.OnEconomyChanged -= SetGold;
        DataPresistenceManager.OnAfterLoad -= RefreshStats;
    }
}
