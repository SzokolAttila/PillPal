# PillPal Test Plan

### User tests (password hashing)

| Scope  | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Unit test | User's hash is unique | Create two users with the same password but different usernames | Their hashed passwords don't match | True |
| Unit test | Password hashing is consistent | Create two users with the same password and same usernames | Their hashed passwords match | True |

### IDCollection tests (CRUD methods)

| Scope | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Component test | Indexer finds existing items | Initialize an IDCollection with a medicine | Index the first and the second item | First exists; second is null |
| Component test | Items can be added | Initialize an empty IDCollection with zero items | Check the Count; add an item then check again | get 0; then get 1 |
| Component test | Items cannot be added twice | Initialize an empty IDCollection and generate a medicine to add | Adding that medicine and check return; add it again and check return | first returns true; second returns false |
| Component test | Items can be removed | Initialize an IDCollection with an item | Remove at existing id; remove at non existing id; check count | returns true; returns false; count is 0 |
| Component test | Existing item can be updated | Initialize empty IDCollection and add medicine to it; Changing medicine name and replacing it | Get the medicine's name from IDCollection | Medicine's name is the changed name |
| Component test | Updating or deleting non existing item returns false | Create an empty IDCollection | Remove from empty collection; updating empty collection | both returns false |

### MedicineAPI tests (Authorization and validation)

| Scope | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Integration test | Posting as anonymous or user throws argument exception | Create user token; create medicine | Post medicine as user; post medicine as anonyomous | Throws exception with 'Forbidden' message; throws exception with 'Unauthorized' message |
| Integration test | Putting as anonymous or user throws argument exception | Create user token; create admin token; create and post medicine | Put to posted medicine as user; put to posted medicine as anonyomous | Throws exception with 'Forbidden' message; throws exception with 'Unauthorized' message |
| Integration test | Deleting as anonymous or user throws argument exception | Create user token; create admin token; post medicine | Delete posted medicine as user; delete posted medicine as anonyomous | Throws exception with 'Forbidden' message; throws exception with 'Unauthorized' message |
| Integration test | Putting to non existing id throws argument exception | Create admin token; create medicine and post it | Putting medicine to non existing id | Throws exception with 'Not Found' message |
| Integration test | Deleting non existing id throws argument exception | Create admin token; create medicine and post it | Deleting medicine at non existing id | Throws exception with 'Not Found' message |
| Integration test | Posting new medicine increments number of medicines | Create admin token; create 5 medicines and post them | GetAll medicines and check Count | Count returns 5 |
| Integration test | Deleting medicine lowers the number of medicines | Create admin token; create 5 medicines and post them; delete 2 medicines with existing id | GetAll medicines and check Count | Count returns 3 |
| Integration test | Updating the name changes it | Create admin token; create medicine and post it | Check if medicine's name is the new one; update medicine with changed name; check if medicines name is the new one | returns false; returns true |
| Integration test | Posting or putting medicine with short or long name throws exception | Create admin token; create medicine and post it | Post and put medicine with short name; post and put medicine with long name | All of them throws exception with the validator's message |
| Integration test | Posting or putting medicine with short or long manufacturer name throws exception | Create admin token; create medicine and post it | Post and put medicine with short manufacturer name; post and put medicine with long manufacturer name | All of them throws exception with the validator's message |
| Integration test | Posting or putting medicine with not mg or ml package unit throws exception | Create admin token; create medicine and post it | Post and put medicine with a different unit | All of them throws exception with the validator's message |

### ReminderAPI tests (validator and authorization)

| Scope  | Description | Preparations | Actions | Expected result |
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

