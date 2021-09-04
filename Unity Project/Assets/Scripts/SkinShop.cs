using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ShopItem
{
    public string imagename;
    public int price;
    public bool isPurchased = false;

    public ShopItem(string imageName, int price, bool isPurchased)
    {
        this.imagename = imageName;
        this.price = price;
        this.isPurchased = isPurchased;
    }
}
public class SkinShop : MonoBehaviour
{
    public List<ShopItem> ShopItemsList;
    public string filename;
    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    public Material flagMat;
    //[SerializeField] Sprite[] images;

    void Start()
    {
        ShopItemsList = FileHandler.ReadListFromJSON<ShopItem>(filename);
        //Debug.Log (;

        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        //ShopItemsList.Add (new ShopItem ("argentina", 100,false));
        FileHandler.SaveToJSON<ShopItem>(ShopItemsList, filename);

        for (int i = 0; i < ShopItemsList.Count; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            string path = "Flags/" + ShopItemsList[i].imagename;
            Sprite spr = Resources.Load<Sprite>(path);
            Image childImage = g.GetComponentInChildren<Image>();
            TMP_Text childText = g.GetComponentInChildren<TMP_Text>();
            Button childButton = g.GetComponentInChildren<Button>();
            childImage.sprite = spr;
            childText.text = ShopItemsList[i].price.ToString();
            if (ShopItemsList[i].isPurchased)
            {
                childButton.GetComponent<BuyBtn>().OnlyText(childButton.gameObject, "USE");
            }
            childButton.interactable = true;
            Debug.Log(i);
            childButton.AddEventListener (i, ClickBtn);


        }
        Destroy(ItemTemplate);
    }

    void ClickBtn(int index)
    {
        if(ShopItemsList[index].isPurchased) SetFlag(index);
        else BuyItem(index);
    }
    void BuyItem(int index)
    {
        Debug.Log(index);
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= ShopItemsList[index].price) {
            //Coins
            PlayerPrefs.SetInt("Coins",coins-ShopItemsList[index].price);
            //Sound
			FindObjectOfType<AudioManager>().upgrade.pitch = 1f;
            FindObjectOfType<AudioManager>().soundUpgrade();
			//purchase Item
			ShopItemsList[index].isPurchased = true;

			//change ui text
			Button btn = ShopScrollView.GetChild(index).GetComponentInChildren<Button>();
            btn.GetComponent<BuyBtn>().OnlyText(btn.gameObject, "USE");
			btn.interactable = true;

            //update json file
            FileHandler.SaveToJSON<ShopItem>(ShopItemsList, filename);
        }
    }

    void SetFlag(int index)
    {   
        string path = "Flags/" + ShopItemsList[index].imagename;
        //Sprite spr = Resources.Load<Sprite>(path);
        Texture tex = Resources.Load<Texture>(path);
        Debug.Log(tex);
        flagMat.SetTexture("_MainTex", tex);
    }

    private void Update() 
    {   
        int coins = PlayerPrefs.GetInt("Coins", 0);
         for (int i = 0; i < ShopItemsList.Count; i++)
        {  
            if(!ShopItemsList[i].isPurchased)
            {
                ShopScrollView.GetChild(i).GetComponentInChildren<Button>().interactable = coins >= ShopItemsList[i].price;
            }
        }
    }
}


