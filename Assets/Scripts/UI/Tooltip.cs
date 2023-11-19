using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    public GameObject tooltip;
    public Camera uicamera;
    public TextMeshProUGUI tooltip_txt;
    public RectTransform tooltip_background_rect;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltip.transform.GetComponent<RectTransform>(), Input.mousePosition, uicamera, out localPoint);
        //tooltip.transform.localPosition = localPoint;
        localPoint = uicamera.ViewportToScreenPoint(Input.mousePosition);
        tooltip.transform.localPosition = localPoint;
    }

    public void ShowTooltip(string text)
    {
        tooltip.SetActive(true);
        tooltip_txt.text = text;
        float text_padding_size = 2;
        Vector2 background_size = new Vector2(tooltip_txt.preferredWidth + text_padding_size , tooltip_txt.preferredHeight + text_padding_size);
        tooltip_background_rect.sizeDelta = background_size;
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}
