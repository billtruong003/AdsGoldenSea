using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour
{
    // Khai báo biến lưu ID quảng cáo và banner quảng cáo.
    private string _adUnitId = "";
    [SerializeField]private string androidAds_ID = "ca-app-pub-3940256099942544/6300978111";
    //[SerializeField]private string IOSAds_ID = "ca-app-pub-6751218490710547/8459839059";
    private BannerView _bannerView;

    public void Start()
    {
        // Khởi tạo SDK quảng cáo Google Mobile Ads.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // Hàm này được gọi khi SDK quảng cáo đã được khởi tạo.
        });

        // Chọn ID quảng cáo dựa trên nền tảng đang chạy.
        #if UNITY_ANDROID
            _adUnitId = androidAds_ID;
        #elif UNITY_IPHONE
            _adUnitId = IOSAds_ID;
        #else
            _adUnitId = "unused";
        #endif

        // Tạo và hiển thị banner quảng cáo.
        CreateBannerView();
    }

    // <summary>
    // Tạo banner quảng cáo với kích thước 320x50 ở vị trí bottom (dưới cùng) của màn hình.
    // </summary>
    public void CreateBannerView()
    {
        Debug.Log("Banner Ads Loading...");

        // Nếu đã có banner quảng cáo thì hủy banner cũ đi.
        if (_bannerView != null)
        {
            DestroyAd();
        }
        
        // Tạo banner quảng cáo với kích thước 320x50 ở vị trí top (đầu tiên) của màn hình.
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Load quảng cáo vào banner.
        LoadAd();
    }

    // <summary>
    // Load quảng cáo vào banner quảng cáo.
    // </summary>
    public void LoadAd()
    {
        // Kiểm tra nếu banner quảng cáo chưa được tạo mới.
        if (_bannerView == null)
        {
            CreateBannerView();
            return;
        }

        // Tạo một yêu cầu quảng cáo rỗng.
        AdRequest request = new AdRequest.Builder().Build();

        // Load quảng cáo vào banner.
        _bannerView.LoadAd(request);
    }

    private void DestroyAd()
    {
        // Hủy banner quảng cáo.
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }
    }
}
