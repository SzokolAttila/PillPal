# Test Results

| Test ID | Kind | Type | Suite | Test Case | Description | Last successful run | Runtime | Latest result |
| ----- | ----- | ----- | ----- | ----- | ----- | ----- | ----- | ----- |
| C/1 | Automated | Component | IDCollectionTests | ExistingItemCanBeUpdated | An added item can be modified via IDCollection | 2025-01-11 | 85 ms | Passed |
| C/2 | Automated | Component | IDCollectionTests | IndexerFindsExistingItems | An added item can be accessed by id with indexer | 2025-01-11 | < 1 ms | Passed |
| C/3 | Automated | Component | IDCollectionTests | ItemCannotBeAddedTwice | An already added item cannot be added to IDCollection again | 2025-01-11 | < 1 ms | Passed |
| C/4 | Automated | Component | IDCollectionTests | ItemsCanBeAdded | Adding an item to IDCollection increases its size | 2025-01-11 | < 1 ms | Passed |
| C/5 | Automated | Component | IDCollectionTests | ItemsCanBeRemoved | Removing an item from IDCollection decreases its size | 2025-01-11 | 4900 ms | Passed |
| C/6 | Automated | Component | IDCollectionTests | UpdatingOrDeletingNotExistingItemReturnsFalse | Cannot remove or update not added item in IDCollection | 2025-01-11 | 93 ms | Passed |
| C/7 | Automated | Component | UserTests | DoHashesMatch | Hashing the same user's password twice returns the same hash (hashing is consistent) | 2025-01-11 | 111 ms | Passed |
| C/8 | Automated | Component | UserTests | HashedPasswordIsNotHashedAgain | When creating a user with a password that is already hashed, the password isn't hashed again | 2025-01-11 | 27 ms | Passed |
| C/9 | Automated | Component | UserTests | IsHashUnique | The same password in two different accounts result in hashes that don't match | 2025-01-11 | 128 ms | Passed |
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
| I/33 | Automated | Integration | UserAPITests | AdminCanDeleteAnyUser | Able to delete any user as admin | 2025-01-11 | 1900 ms | Passed |
| I/34 | Automated | Integration | UserAPITests | AdminCanGetAllUsers | Able to access all user as admin | 2025-01-11 | 342 ms | Passed |
| I/35 | Automated | Integration | UserAPITests | AdminCanGetAnyUserData | Able to access any user as admin | 2025-01-11 | 492 ms | Passed |
| I/36 | Automated | Integration | UserAPITests | AdminCanUpdateAnyUser | Able to update any user as admin | 2025-01-11 | 963 ms | Passed |
| I/37 | Automated | Integration | UserAPITests | CreatingDuplicatedUserThrowsArgumentException | Creating a user twice throws exception | 2025-01-11 | 148 ms | Passed |
| I/38 | Automated | Integration | UserAPITests | CreatingUserWithLongUsernameThrowsArgumentException | Posting a user with long username throws exception | 2025-01-11 | 57 ms | Passed |
| I/39 | Automated | Integration | UserAPITests | CreatingUserWithNoLowercaseInPasswordThrowsArgumentException | Posting a user with no lowercase character in password throws exception | 2025-01-11 | 45 ms | Passed |
| I/40 | Automated | Integration | UserAPITests | CreatingUserWithNoNumberInPasswordThrowsArgumentException | Posting a user with no number in password throws exception | 2025-01-11 | 48 ms | Passed |
| I/41 | Automated | Integration | UserAPITests | CreatingUserWithNoSpecialInPasswordThrowsArgumentException | Posting a user with no special character in password throws exception | 2025-01-11 | 566 ms | Passed |
| I/42 | Automated | Integration | UserAPITests | CreatingUserWithNoUppercaseInPasswordThrowsArgumentException | Posting a user with no uppercase character in password throws exception | 2025-01-11 | 94 ms | Passed |
| I/43 | Automated | Integration | UserAPITests | CreatingUserWithShortPasswordThrowsException | Posting a user with short password throws exception | 2025-01-11 | 140 ms | Passed |
| I/44 | Automated | Integration | UserAPITests | CreatingUserWithShortUsernameThrowsArgumentException | Posting a user with short username throws exception | 2025-01-11 | 57 ms | Passed |
| I/45 | Automated | Integration | UserAPITests | CreatingUserWithSpecialCharacterUsernameThrowsArgumentException | Posting a user with special character in username throws exception | 2025-01-11 | 124 ms | Passed |
| I/46 | Automated | Integration | UserAPITests | CreatingUserWithUniqueUserAndProperPasswordReturnsTrue | Posting a user with unique username and proper password is functioning well | 2025-01-11 | 111 ms | Passed |
| I/47 | Automated | Integration | UserAPITests | DeletingNonExistingIdThrowsException | Deleting a non existing user throws exception | 2025-01-11 | 409 ms | Passed |
| I/48 | Automated | Integration | UserAPITests | GetAllUsersNeedsAuthorization | Accessing all users needs authorization as admin | 2025-01-11 | 48 ms | Passed |
| I/49 | Automated | Integration | UserAPITests | InvalidUserLoginThrowsException | Failed login throws exception | 2025-01-11 | 51 ms | Passed |
| I/50 | Automated | Integration | UserAPITests | LoginExistingUserGivesBackToken | Successful login returns the bearer token | 2025-01-11 | 338 ms | Passed |
| I/51 | Automated | Integration | UserAPITests | PuttingUserWithChangedLongUsernameThrowsException | Modifying username to too long username throws exception | 2025-01-11 | 407 ms | Passed |
| I/52 | Automated | Integration | UserAPITests | PuttingUserWithChangedPasswordWithoutLowercaseThrowsArgumentException | Modifying password to password without lowercase character throws exception | 2025-01-11 | 491 ms | Passed |
| I/53 | Automated | Integration | UserAPITests | PuttingUserWithChangedPasswordWithoutNumbersThrowsException | Modifying password to password without number throws exception | 2025-01-11 | 544 ms | Passed |
| I/54 | Automated | Integration | UserAPITests | PuttingUserWithChangedPasswordWithoutSpecialThrowsArgumentException | Modifying password to password without special character throws exception | 2025-01-11 | 391 ms | Passed |
| I/55 | Automated | Integration | UserAPITests | PuttingUserWithChangedPasswordWithoutUppercaseThrowsException | Modifying password to password without uppercase character throws exception | 2025-01-11 | 561 ms | Passed |
| I/56 | Automated | Integration | UserAPITests | PuttingUserWithChangedShortPasswordThrowsException | Modifying password to short password throws exception | 2025-01-11 | 564 ms | Passed |
| I/57 | Automated | Integration | UserAPITests | PuttingUserWithChangedShortUsernameThrowsException | Modifying username to short username throws exception | 2025-01-11 | 466 ms | Passed |
| I/58 | Automated | Integration | UserAPITests | PuttingUserWithChangedUsernameWithSpecialCharacterThrowsException | Modifying username to username with special character throws exception | 2025-01-11 | 590 ms | Passed |
| I/59 | Automated | Integration | UserAPITests | PuttingUserWithDuplicatedUsernameThrowsAException | Modifying username to duplicated username throws exception | 2025-01-11 | 466 ms | Passed |
| I/60 | Automated | Integration | UserAPITests | PuttingUserWithImproperChangedPasswordAndProperChangedUsernameThrowsException | Modifying username and password to proper username but improper password throws exception | 2025-01-11 | 600 ms | Passed |
| I/61 | Automated | Integration | UserAPITests | PuttingUserWithProperChangedPasswordAndImproperChangedUsernameThrowsException | Modifying username and password to proper password but improper username throws exception | 2025-01-11 | 1100 ms | Passed |
| I/62 | Automated | Integration | UserAPITests | UpdatingNonExistingIdThrowsException | Modifying non existing user throws exception | 2025-01-11 | 295 ms | Passed |
| I/63 | Automated | Integration | UserAPITests | UserCanDeleteOwnData | Deleting own data as logged in user runs without any issue | 2025-01-11 | 240 ms | Passed |
| I/64 | Automated | Integration | UserAPITests | UserCanGetOwnUserData | Accessing own data as logged in user runs without any issue | 2025-01-11 | 1100 ms | Passed |
| I/65 | Automated | Integration | UserAPITests | UserCannotDeleteOtherUserData | Deleting other user's data as logged in user throws exception | 2025-01-11 | 582 ms | Passed |
| I/66 | Automated | Integration | UserAPITests | UserCannotGetOtherUsersData | Accessing other user's data as logged in user throws exception | 2025-01-11 | 815 ms | Passed |
| I/67 | Automated | Integration | UserAPITests | UserCannotUpdateOtherUserData | Updating other user's data as logged in user throws exception | 2025-01-11 | 445 ms | Passed |
| I/68 | Automated | Integration | UserAPITests | UserCanUpdateOwnData | Updating own user's data as logged in user runs without any issue | 2025-01-11 | 468 ms | Passed |
| I/69 | Automated | Integration | PackageSizeAPITests | AdminRoleNeededToCreatePackageSize | Trying to post a package size without admin role throws Forbidden error | 2025-01-11 | 206 ms | Passed |
| I/70 | Automated | Integration | PackageSizeAPITests | AdminRoleNeededToDeletePackageSize | Trying to delete a package size without admin role throws Forbidden error | 2025-01-11 | 219 ms | Passed |
| I/71 | Automated | Integration | PackageSizeAPITests | AdminRoleNeededToEditPackageSize | Trying to update a package size without admin role throws Forbidden error | 2025-01-11 | 269 ms | Passed |
| I/72 | Automated | Integration | PackageSizeAPITests | CannotAddDuplicatePackageSize | Trying to post two package sizes with the same size will result in an exception | 2025-01-11 | 165 ms | Passed |
| I/73 | Automated | Integration | PackageSizeAPITests | CannotAddPackageSizeToNonExistantMedicine | Trying to add a package size with a medicine id that cannot be found throws an exception | 2025-01-11 | 132 ms | Passed |
| I/74 | Automated | Integration | PackageSizeAPITests | CannotCreatePackageSizeWithSizeLessThanOne | Trying to add a package size a size not greater than zero will result in a validation error | 2025-01-11 | 135 ms | Passed |
| I/75 | Automated | Integration | PackageSizeAPITests | CannotDeleteNonExistantPackageSize | Trying to delete a package size that cannot be found in the database throws a Not Found exception | 2025-01-11 | 133 ms | Passed |
| I/76 | Automated | Integration | PackageSizeAPITests | CannotEditNonExistantPackageSize | Trying to update a package size that cannot be found in the database throws a Not Found exception | 2025-01-11 | 154 ms | Passed |
| I/77 | Automated | Integration | PackageSizeAPITests | CannotEditToDuplicatePackageSize | Trying to update a package size to a size that has already been added to the given medicine throws a validation error | 2025-01-11 | 159 ms | Passed |
| I/78 | Automated | Integration | PackageSizeAPITests | CannotEditToInvalidPackageSize | Trying to update a package size to a size that is not greater than zero will result in a validation error | 2025-01-11 | 149 ms | Passed |
