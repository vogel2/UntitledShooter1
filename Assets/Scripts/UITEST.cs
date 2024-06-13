using UnityEngine;
using UnityEngine.UI;

public class UITEST : MonoBehaviour
{
    public UIManager uiManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            uiManager.UpdateKeyUI(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            uiManager.UpdateKeyUI(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            uiManager.UpdateKeyUI(3);
        }
    }
}