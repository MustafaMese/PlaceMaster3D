using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CoinsManager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] Canvas goldCanvas;
    [SerializeField] TMP_Text coinUIText;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header("Available coins : (coins to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] Ease easeType;
    [SerializeField] float spread;
    Vector3 targetPosition;

    private int coins = 0;
    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            coinUIText.text = Coins.ToString();
        }
    }

    void Awake()
    {
        targetPosition = target.position;
        coinUIText.text = 0.ToString();
        PrepareCoins();
    }

    void PrepareCoins()
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = goldCanvas.transform;
            coin.transform.rotation = new Quaternion(0, 0, 0, 0);
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    void Animate(Vector3 collectedCoinPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if(coinsQueue.Count > 0)
            {
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);
                WorldPositionToCanvasPosition(collectedCoinPosition, coin);

                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);

                        Coins++;
                    });
            }
        }
    }

    private void WorldPositionToCanvasPosition(Vector3 collectedCoinPosition, GameObject coin)
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(collectedCoinPosition);
        coin.transform.position = collectedCoinPosition;
        print(coin.transform.position);
        RectTransform CanvasRect = goldCanvas.GetComponent<RectTransform>();

        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        coin.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
        coin.transform.position = coin.transform.position + new Vector3(Random.Range(-spread, spread), 0f, 0f);
    }

    public void AddCoins(Vector3 collectedCoinPosition, int amount)
    {
        Animate(collectedCoinPosition, amount);
    }
}
