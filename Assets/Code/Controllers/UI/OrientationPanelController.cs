using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrientationPanelController : MonoBehaviour, IDataRecipient
{
    [SerializeField] private TextMeshProUGUI m_RollText;
    [SerializeField] private TextMeshProUGUI m_PitchText;
    [SerializeField] private TextMeshProUGUI m_YawText;

    private void Start()
    {
        m_RollText.SetText("Roll: NaN");
        m_PitchText.SetText("Pitch: NaN");
        m_YawText.SetText("Yaw: NaN");
    }

    public void OnSetData(RecipientData data)
    {
        m_RollText.SetText("Roll: " + Mathf.RoundToInt(data.roll).ToString() + "�");
        m_PitchText.SetText("Pitch: " + Mathf.RoundToInt(data.pitch).ToString() + "�");
        m_YawText.SetText("Yaw: " + Mathf.RoundToInt(data.yaw).ToString() + "�");
    }
}