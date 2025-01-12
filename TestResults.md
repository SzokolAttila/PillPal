# Test Results

| Test ID | Kind | Type | Suite | Test Case | Description | Last successful run | Runtime | Latest result |
| ----- | ----- | ----- | ----- | ----- | ----- | ----- | ----- | ----- |
| C/1 | Automated | Component | IDCollectionTests | ExistingItemCanBeUpdated | An added item can be modified via IDCollection | 2025-01-11 | 85 ms | Passed |
| C/2 | Automated | Component | IDCollectionTests | IndexerFindsExistingItems | An added item can be accessed by id with indexer | 2025-01-11 | < 1 ms | Passed |
| C/3 | Automated | Component | IDCollectionTests | ItemCannotBeAddedTwice | An already added item cannot be added to IDCollection again | 2025-01-11 | < 1 ms | Passed |
| C/4 | Automated | Component | IDCollectionTests | ItemsCanBeAdded | Adding an item to IDCollection increases its size | 2025-01-11 | < 1 ms | Passed |
| C/5 | Automated | Component | IDCollectionTests | ItemsCanBeRemoved | Removing an item from IDCollection decreases its size | 2025-01-11 | 4900 ms | Passed |
| C/6 | Automated | Component | IDCollectionTests | UpdatingOrDeletingNotExistingItemReturnsFalse | Cannot remove or update not added item in IDCollection | 2025-01-11 | 93 ms | Passed |
| I/1 | Automated | Integration | MedicineAPITests | DeletingAsAnonyomousOrUserThrowsArgumentException | Trying to delete medicine as anyone but admin throws exception | 2025-01-11 | 897 ms | Passed |
| I/2 | Automated | Integration | MedicineAPITests | DeletingMedicineLowersTheNumberOfMedicines | Deleting medicine as admin decreases number of medicines | 2025-01-11 | 798 ms | Passed |
| I/3 | Automated | Integration | MedicineAPITests | DeletingNonExistentIdThrowsArgumentException | Deleting non existent medicine throws exception | 2025-01-11 | 383 ms | Passed |
| I/4 | Automated | Integration | MedicineAPITests | PostingAsAnonyomousOrUserThrowsArgumentException | Trying to post medicine as anyone but admin throws exception | 2025-01-11 | 10.500 ms | Passed |
| I/5 | Automated | Integration | MedicineAPITests | PostingNewMedicineIncrementsNumberOfMedicines | Posting medicine as admin increases number of medicines | 2025-01-11 | 640 ms | Passed |
| I/6 | Automated | Integration | MedicineAPITests | PostingOrPuttingMedicineWithNotMgOrMlPackageUnitThrowsException | Posting or putting medicine with not mg or ml package unit throws exception | 2025-01-11 | 600 ms | Passed |
| I/7 | Automated | Integration | MedicineAPITests | PostingOrPuttingMedicineWithShortOrLongManufacturerThrowsException | Posting or putting medicine with long or short manufacturer name throws exception | 2025-01-11 | 472 ms | Passed |
| I/8 | Automated | Integration | MedicineAPITests | PostingOrPuttingMedicineWithShortOrLongNameThrowsException | Posting or putting medicine with long or short name throws exception | 2025-01-11 | 595 ms | Passed |
| I/9 | Automated | Integration | MedicineAPITests | PuttingAsAnonyomousOrUserThrowsArgumentException | Trying to put medicine as anyone but admin throws exception | 2025-01-11 | 1700 ms | Passed |
| I/10 | Automated | Integration | MedicineAPITests | PuttingToNonExistentIdThrowsArgumentException | Trying to put medicine to non existing id throws exception | 2025-01-11 | 462 ms | Passed |
| I/11 | Automated | Integration | MedicineAPITests | UpdatingTheNameChangesIt | Putting to medicines with modified name changes the medicine's name | 2025-01-11 | 444 ms | Passed |
