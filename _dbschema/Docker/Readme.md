sudo docker build -t mssql:dev .
sudo docker run --name mssql-dev -d -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password1!' -p 1433:1433 mssql:dev