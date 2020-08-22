using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admob : MonoBehaviour
{
    private BannerView adBanner;
    private string idApp, idBanner;

    // Start is called before the first frame update
    void Start()
    {
        idApp = "ca-app-pub-3940256099942544/2247696110";
        idBanner = "ca-app-pub-3940256099942544/6300978111";

        MobileAds.Initialize(idApp);
        RequestBannerAd();
    }

    #region Banner Methods
    public void RequestBannerAd()
    {
        adBanner = new BannerView(idBanner, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = AdRequestBuild();
        adBanner.LoadAd(request);
    }

    public void DestroyBannerAd()
    {
        if (adBanner != null)
            adBanner.Destroy();
    }
    #endregion
    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }
}
