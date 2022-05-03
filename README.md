# CafesAPI
#### Author: Jordan Sukhnandan
### Description
An api containing a list of cafes and information for people to find a good place to study and/or do their work. Currently the api only contains information for places located in New York City.

## API Endpoints
| HTTP Method |  API Endpoint                         | Description                                                                 |
| ----------- | ------------------------------------- | --------------------------------------------------------------------------- |                               
| GET         | /api/cafes                            | Return a list of all the cafes                                              |
| GET         | /api/cafes/{CafeId}                   | Return a cafe for a given id                                                |
| GET         | /api/cafes/{CafeId}/items/{ItemName}  | Return item(s) for a cafe for a given item name and cafe id                 |
| GET         | /api/cafes/{CafeName}                 | Return a list of cafes for a given cafe name                                |
| POST        | /api/cafes                            | Add a new cafe                                                              |
| PUT         | /api/cafes/{CafeId}                   | Update an existing cafe for a given id                                      |
| DELETE      | /api/cafes/{CafeId}                   | Delete an existing cafe for a given id, Note: Will delete children as well! |
| GET         | /api/menus                            | Return a list of all the menus and their items                              |
| GET         | /api/menus/{MenuId}                   | Return a menu and it's items for a given id                                 |
| GET         | /api/menus/{itemName}                 | Return a list of menus containing a given item name                         |
| POST        | /api/menus                            | Add a new menu for an existing cafe                                         |
| PUT         | /api/menus/{MenuId}                   | Update an existing menu for a given id                                      |
| DELETE      | /api/menus/{MenuId}                   | Delete an existing menu for a given id                                      |
| GET         | /api/items                            | Return a list of all the items                                              |
| GET         | /api/items/{ItemId}                   | Return an item for a given id                                               |
| POST        | /api/items                            | Add a new item for an existing menu                                         |
| PUT         | /api/items/{ItemId}                   | Update an existing item for a given id                                      |
| DELETE      | /api/items/{ItemId}                   | Delete an existing item for a given id                                      |


## Sample Requests/Response
- GET /api/cafes
<img src="https://github.com/Kirazuto7/CafesAPI/blob/master/getcafes.png" width=350>

- GET /api/cafes/{CafeId}
<img src="https://github.com/Kirazuto7/CafesAPI/blob/master/getcafe.png" width=500>

- GET /api/cafes/id/{CafeName}
<img src="https://github.com/Kirazuto7/CafesAPI/blob/master/getcafename.png" width=500>

- GET /api/menus
<img src="https://github.com/Kirazuto7/CafesAPI/blob/master/getmenus.png" width=500>

- POST /api/cafes
<img src="https://github.com/Kirazuto7/CafesAPI/blob/master/postcafe.png" width=500>

## Changes
- I decided to turn the OpeningTime and ClosingTime into its own table called Schedule. The schedule contained properties relevant to a Schedule entity, but not to a Cafe such as "Monday", "Friday", etc...
- I decided to turn the Location property into its own table. The Location entity contained properties such as "zipcode", "street", "city",..etc which are relevant to a Location entity rather than a Cafe.
- To create a proper parent and child relationship I made several changes regarding the foreign key placement.
  I placed the MenuId inside of Item, the CafeId inside of Menu, the CafeId inside of Schedule, and the CafeId inside of Location.
- I removed the InStock property from Item because I felt like it was unecessary and it would be difficult to keep track of in a realistic scenario.
- I added a phone number and student discount property to cafe to provide even more useful information. I gave the phone number a unique constraint since there should not be a duplicate phone number.
