using B_Extensions;
using UnityEngine;

public class ButtonImproveBackPack : BaseButtonAttendant
{
    [SerializeField] private int _amountToAdd = 5;
    BackPackImproveScreen backPackScreenImprove;
    private void Start()
    {
        buttonComponent.onClick.AddListener(Improve);
        backPackScreenImprove = BackPackImproveScreen.Instance;

    }

    public void Improve()
    {
        BackPack backPack = PlayerHandler.Instance.AccessBackPackHandler().AccessBackPackAsClass();
        if (backPack == null)
            return;
        backPack.ImproveAmountMax(_amountToAdd);
        backPackScreenImprove.ConsumeCost();
    }

    public void EnableButton() => buttonComponent.interactable = true;
    public void DisableButton() => buttonComponent.interactable = false;
}
