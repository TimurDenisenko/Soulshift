using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject tooltipBox;
    [SerializeField] TMP_Text tooltipText;
    [SerializeField] Vector3 offset;
    bool isShowed;
    void Update()
    {
        if (!isShowed)
            return;
        tooltipBox.transform.position = Input.mousePosition + offset;
    }
    public void ShowTooltip(string text)
    {
        isShowed = true;
        tooltipBox.SetActive(true);
        tooltipText.text = text;
    }
    public void HideTooltip()
    {
        isShowed = false;
        tooltipBox.SetActive(false);
    }
}
