using UnityEngine;
using TMPro;
using System.Diagnostics;

public class UIInventoryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI milkText;
    [SerializeField] private TextMeshProUGUI saltText;
    [SerializeField] private TextMeshProUGUI sugarText;
    [SerializeField] private TextMeshProUGUI butterText;
    [SerializeField] private TextMeshProUGUI creamText;
    [SerializeField] private TextMeshProUGUI iceCreamText;
    private int milkValue, sugarValue, saltValue, butterValue, creamValue, icecreamValue;

    public void Initialize()
    {
        Gatherable.OnItemGathered += UpdateUIText;
        UpdateTextValues();
        ResetValues();
    }
    void OnDisable()
    {
        Gatherable.OnItemGathered -= UpdateUIText;
    }

    private void ResetValues()
    {
        milkValue = 0;
        saltValue = 0;
        sugarValue = 0;
        butterValue = 0;
        creamValue = 0;
        icecreamValue = 0;
    }

    private void UpdateTextValues()
    {
        milkText.text = milkValue.ToString();
        saltText.text = saltValue.ToString();
        sugarText.text = sugarValue.ToString();
        butterText.text = butterValue.ToString();
        creamText.text = creamValue.ToString();
        iceCreamText.text = icecreamValue.ToString();
    }

    private void UpdateUIText(GatherableSO _gatherable)
    {
        switch (_gatherable.gatherableType)
        {
            case GatherableType.milk:
                milkValue++;
                break;
            case GatherableType.salt:
                saltValue++;
                break;
            case GatherableType.sugar:
                sugarValue++;
                break;
            case GatherableType.butter:
                butterValue++;
                break;
            case GatherableType.cream:
                creamValue++;
                break;
            case GatherableType.icecream:
                icecreamValue++;
                break;
            default: break;
        }
        UpdateTextValues();
    }
}
