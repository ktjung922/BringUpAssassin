using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class ADmoManager : MonoBehaviour
{
    public bool isTestMode;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadBannerAd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    AdRequest GetAdRequest() {
        return new AdRequest.Builder().AddTestDevice("4402D075F5125BFF").Build();
    }

    #region 배너광고
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-7297606231096997/2504193461";
    BannerView bannerAd;

    void LoadBannerAd() {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID, AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
    }
    #endregion
}
