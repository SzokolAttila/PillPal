# PillPal

## Devtunnel

### Prequisites

To install devtunnel run the following line `curl -sL https://aka.ms/DevTunnelCliInstall | bash`
For logging in your GitHub account, you'll need a web page tool like `xdg`.
To install it run the following line `sudo apt install xdg-utils`
Now you can easily log in using `devtunnel login -g`

### Create and run devtunnel

First off, create a devtunnel using `devtunnel create <devtunnel-name>`
Then to attach a port to it, run `devtunnel port create <devtunnel-name> -p 5236 --protocol http`
Now, all you need to do is this line `devtunnel host <devtunnel-name>.euw --allow-anonymous`

### Copy the URL
As the API is now accesible via the devtunnel, you should copy the URL you get when hosting the devtunnel.
- Go into `/PillPalWebApp/src/utils/http.mjs` and change the baseURL of the http object
- Open `/PillPalLib/APIHandlers/APIHandlerBase.cs` and change the default value of the baseURL in the constructor (Beware: your mobile app needs to be run out of the virtual machine, so change the file in the cloned repository outside of the virtual machine!)

## Docker containers

### Starting up

To start all the containers, all you have to do is running `start.sh`. The script will start up all the services in detached mode and then start only the API with not detaching it, which means it will fill your terminal so you can see when the API is started up.
**Note:** If your MSSQL service end up as unhealty, try out a bigger number for `start_period` in `compose.yaml`.

### Stopping

To stop the services you have to push a `CTRL+C` and run `docker compose down`. To remove them instead run `docker compose down -v`.

## Mobile application

The mobile application is somehow independent from the other parts of this project. The mobile app tries to request from the devtunnel, so you need to run all the things written above before trying out the mobile application. We suggest cloning the repository outside of virtual machine and then oppening the whole solution (`/PillPal.sln`) with Visual Studio 2022. Then you can either run it on an emulator or connect your own mobile device.

And you're good to go :D
