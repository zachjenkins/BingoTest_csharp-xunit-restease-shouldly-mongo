#!bin/bash

if [ ! -d /monogo_database/bingo ]
then
  sudo mkdir -p /mongo_database/bingo
  sudo chmod -R 777 /mongo_database/bingo
fi
osascript -e 'tell app "Terminal"
    do script "mongod --dbpath /mongo_database/bingo"
end tell'
mongo --nodb load.js
