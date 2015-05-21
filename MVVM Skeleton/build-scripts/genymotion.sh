#!/bin/bash

api_version="API ${1}"

num_processes=`ps -ef | grep "/Applications/Genymotion\.app/Contents/MacOS/player" | awk '{print $1}' | wc -l`

#check if adb is connected to it
if [ $num_processes != '0' ]; then
    num_devices=`~/Library/Developer/Xamarin/android-sdk-macosx/platform-tools/adb devices -l | grep "API_${1}" | wc -l`

    #if not connected, find vm with version and open it
    if [ $num_devices == '0' ]; then
        echo "ADB is not connected to the open emulator. Restarting it."
        `~/Library/Developer/Xamarin/android-sdk-macosx/platform-tools/adb kill-server`
        `~/Library/Developer/Xamarin/android-sdk-macosx/platform-tools/adb start-server`
    fi
else
    vm_name=`/Applications/VirtualBox.app/Contents/MacOS/VBoxManage list vms | grep "$api_version" | awk '{split($0,array,"{"); print array[1]}'`

    vm_name=`echo "$vm_name" | awk {'split($0, array,"\""); print array[2]'}`
    echo $vm_name
    /Applications/Genymotion.app/Contents/MacOS/player --vm-name "${vm_name}"&
fi
