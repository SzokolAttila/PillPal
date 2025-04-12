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
- Go into `/home/neu/PillPal/PillPalWebApp/src/utils/http.mjs` and change the baseURL of the http object
- Open `/home/neu/PillPal/PillPalLib/APIHandlers/APIHandlerBase.cs` and change the default value of the baseURL in the constructor

And you're good to go :D