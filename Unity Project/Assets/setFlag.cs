using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setFlag : MonoBehaviour
{
    public Material flagMat;

    public void setTex() {
        Texture tex = gameObject.GetComponentInChildren<Image>().sprite.texture;
        Debug.Log(tex);
        flagMat.SetTexture("_MainTex", tex);
    }
}
