using UnityEngine;
using System.Collections;
using admob;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }
    const string bannerId = "ca-app-pub-8827222531749319/9049313588";

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Admob.Instance().initAdmob(bannerId, "");
    }

    public void ShowBanner()
    {
        Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 3);
    }

    
}
