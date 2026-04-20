## 14 Data building Site

**Do**
Quiero que guardes los datos de BuildingPlaceHandler.cs

**Requirements**
- crea BuildingSiteDataService.js guiate de la skill uservice
- BuildingPlaceHandler tiene un storageRequeriments que es una copia de _buildingRequirments
- primero detecta que la llave "BuildingSite"+IDSite en playerprefs .HasKey al principio y si existe agrega 
el Wrapper obtenido del PlayerPrefs
- en el metodo ConsumeResource crea un metodo que Actualice el Playerprefs.SetString("BuildingSite"+IDSite)
y guarda un WrapperModel que ya existe con los storageRequirements