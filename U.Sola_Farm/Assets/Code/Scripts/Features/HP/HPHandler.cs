using System;
using UnityEngine;

public class HPHandler : MonoBehaviour
{
    [SerializeField] LifeData data;
    [SerializeField] bool destroyOnDead = true;

    LifeModel lifeModel;

    private void Awake()
    {
        lifeModel = new LifeModel(data);
    }

    public LifeModel AccessLife()
    {
        if(lifeModel == null)
            lifeModel = new LifeModel(data);
        return lifeModel;
    }
}


