using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class ads : MonoBehaviour
{
    private void Start()
    {
        Advertisement.Initialize("3569945", false);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void RewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Time.timeScale = 0;
            Advertisement.Show("rewardedVideo");

        }
        OnUnityAdsDidFinish("rewardedVideo");


    }

    public void Ad()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
            Time.timeScale = 0;
        }
        OnUnityAdsDidFinish("video");
    }

    void OnUnityAdsDidFinish(string placementId)
    {
        if (placementId == "rewardedVideo")
        {
            GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Continue>().ContinueGame();
        }
        if (placementId == "video")
        {
            GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneChanger>().ToMainGame();
        }
    } 
}
