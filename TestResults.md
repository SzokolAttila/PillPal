# Test Results

| Test ID | Kind | Type | Suite | Test Case | Description | Last successful run | Runtime | Latest result |
| ----- | ----- | ----- | ----- | ----- | ----- | ----- | ----- | ----- |
| C/1 | Automated | Component | IDCollectionTests | ExistingItemCanBeUpdated | An added item can be modified via IDCollection | 2025-01-11 | 85 ms | Passed |
| C/2 | Automated | Component | IDCollectionTests | IndexerFindsExistingItems | An added item can be accessed by id with indexer | 2025-01-11 | < 1 ms | Passed |
| C/3 | Automated | Component | IDCollectionTests | ItemCannotBeAddedTwice | An already added item cannot be added to IDCollection again | 2025-01-11 | < 1 ms | Passed |
| C/4 | Automated | Component | IDCollectionTests | ItemsCanBeAdded | Adding an item to IDCollection increases its size | 2025-01-11 | < 1 ms | Passed |
| C/5 | Automated | Component | IDCollectionTests | ItemsCanBeRemoved | Removing an item from IDCollection decreases its size | 2025-01-11 | 4900 ms | Passed |
| C/6 | Automated | Component | IDCollectionTests | UpdatingOrDeletingNotExistingItemReturnsFalse | Cannot remove or update not added item in IDCollection | 2025-01-11 | 93 ms | Passed |
