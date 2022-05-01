# CafesAPI

## API Endpoints

## Changes
- I decided to turn the OpeningTime and ClosingTime into its own table called Schedule. The schedule contained properties relevant to a Schedule entity, but not to a Cafe such as "Monday", "Friday", etc...
- I decided to turn the Location property into its own table. The Location entity contained properties such as "zipcode", "street", "city",..etc which are relevant to a Location entity rather than a Cafe.
- To create a proper parent and child relationship I made several changes regarding the foreign key placement.
  I placed the MenuId inside of Item, the CafeId inside of Menu, the CafeId inside of Schedule, and the CafeId inside of Location.
- I removed the InStock property from Item because I felt like it was unecessary and it would be difficult to keep track of in a realistic scenario.
- I added a phone number and student discount property to cafe to provide even more useful information. I gave the phone number a unique constraint since there should not be a duplicate phone number.
