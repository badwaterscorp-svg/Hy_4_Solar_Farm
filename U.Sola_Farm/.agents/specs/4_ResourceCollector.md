## 4 Resource Collector

**Do**
- revisa el feature "Resources" ,recuerda ufolder, para encontrar la carpeta, quiero almacenar los Resources en player prefs
**Requeriment 1**
- debes utilizar CollsionDetection.cs, y afiliate al on collision enter
- cuando ocurre una collision agregar al siguiente servicio (guiate de uservice) ResourceInventoryService
- quiero que agreges a una lista de ResourceModel dentro de ResourceInventoryService, una copia de ResourceModel ontenida por el metodo copy del ICopy
- usa un metodo para guardar el inventario en un json Revisa ResourceWrapper.cs , la llave de playerPrefs sera la debes crear en KeyStorage.cs, ya esta creada
buscala en ExternalAssets/B_Extensions/(debes buscar dentro de esta carpeta que no se donde esta)