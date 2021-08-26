using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{
    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] Sprite[] images;
    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        for(int i = 0; i<images.Length;i++)
        {
            g = Instantiate (ItemTemplate, ShopScrollView);
            Image child = g.GetComponentInChildren<Image>();
            child.sprite = images[i];
        }
        Destroy (ItemTemplate);
    }
    
}
