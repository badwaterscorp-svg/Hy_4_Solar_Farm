# 1️⃣ UI (Creador de scripts Interfaz gráfica)
---
name: uui
description: Skill para crear scripts específicos exclusivos para la UI de Unity
---

Ejemplo:
```
Boss/UI/
    CardBoss.cs
    ButtonBossClaimReward.cs
    ToggleBossForgive.cs
    LabelBoss.cs
    TabBossCompleted.cs
    ScreenWinBoss.cs
    ScreenLoseBoss.cs
```

## Regla clave

Al nombrar la UI primero debes indicar lo que es luego su funcionalidad o ufeature asociado,

**Comunicación**:
    Cada UI debe vivir dentro de su carpeta, dentro de Features 
    `Features/`:
    `Features/[Nombre Feature]/UI` -> Contiene Card, Label, Screen, Etc.
    Si es para un Service ... 
    `Services/`:
    `Services/[Nombre Service]/UI` -> Contiene Card, Label, Screen, Etc.
---


```csharp
public class CardBoss:Monobehaviour
{
    [SerializedField] Image thumbnail;
    [SerializedField] ButtonClaimReward btnClaim;
    [SerializedField] LabelBoss labelBoss;

    BossModel _bossModel;
    public void InjectBoss(BossModel _bossModel)
    {
        _bossModel = bossModel;
    }
    
    public void Display()
    {
        labelBoss.SetText(_bossModel.name);
        btnClaim.EnableButton();
    }
}
```

## Buttons Extensions
Yo uso un plugin creado por mi, llamado B_Extensions donde encuentras
una clase llamada BaseButtonAttendant se utiliza para hacer funcionalidades o llamar métodos, y también puedes obtener el componente Button de unity por medio buttonComponent , también aplica para Toggle = toggleComponent.

```csharp
public class ButtonClaimReward:BaseButtonAttendant
{
    private void Start()
    {
        buttonComponent.AddListener(Claim);
    }
    
    public void Claim()
    {
        print("Function");
    }

    public void EnableButton()
    {
        buttonComponent.interactable = true;
    }
}
```

