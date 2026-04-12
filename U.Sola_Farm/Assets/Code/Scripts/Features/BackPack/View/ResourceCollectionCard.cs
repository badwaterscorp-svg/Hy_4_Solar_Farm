using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceCollectionCard : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountText;
    ResourceSheet sheetBuffer = null;
    public ResourceSheet Sheet => sheetBuffer;
    public bool HasSheet => sheetBuffer != null;

    public void Configure(ResourceSheet sheet, int amount) 
    {
        sheetBuffer = sheet;
        if (sheet != null && sheet.Spt != null)
        {
            _icon.sprite = sheet.Spt;
        }
        _amountText.text = amount.ToString();
    }

    public void Draw(ResourceSheet sheet, int amount)
    {
        if (sheet != null && sheet.Spt != null)
        {
            _icon.sprite = sheet.Spt;
        }
        _amountText.text = amount.ToString();
    }

    public void Draw(ResourceModel model)
    {
        if (model != null && model.Name.Equals(sheetBuffer.Model.Name))
        {
            _amountText.text = model.Amount.ToString();
        }
    }
}