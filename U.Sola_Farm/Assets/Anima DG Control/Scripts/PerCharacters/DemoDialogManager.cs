using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

public class DemoDialogManager:MonoBehaviour
{
    [SerializeField] DemoDialogAnimation dialogDisplayer;
    [SerializeField] public List<string> dialogs = new List<string>();
    [SerializeField] List<string> interfaces = new List<string>();

    private void OnValidate()
    {
#if ANIMA_DOTWEEN_PRO
        var interfaceType = typeof(ITypingAnimaStrategy);
        var implementingTypes = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass);
        foreach (var type in implementingTypes)
        {
            if (!interfaces.Contains(type.Name))
            {
                interfaces.Add(type.Name);
            }
        }
#endif
    }

    private IEnumerator Start()
    {

        yield return new WaitForSeconds(0.1f);
#if ANIMA_DOTWEEN_PRO
        Next();
#else
        print("You don't have DOTween Pro. This script will not work.");
#endif
    }

#if ANIMA_DOTWEEN_PRO
    public void Next() 
    {
        // here get the type of the class by it name, the first of the list
        Type tipo = Type.GetType(interfaces[0]);

        if (tipo != null)
        {
            object instancia = Activator.CreateInstance(tipo);
            if (instancia is ITypingAnimaStrategy asInterface)
            {
                dialogDisplayer.AnimateText(asInterface, dialogs[0]);
            }
        }
    }
#endif
}