### UserAPI tests (Authorization and validation)
| Scope  | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Integration test | Creating user with unique user and proper password returns true | Create a user | Post proper user to the API | Runs without throwing exception |
| Integration test | Creating user with short username throws argument exception | Create user with short username | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating user with special characters in username throws argument exception | Create user with special character username | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating user with long username throws argument exception | Create user with long username | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating duplicated user throws argument exception | Create proper user | Post the user to the API twice | The second post throws argument exception with validator's message |
| Integration test | Creating user with short password throws argument exception | Create user with short password | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating user with no lowercase in password throws argument exception | Create user with no lowercase character in password | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating user with no uppercase in password throws argument exception | Create user with no uppercase character in password | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating user with no number in password throws argument exception | Create user with no number in password | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Creating user with no special in password throws argument exception | Create user with no special character in password | Post the user to the API | Throws argument exception with validator's message |
| Integration test | Login existing user gives back token | Create a proper user and post it to API | Check how long string does Login function give back | returned string's length is more than 0 |
| Intagration test | Invalid user login throws exception | Create non existing user | Call Login function with that new user | Throws argument exception with 'Invalid username or password.' message |
| Integration test | Get all users needs authorization | No prerequisites | Call GetUsers function without jwt token string | Throws argument exception with 'Unauthorized' message |
| Integration test | Admin can get all users | Create admin user, post it and login with it | Call GetUsers and check item count | Item count should be 1 (only the admin) |
| Integration test | Admin can get any user's data | Create admin and a user and post them; login with admin | Get the user's and own data by id with GetUser and check their usernames | User's given username; 'administrator' |
| Integration test | User cannot get other user's data | Create two users, post them and login with the first | Get second user's data by id | Throws argument exception with 'Forbidden' message |
| Integration test | User can get own user data | Create user, post it and login with it | Get own user data by id and check on username | Returned username is the created user's username |
| Integration test | User cannot delete other user | Create two users, post them and login with the first | Delete second user by id | Throws argument exception with message 'Forbidden' |
| Integration test | User can delete own data | Create a user, post it and login with it | GetUser By id; Delete created user by id; GetUser by id again | returns created user; runs without any issues; Throws Argument exception with 'Not Found' message |
| Integration test | Admin can delete any user | Create user and admin, post them and login with admin | Delete user; Check GetUsers count; Delete admin; Check GetUsers count again | Runs without any issues; returns 1; runs without any issues; returns 0 |
| Integration test | Deleting non existing id throws exception | Create a admin, post it and login with it | GetUser By id; Delete at id 2 | returns created admin; Throws Argument exception with 'Not Found' message |
| Integration test | User cannot update other user | Create two users, post them and login with the first | Update second user by id | Throws argument exception with message 'Forbidden' |
| Integration test | User can update own data | Create a user, post it and login with it | GetUser By id; Update created user by id; GetUser by id again | returns created user; runs without any issues; returns modified user |
| Integration test | Admin can update any user | Create user and admin, post them and login with admin | Check GetUser name; Update user; Check GetUser name | returns original username; runs without any issues; returns modified username |
| Integration test | Updating non existing id throws exception | Create admin, post it and login with it | GetUser By id; Update at id 2 | returns created admin; Throws Argument exception with 'Not Found' message |
| Integration test | Putting user with changed duplicated username throws exception | Create admin and user, post them and login with admin | Modify user's username to 'administrator', then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed short username throws exception | Create admin and user, post them and login with admin | Modify user's username to short, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed long username throws exception | Create admin and user, post them and login with admin | Modify user's username to long, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed username with special characters throws exception | Create admin and user, post them and login with admin | Modify user's username to special characters, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed short password throws exception | Create admin and user, post them and login with admin | Modify user's password to short, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed password without uppercase throws exception | Create admin and user, post them and login with admin | Modify user's password to one without uppercase, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed password without lowercase throws exception | Create admin and user, post them and login with admin | Modify user's password to one without lowercase, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed password without numbers throws exception | Create admin and user, post them and login with admin | Modify user's password to one without numbers, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with changed password without special throws exception | Create admin and user, post them and login with admin | Modify user's password to one without special characters, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with proper changed password and improper changed username throws exception | Create admin and user, post them and login with admin | Modify user's password to proper one and username to an improper one, then put it | Throws argument exception with validator's message |
| Integration test | Putting user with improper changed password and proper changed username throws exception | Create admin and user, post them and login with admin | Modify user's password to improper one and username to an proper one, then put it | Throws argument exception with validator's message |

### PackageSizeAPI tests (authorization, validation and join table configuration)
| Scope | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Integration test | Admin Role needed to create PackageSize for medicine | Create an admin user, a simple user and a medicine | Try to post a packageSize first with the user's token, then with the admin token | first attempt will fail with "Forbidden" message, the second one will succeed |
| Integration test | Added package size can be viewed via the medicine | Create an admin user, a medicine and post a packageSize | Get the medicine the package size was added to | Count of packageSizes will be one | 
| Integration test | Cannot create package size if the size is not greater than 0 | Create an admin user and a medicine | Try posting a package size with 0 as size | Throws argument exception saying "Package size has to be greater than 0." |
| Integration test | Cannot add packageSize to a non-existant medicine | Creat an admin user | Try posting a package size without adding a medicine | Throws argument exception saying that the medicine doesn't exist | 
| Integration test | Admin role needed to edit package size | Create an admin user, a simple user, a medicine and a package size | Try putting the edited package size with the user token and then the admin token | First attempt will fail with "Forbidden" message, second attempt will succeed | 
| Integration test | Admin Role needed to delete package size | Create an admin user, a simple user, a medicine and a package size | Try deleting the package size first with the user token and then the admin token | First attempt: Forbidden; second attempt: NoContent |
| Integration test | Cannot edit / delete non-existant package size | Create an admin user | Try to edit / delete the package size without adding it first | Not Found exception | 

