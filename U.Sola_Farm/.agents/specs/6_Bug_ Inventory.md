## 6 Bug inventory
**Context**
En este momento en ResourceInventoryService agrega un objeto, quiero cambiar esto

**Requeriments**
-haz que cada vez que se agregue un resource con el mismo nombre o que ya este su referencia aumente su varible cantidad, si no esta creala en ResourceModel

-crea un event action para el ResourceModel que se invoque cuando su cantidad haya cambiado, cob el primer parametro, valor actual y valor anterior