# EFBulkInsert_Implementation
An implementation using EFBulkInsert and LINQ to Entities with SQL server 2019 Standard Ed.

## Install "EFBulkInsert" from nuget.org
https://github.com/andreisabau/EFBulkInsert

## Bulk insert performance.
There may be more than 10x performance than inserting records with using the AddRange method of EF.
I tried inserting 15 million data (15 * 1000000) to my local sql server database.
