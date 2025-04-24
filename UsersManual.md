# PillPal User's Manual

## Mobile application
Elder people usually need to take in aroud 8 medicines daily. Keeping them in mind and arranging them properly can be a really hard task. Not only that they need to remind to take in the meds they also need to regularly remember all those information about the medicines. To help these people in need we are continously developing this application. We have bursts of ideas to improve this fundamental app, so stay tuned!

### What we do BETTER
Our application is for creating reminders of taking in meds, just as the other similar apps, but not just that! Users can select the medicine from an already filled database, so they can be easily informed about they meds without adding any difficult data manually.
- **Easy-to-handle reminders ✔️**
- **Accessible medicine infos 💊**
- **Opportunities to revolutionize medicine reminder apps 🚀**

### Account handling
- Every reminder is connected to an account, so they are independent of any device, so if you for example change your mobile, it will still has all your reminders.
- When you are logged out (or at least not logged in) the application automatically starts up at the login page, so you can immediately log in.
- If you don't have an account, you can easily register one by a unique username, and a password, which is secure enough.

### Menu button
To make it easier for you to navigate between the different pages, the app has a circular radius menu button. Which has the following options:
- Adding a new reminder ➕
- Your reminders home page 🏠
- Application settings ⚙️

### Seeing your reminders
- The main page shows you all your reminders, with some simplified cards.
- They are ordered by time, and the first one is always the closest (which is not yet fired this day), to make it easier for you to find the most relevant reminder.
- Each card shows you the schedule time, the medicine itself, the dose you should take in, and the method of taking it.
- A reminder card also contains three buttons. You can either *delete* an unwanted reminder, correct something about it by tapping on *edit* button, or get to know it better by tapping on the *info* button.

### Editing a reminder
On your reminders home page you can click on a reminder card's edit button to make some changes.
- This screen contains a search bar with a medicine list below, where you can find a medicine and select it if you want to change the medicine to take in.
- In the section beneath this you can edit the dose, the schedule time and even the take in method.
- To save the changes, you need to tap on the *save* button, which leads you back to the home page on success.
- If you change your mind and don't want to make those changes, you can simply tap the *back* button, which leads you back to the home page and discards the modifications.

### Infos about a medicine
Sometimes you need to care about the possible side effects or the active ingredients of a medicine, or read about what a medicine is good for. To get all this data you would need to look for the description which you probably threw out years ago. With this app you can gain all these useful information easily without wasting any time. Here you can find all the needed info in a well-structured way.

### Creating a new reminder
The basis of this app are the reminders, so there is a page for creating them. This page is built up similarly to edit page.  
- This screen contains a search bar with a medicine list below, where you can find a medicine and select which medicine you need to take in.
- In the section beneath this you can set the dose, the schedule time and even the take in method.
- To add the new reminder, you need to tap on the *add* button, which leads you back to the home page on success.

### Application settings
This page only contains a few things for now but will have more attention in the long run. Here you can do the following:
- Switch between dark mode and light mode
- Log out of your account, so you can log into another
- Remove your account if you don't need it anymore or you want reset it

## Admin webapp

We made a user-friendly webapp using Vue for the administrator so they can manipulate the other users and the all the data connected to medicines.

### Login page
This page is quite self-explanatory, it is for checking whether you have the proper access level and have an account. In case you log out, or your account gets deleted while you're logged in, the webapp redirects you to this page so you need to log in again. You need to type in your username and the correct password to it, so try to remind these two data.

### Navbar
As you may have used to navbars on webpages we also included one to make the navigation easier and quicker. We implemented a collapsible sidebar so the page works well on any devices. 

### Users page
Sometimes users behave in an inappropriate way. To handle these scenarios the administrator can delete users. The page shows all the users to the administrator in a list. The page only shows the username, the number of the user's reminders and a remove button in case you want to remove a specific user. This page also contains a searchbar to make the finding process easier. If you know the username, you can easily find the user by their username. Clicking on the remove button shows a confirm popup to prevent accidental user deletion. 

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