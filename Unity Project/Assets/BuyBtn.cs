using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnlyText(GameObject gm, string text)
    {
        gm.GetComponentInChildren<TMP_Text>().text = text;
        gm.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        gm.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        //bombButton.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().rect = new Rect(0,0,88,68);
        gm.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        gm.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().sizeDelta = new Vector2(88, 68);
            
        gm.transform.Find("Star").transform.localScale = Vector3.zero;
    }
}
