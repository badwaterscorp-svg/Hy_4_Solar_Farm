using System.Collections;
using UnityEngine;

[System.Serializable]
public class ShowPanelDirty 
{
    [SerializeField] GameObject iconDirty;
    [SerializeField] TriggerDetector triggerDirty;
    [SerializeField] ResourceCollectionCard card;
    [SerializeField] ResourceSheet sheetResource;
    [SerializeField] int amountToClean = 2;
    [SerializeField] ResourceSpawnerCountView countView;
    private ResourceModel bufferModel;
    SolarPanelSpawnerHandler handler;

    public void Configure(SolarPanelSpawnerHandler _handler)
    {
        this.handler = _handler;
        bufferModel = sheetResource.Model.Copy();
        bufferModel.Amount = amountToClean;
        card.Configure(sheetResource, amountToClean);
        triggerDirty.OnTriggerStayed += Clean;
    }

    public void Unsubscribe() 
    {
        triggerDirty.OnTriggerStayed -= Clean;
    }

    private void Clean(Transform t) 
    {
        if(debtCoroutine == null)
            debtCoroutine = handler.StartCoroutine(DoDebt());
    }

    Coroutine debtCoroutine;
    private IEnumerator DoDebt() 
    {
        yield return new WaitForSeconds(0.1f);
        BackPackHandler backPack = PlayerHandler.Instance.AccessBackPackHandler();
        if (backPack.GetCountResource(bufferModel.Name) > 0)
        {
            PlayerHandler.Instance.ThrowResource(bufferModel, triggerDirty.transform, 0.5f);
            backPack.RemoveResource(bufferModel);
            bufferModel.RemoveAmount(1);
            card.Draw(bufferModel);
        }

        if (bufferModel.Amount <= 0)
        {
            Debug.Log("TODO Cleaned. Mostrar Particulas");
            DoClean();
        }

        debtCoroutine = null;
    }

    public void DoDirty()
    {
        handler.StopSpawning();
        iconDirty.SetActive(true);
        card.gameObject.SetActive(true);
        countView?.gameObject.SetActive(false);
        triggerDirty.gameObject.SetActive(true);
    }

    public void DoClean() 
    {
        handler.StartSpawning();
        iconDirty.SetActive(false);
        card.gameObject.SetActive(false);
        countView?.gameObject.SetActive(true);
        triggerDirty.gameObject.SetActive(false);
    }
}