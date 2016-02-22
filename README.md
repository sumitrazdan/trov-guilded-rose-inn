# trov-guilded-rose-inn
The Gilded Rose WebAPI

Assumptions:
1. Each Item will be uniquely identified by the "Name".
2. Every item in the inventory should have an intial Quantity.

Based on the above assumptions, modified the "Item" class to add a "int Quantity" property. Moreover, in keeping with the need of a inventory, using the Repository Pattern defined a inventory for data persistance.

The IReposity<T> generic interface enables for other repositories to be implemented as needed (eg. dbContext) to connect to various persistance mechanism. For the project's scope, defined an in memory "MemoryRepository" for this purpose with some seed data.

Seperately, a Shopping "Cart" object was defined to provide the "buy product" behaviour. This includes keeping a running total of the shopping cart along with inventory update.
