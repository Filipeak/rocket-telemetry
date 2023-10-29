/*
 * DOCUMENTATION: https://developers.google.com/maps/documentation/maps-static/start
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapController : DataRecipient
{
    private const string MAPS_API_KEY = "AIzaSyCcEhow5e8b5YGwrRYCfauF6Vi-kv_rfm8";
    private const int MAP_ZOOM = 13;
    private const int MAP_IMAGE_SIZE = 350;
    private const string MAPS_URL = "https://maps.googleapis.com/maps/api/staticmap?";
    private const float MAP_UPDATE_RATE = 2f;

    [SerializeField] private TextMeshProUGUI m_LatitudeText;
    [SerializeField] private TextMeshProUGUI m_LongtitudeText;
    [SerializeField] private RawImage m_MapImage;

    private float _lastUpdateTime = -999f;

    public override void OnSetData(RecipentData data)
    {
        if (AppModeController.CurrentMode == AppMode.DataDownload)
        {
            return;
        }

        m_LatitudeText.SetText($"{MathUtils.NumberFiveDecimalPlaces(data.latitude)}�");
        m_LongtitudeText.SetText($"{MathUtils.NumberFiveDecimalPlaces(data.longitude)}�");

        if (Time.time > _lastUpdateTime + MAP_UPDATE_RATE)
        {
            StartCoroutine(GetLocationRoutine(data.latitude, data.longitude));

            _lastUpdateTime = Time.time;
        }
    }

    private IEnumerator GetLocationRoutine(float lat, float lon)
    {
        var url = $"{MAPS_URL}center={lat},{lon}&zoom={MAP_ZOOM}&size={MAP_IMAGE_SIZE}x{MAP_IMAGE_SIZE}&key={MAPS_API_KEY}";

        using var map = UnityWebRequestTexture.GetTexture(url);

        yield return map.SendWebRequest();

        if (map.result == UnityWebRequest.Result.ConnectionError || map.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Map error: " + map.error);
        }

        m_MapImage.texture = DownloadHandlerTexture.GetContent(map);
    }
}