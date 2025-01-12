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
| I/12 | Automated | Integration | ReminderAPITests | AdminCanCreateReminderForAnyone | Admin can post a reminder for any user | 2025-01-11 | 656 ms | Passed |
| I/13 | Automated | Integration | ReminderAPITests | AdminCanDeleteAnyReminder | Admin can delete a reminder from any user | 2025-01-11 | 713 ms | Passed |
| I/14 | Automated | Integration | ReminderAPITests | AdminCanEditAnyReminder | Admin can put a reminder to any user | 2025-01-11 | 878 ms | Passed |
| I/15 | Automated | Integration | ReminderAPITests | AdminCanGetUserReminders | Admin can access any reminder from any user | 2025-01-11 | 805 ms | Passed |
| I/16 | Automated | Integration | ReminderAPITests | AdminCanMoveReminders | Admin can modify the userID of any reminder | 2025-01-11 | 755 ms | Passed |
| I/17 | Automated | Integration | ReminderAPITests | AdminRoleNeededToGetAllReminders | Only admin can access all reminders | 2025-01-11 | 964 ms | Passed |
| I/18 | Automated | Integration | ReminderAPITests | CannotAddReminderToNonExistantUser | Posting a reminder to a non existing user throws exception | 2025-01-11 | 659 ms | Passed |
| I/19 | Automated | Integration | ReminderAPITests | CannotAddReminderWithNonExistantMedicine | Posting a reminder with a non existing medicine throws exception | 2025-01-11 | 2800 ms | Passed |
| I/20 | Automated | Integration | ReminderAPITests | CannotDeleteNonExistantReminder | Deleting a non existing reminder throws exception | 2025-01-11 | 603 ms | Passed |
| I/21 | Automated | Integration | ReminderAPITests | CannotEditNonExistantReminder | Modifying a non existing reminder throws exception | 2025-01-11 | 322 ms | Passed |
| I/22 | Automated | Integration | ReminderAPITests | DoseCountCannotBeNegativeOrZero | Posting a reminder with negative or zero dose count throws exception as it would make no sense | 2025-01-11 | 514 ms | Passed |
| I/23 | Automated | Integration | ReminderAPITests | DoseMgCannotBeNegativeOrZero | Posting a reminder with negative or zero dose mg throws exception as it would make no sense | 2025-01-11 | 397 ms | Passed |
| I/24 | Automated | Integration | ReminderAPITests | UserCanAddOwnReminder | Posting a reminder to a user increases the number of reminders of the user | 2025-01-11 | 803 ms | Passed |
| I/25 | Automated | Integration | ReminderAPITests | UserCanDeleteOwnReminder | Deleting a reminder from a user decreases the number of reminders of the user | 2025-01-11 | 558 ms | Passed |
| I/26 | Automated | Integration | ReminderAPITests | UserCanEditOwnReminder | Putting a reminder to a user modifies the user's reminder | 2025-01-11 | 665 ms | Passed |
| I/27 | Automated | Integration | ReminderAPITests | UserCanGetOwnReminders | User can access their own reminders | 2025-01-11 | 327 ms | Passed |
| I/28 | Automated | Integration | ReminderAPITests | UserCannotAddReminderToOtherUsers | User can't post a reminder with other userID | 2025-01-11 | 484 ms | Passed |
| I/29 | Automated | Integration | ReminderAPITests | UserCannotDeleteOthersReminder | User can't delete a reminder with other userID | 2025-01-11 | 659 ms | Passed |
| I/30 | Automated | Integration | ReminderAPITests | UserCannotEditOthersReminder | User can't put a reminder with other userID | 2025-01-11 | 664 ms | Passed |
| I/31 | Automated | Integration | ReminderAPITests | UserCannotGetOtherReminders | User can't access a reminder from other user | 2025-01-11 | 620 ms | Passed |
| I/32 | Automated | Integration | ReminderAPITests | UserCannotMoveReminder | User can't change userID of a reminder to move it to another user | 2025-01-11 | 696 ms | Passed |
