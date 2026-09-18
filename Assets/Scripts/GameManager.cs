using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("金币UI文本")]
    public TextMeshProUGUI coinText;
    private int _coinCount;

    void Start()
    {
        _coinCount = 0;
        RefreshCoinDisplay();
        //订阅金币拾取事件
        Coin.OnCoinPicked += HandleCoinPickup;
    }

    void OnDestroy()
    {
        //取消订阅，防止内存泄漏！
        Coin.OnCoinPicked -= HandleCoinPickup;
    }

    void HandleCoinPickup()
    {
        _coinCount++;
        RefreshCoinDisplay();
    }

    void RefreshCoinDisplay()
    {
        coinText.text = $"Coins: {_coinCount}";
    }
}
