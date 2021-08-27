using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShop : MonoBehaviour
{
    [System.Serializable] public class ShopItem
	{
		public string Imagename;
		public int Price;
		public bool IsPurchased = false;
	}

	public List<ShopItem> ShopItemsList;
    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    //[SerializeField] Sprite[] images;

    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        for(int i = 0; i<ShopItemsList.Count;i++)
        {
            g = Instantiate (ItemTemplate, ShopScrollView);
            string path = "Flags/" + ShopItemsList[i].Imagename;
            Sprite spr = Resources.Load<Sprite>(path);
            Image childImage = g.GetComponentInChildren<Image>();
            TMP_Text childText = g.GetComponentInChildren<TMP_Text>();
            childImage.sprite = spr;
            childText.text = ShopItemsList[i].Price.ToString();
        }
        Destroy (ItemTemplate);
    }
    
}
