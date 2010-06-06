#!/bin/bash

#Copy and rename the assembly in the linux folder under the release folder.
cp ./../bin/Release/NoteFly.exe  ./../bin/Release/linux/notefly

#Make sure execute bit is set.
chmod u+x ./../bin/Release/linux/notefly
