# PillPal User's Manual

## Admin webapp

We made a user-friendly webapp using Vue for the administrator so they can manipulate the other users and the all the data connected to medicines.

### Login page
This page is quite self-explanatory, it is for checking whether you have the proper access level and have an account. In case you log out, or your account gets deleted while you're logged in, the webapp redirects you to this page so you need to log in again. You need to type in your username and the correct password to it, so try to remind these two data.

### Navbar
As you may have used to navbars on webpages we also included one to make the navigation easier and quicker. We implemented a collapsible sidebar so the page works well on any devices. 

### Users page
Sometimes users want to create a whole new account, delete an existing one or behave in an inappropriate way. To handle these scenarios the administrator can delete users. The page shows all the users to the administrator in a list. The page only shows the username, the number of the user's reminders and a remove button in case you want to remove a specific user. This page also contains a searchbar to make the finding process easier. If you know the username, you can easily find the user by their username. Clicking on the remove button shows a confirm popup to prevent accidental user deletion. 

### New medicine
If you want to extend the variety of medicines they can easily do so by filling out the form on this page and saving it with clicking the button. All the fields are easily understandable and the validation messages help you fill out the form with the proper data. To add a new package unit which doesn't exist you can do to via the *New medicine data page*. The page sends a feedback if the medicine was successfully added. 
**Beware, that this page only creates the medicine! To add some more data to a medicine you might use the edit medicine data page.**

### Edit medicine page
Everyone makes mistakes, to correct those mistakes the admin can edit, delete or extend the existing medicines with more data. The first thing you'll notice is the searchbar. To find a specific medicine you can just type in its name and it will show the wanted medicine. All its data will immediately load into the form and show in the other four sections. In the form you can freely modifiy the data of the medicine just like in the new medicine page's form. To save the changes you need to click on the modify button. In case a medicine goes out of pharmacies, you can delete it by clicking on the delete button instead, then it will ask for confirmation and delete it. In case you want to modify the medicine's side effects, active ingredients, ailments (which it cures) or package sizes, or you just want to add these initially, you can do so. So each section contains the already added elements in a list and an add button. Each item contains an input field for the value and a remove button, so you can erase any of these data if you want to. Clicking on the add button will add a new item to the bottom of the list with a default value. Changing the value will immediately update it in the database. Clicking on the remove button will remove the row from the page and the database as well.
- **Side effects section:** there you can add new side effects. The input field is a dropdown which contains the existing options which you can add to via the *New medicine data page*.
- **Active ingredients section:** there you can add new active ingredients. The input field is a dropdown which contains the existing options which you can add to via the *New medicine data page*.
- **Ailments section:** there you can add new ailments which the medicine is remedy for. The input field is a dropdown which contains the existing options which you can add to via the *New medicine data page*.
- **Package sizes section:** there you can add new package sizes. The input field is a number input which must be a unique data for that medicine.

### New medicine data page
As it was mentioned previously there are some scenarios where you might need to add options to package units, side effects, active ingredients or ailments. For this purpose this page contains four sections. Each section has a search bar for finding an item by its name. Clicking on a specific item from the list will switch the view below. The default view is an input field and an add button. Its working is simple, the written item in the input will be added to the database by clicking the button. The switched view is for modifying or deleting an existing item. If you want to edit it, just change the value loaded into the input field and click on the modify button. In case you want to remove the item, click on the remove button which will ask you for confirmation to prevent accidental deletions. If you want to switch back to the other view, just click on the cancel button which will unselect the item in the item list. So there is an independent section for side effects, active ingredients, ailments (which the medicine cures) and package units. 