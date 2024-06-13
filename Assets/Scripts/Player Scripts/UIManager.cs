using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image keyImage1;
    public Image keyImage2;
    public Image keyImage3;

    public Sprite key1CollectedSprite;
    public Sprite key2CollectedSprite;
    public Sprite key3CollectedSprite;

    public void UpdateKeyUI(int keyNumber)
    {
        switch (keyNumber)
        {
            case 1:
                keyImage1.sprite = key1CollectedSprite;
                Debug.Log("Updated Key 1 Image");
                break;
            case 2:
                keyImage2.sprite = key2CollectedSprite;
                Debug.Log("Updated Key 2 Image");
                break;
            case 3:
                keyImage3.sprite = key3CollectedSprite;
                Debug.Log("Updated Key 3 Image");
                break;
            default:
                Debug.Log("Invalid key number");
                break;
        }
    }
}
