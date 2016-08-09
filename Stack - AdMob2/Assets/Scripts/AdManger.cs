using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManger : MonoBehaviour
{
    private BannerView bannerView;

    public string BannerID; 

    public static AdManger Instance { set; get; }

    private void Start()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public void ShowBanner()
    {
        bannerView = new BannerView(BannerID, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(CreateAdRequest());
        bannerView.Show();
    }

    public void DestroyBanner()
    {
        //if(bannerView.)
        bannerView.Destroy();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

}
