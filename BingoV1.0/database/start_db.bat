if not exist "C:\mongo_database\bingo" mkdir "C:\mongo_database\bingo"
start cmd.exe @cmd /k "mongod  --dbpath c:/mongo_database/"
mongo --nodb load.js

REM To start mongo in ssl mode, use the command below substituting appropriate path to certs on your machine
REM start cmd.exe @cmd /k "mongod  --dbpath c:/mongo_database/ --sslMode requireSSL --sslPEMKeyFile c:/MakeCert/New/TY1LTCNU238B0QJ.pem --sslCAFile c:/MakeCert/New/ca-chain.cert.pem"