using UnityEngine;

public class Coin : MonoBehaviour
{
    //静态事件：金币拾取事件
    public static event System.Action OnCoinPicked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //触发事件，通知GameManager；金币自己不知道管理器是谁
            OnCoinPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}
