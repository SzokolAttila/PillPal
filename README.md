# PillPal

## Devtunnel

### Prequisites

To install devtunnel run the following line `curl -sL https://aka.ms/DevTunnelCliInstall | bash`
For logging in your GitHub account, you'll need a web page tool like `xdg`.
To install it run the following line `sudo apt install xdg-utils`
Now you can easily log in using `devtunnel user login -g -d`

### Create and run devtunnel

First off, create a devtunnel using `devtunnel create <devtunnel-name>`
Then to attach a port to it, run `devtunnel port create <devtunnel-name> -p 5236 --protocol http`
Now, all you need to do is this line `devtunnel host <devtunnel-name>.euw --allow-anonymous`

And you're good to go :D

## MSSQL Server

### Make database persistent

All the data is related to MSSQL Server is stored in the `mssql-data` directory and it is mounted on the `mssql` service.
As it wouldn't have edit access in the container, you need to change the owner with `sudo chown -R 10001:0 ./mssql-data`.
It is also necessary to give permission to edit it in the container, so run `chmod -R 770 ./mssql-data`.