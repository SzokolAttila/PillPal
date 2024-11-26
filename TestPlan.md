# PillPal Test Plan

### User tests (password hashing)

| Scope  | Description | Preparations | Asserts | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Unit test | User's hash is unique | Create two users with the same password but different usernames | Their hashed passwords don't match | True |
| Unit test | Password hashing is consistent | Create two users with the same password and same usernames | Their hashed passwords match | True |

### IDCollection tests

| Scope  | Description | Preparations | Asserts | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Unit test | Indexer finds existing items | Initialize an IDCollection with a medicine | Index the first and the second item | First exists; second is null |
| Unit test | Items can be added | Initialize an empty IDCollection with zero items | Check the Count; add an item then check again | get 0; then get 1 |
| Unit test | Items cannot be added twice | Initialize an empty IDCollection and generate a medicine to add | Adding that medicine and check return; add it again and check return | first returns true; second returns false |
| Unit test | Items can be removed | Initialize an IDCollection with an item | Remove at existing id; remove at non existing id; check count | returns true; returns false; count is 0 |
| Unit test | Existing item can be updated | Initialize empty IDCollection and add medicine to it; Changing medicine name and replacing it | Get the medicine's name from IDCollection | Medicine's name is the changed name |
| Unit test | Updating or deleting non existing item returns false | Create an empty IDCollection | Remove from empty collection; updating empty collection | both returns false |

### ReminderAPI tests (validator and authorization)

| Scope  | Description | Preparations | Asserts | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Integration test | Admin can get all reminders | Create admin user | Getting all the reminders returns them in a list | True |
| Integration test | User cannot get all reminders | Create user | Getting all the reminders throws an argument exception | Forbidden |
| Integration test | Admin can get specific user's reminders | Create admin user and user | Getting the user's reminders returns them in a list | True |
| Integration test | User can get their own reminders | Create user | Getting the user's reminders returns them in a list | True |
| Integration test | User cannot get other users' reminders | Create user | Getting other users' reminders throws argument exception | Forbidden |
| Integration test | A reminder's DoseMg cannot be negative or zero | Create a medicine and a reminder that has an incorrect DoseMg value | Trying to post it will throw an argument exception | Cannot add medicine with negative dose |
| Integration test | A reminder's DoseCount cannot be negative or zero | Create a medicine and a reminder that has an incorrect DoseCount value | Trying to post it will throw an argument exception | Cannot add medicine with negative dose |
| Integration test | Cannot add reminder to a user that doesn't exist | Create admin and a reminder that is connected to a non-existant user | Trying to post it will throw an argument exception | User with the given ID doesn't exist. |
| Integration test | Cannot add reminder with a medicine that doesn't exist | Create admin and a reminder that is connected to a non-existant medicine | Trying to post it will throw an argument exception | Medicine with the given ID doesn't exist. |
| Integration test | Admin can add reminder to any user | Create admin user and a user | Getting user's reminders with admin token returns reminders in a list | True |
| Integration test | User cannot add reminder to other users | Create two users | Getting one user's reminders with the other user's token throws an argument exception | Forbidden |
| Integration test | User can add own reminder | Create a user | Trying to post a reminder to the user with the user's token will succeed | True |
| Integration test | Admin can edit any reminder | Create admin user and a user with a reminder | The user's reminder can be edited with the admin's token | True |
| Integration test | Admin can move a reminder from one user to another | Create admin user and two users, one with a reminder | With the admin's token the reminder's "UserId" field can be changed | True |
| Integration test | Cannot edit non-existant reminder | Create admin user | Trying to edit a non-existant reminder will throw an argument exception | Not Found |
| Integration test | User cannot edit other user's reminder | Create two users, one with a reminder | Trying to edit the reminder with the other user's token will throw an argument exception | Forbidden |
| Integration test | User can edit own reminder | Create a user with a reminder | User can edit the details of their own reminder | True |
| Integration test | User cannot move reminder | Create two users, one with a reminder | Trying to edit the "UserId" field of the reminder will throw an argument exception | Forbidden |
| Integration test | Admin can delete any reminder | Create admin user and a user with a reminder | The user's reminder can be deleted with the admin's token | True |
| Integration test | User can delete own reminder | Create a user with a reminder | User can delete their own reminder with their own token | True |
| Integration test | User cannot delet other user's reminder | Create two users, one with a reminder | Trying to delete the user's reminder with other user's token will throw an argument exception | Forbidden |
| Integration test | Cannot delete non-existant reminder | Create admin user | Trying to delete a reminder that doesn't exist will throw an argument exception | Not Found |

Unit tests:
- Password hashing for user
- Add, remove, replace, index for IDCollection

Integration tests:
- API validator
- API CRUD works as expected
- Authentication for API
- Login & registration system

System tests:
- Can add new reminder via UI
- Can edit reminder via UI
- App sends notification for reminding
- Shows the reminders in the right order
- User can register and log in in the application