### RemedyForAPI tests (authorization, validation and join table configuration (medicineRemedyFor also included))
| Scope  | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| Integration test | Admin role needed to create remedyFor (and medicineRemedyFor) | Create an admin user, a simple user and a medicine | Try adding a remedyFor (and then a medicineRemedyFor) first with user token, then with admin token | Throws a Forbidden error when attempting with user token |
| Integration test | Cannot add remedy if the ailment's length is less than 3 | Create an admin user and a medicine | Try posting a remedyFor with the ailment's length less than 3 | Validation exception will be thrown | 
| Integration test | Cannot add the same ailment twice | Create an admin user and a remedyFor | Try posting the remedyFor twice | The second attempt will throw a validation exception | 
| Integration test | Cannot add remedy to non-existant medicine | Creat an admin user, a remedyFor and a medicineRemedyFor | Try posting the medicineRemedyFor without adding a medicine | Throws exception saying the medicine doesn't exist | 
| Integration test | Cannot add non-existant remedyFor to medicine | Create an admin user, a medicine and a medicineRemedyFor | Try posting the medicineRemedyFor without adding the remedyFor | Exception saying that the RemedyFor doesn't exist. |
| Integration test | Admin role needed to edit / delete remedyFor | Create an admin user, a simple user, a medicine, a remedyFor and a medicineRemedyFor | Try editing / deleting the remedyFor (and the medicineRemedyFor) first with user token, then with admin token | First attempt will throw Forbidden error, the second one will succeed | 
| Integration test | Cannot edit / delete non-existant remedyFor / medicineRemedyFor | Create an admin user | Try to edit / delete a remedyFor / medicineRemedyFor without adding it first | Not Found error | 

### GUI tests (manual testing)
| Scope  | Description | Preparations | Actions | Expected result |
| ------------- |:-------------:|:-------------:|:-------------:|:-------------:|
| System test | Register result will show pop-up window | Open register tab | Register user with invalid data | Pop-up will be shown with the details of the error |
| System test | Register result will show pop-up window | Open register tab | Register user with valid data | Pop-up will be shown with a "Successful registration" text |
| System test | Invalid login makes a pop-up window appear | Open login tab | Try to log in with invalid data | Pop-up will say "Invalid username or password" |
| System test | Valid login will lead user to reminders tab | Open login tab | Log in to existing user with valid data | No pop-up, list of reminders will be displayed |
| System test | Reminder can be added via UI | Register a user and log in | Create a reminder with valid data | Reminder will be added to reminders and shown in the reminders tab |
| System test | Invalid reminder results in pop-up | Register a user and log in | Create a reminder with invalid data | A window will pop up, explaining the problem |
| System test | User can check the description of a medicine | Register a user, log in and add a reminder | Click on the information button of the reminder | A page with the detailed description of the medicine will be displayed |
| System test | User can delete a reminder | Register a user, log in and add a reminder | Click on the delete button of the reminder | Reminder will disappear from the list |
| System test | Edit button of a reminder leads to edit tab | Register a user, log in and add a reminder | Click on the edit button of the reminder | Edit page will be displayed, with the current data of the reminder |
| System test | User can edit a reminder | Register a user, log in, add a reminder and click on edit | Input the new data and click save | Reminder will be listed with the updated data |
| System test | Invalid updated data will result in pop-up | Register a user, log in, add a reminder and click on edit | Input the new invalid data and click save | Pop-up will appear |
| System test | User is notified about a reminder | Register a user, log in and add a reminder | Wait until the time of the reminder | Notification will pop up on the phone's screen |  
| System test | User is can dismiss a reminder | Register a user, log in and add a reminder and wait for it to notify you | Click on "dismiss" | Notification will disappear and won't pop up again until the next day |  
| System test | User is can "snooze" a reminder | Register a user, log in and add a reminder and wait for it to notify you | Click on "snooze" | Notification will disappear and will pop up again a few minutes later |  

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
