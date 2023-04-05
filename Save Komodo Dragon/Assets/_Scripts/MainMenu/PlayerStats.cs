
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
        dataHolder.OnGoldChanged += SetGold;
        DataPresistenceManager.OnAfterLoad += RefreshStats;
    }
    private void Start() {
        RefreshStats();
    }
    void SetGold(){
        gold.text = dataHolder.Gold.ToString();
    }
    private void RefreshStats() {
        if(!dataHolder.IsLoaded) return;
        playerName.text = dataHolder.PlayerName;
        gold.text = dataHolder.Gold.ToString();
        crystal.text = dataHolder.Crystal.ToString();
        energy.text = dataHolder.Energy.ToString();
    }
    private void OnDestroy() {
        dataHolder.OnGoldChanged -= SetGold;
        DataPresistenceManager.OnAfterLoad -= RefreshStats;
    }
}
