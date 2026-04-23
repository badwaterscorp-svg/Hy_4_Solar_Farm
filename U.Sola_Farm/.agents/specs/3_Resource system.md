## 3 Resource System

**Do**
- utiliza el skill ufeature para crear la lógica de recolección

**Requeriment 1**
- debes utilizar CollsionDetection.cs, y afiliate al on collision enter
- cuando ocurre una collision agregar al siguiente servicio (guiate de uservice) ResourceInventoryService
- crea un scriptable object ResourceSheet.cs dentro usa un modelo llamado ResourceModel.cs
- dentro del proyecto ya hay una interface que se llamada ICopy recuerda el skill ufeature,
quiero que agreges a una lista de ResourceModel dentro de ResourceInventoryService, una copia de ResourceModel ontenida por el metodo copy del ICopy
**Criterio Aceptacion**
- se debe tener 