8/29/2024
Jared Holston

This folder contains the config files for the server.

The directories they belong in and commands to deploy them are listed here.


Apache2 Web Server:
    recipe-builder.conf -> /etc/apache2/sites-available
                    sudo a2ensite recipe-builder
                    sudo a2enmod mod_proxy


Neo4j Database Server:
    (Complex, needs to be filled in later.)

Server Certificates:    
    I piggy-backed the existing certs I had on my server to get us started.
    Ideally, use the acme tool from Let's Encrypt to help create a certificate signing request and deploy the certificate to the web server configuration.
    There will likely be more to modify for ASP.net to utilize SSL. It's also possible that apache2 is already tunneling the communication and it doesn't matter.
