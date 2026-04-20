using UnityEngine;

[System.Serializable]
public class  BuildingPlaceUI
{
    [SerializeField] private ResourceCollectionCard[] cards;
    BuildingPlaceHandler placeHandler;

    public void Configure(BuildingPlaceHandler placeHandler) 
    {
        this.placeHandler = placeHandler;
        for (int i = 0; i < placeHandler.storageRequirements.Count; i++)
        {
            var req = placeHandler.storageRequirements[i];
            var sheet = ResourcesRepository.Instance.GetSheetByName(req.Name);
            cards[i].Configure(sheet, req.Amount);
        }
    }

    public void UpdateUI()
    {
        foreach (var item in placeHandler.storageRequirements)
        {
            foreach (var card in cards)
            {
                card.Draw(item);
            }
        }
    }
}