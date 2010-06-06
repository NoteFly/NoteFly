#!/bin/bash

if [ -d ~/.NoteFly ]
then	
	rm ~/.NoteFly/settings.xml
	echo "NoteFly settings deleted."
else
	echo ".NoteFly folder cannot be found."
fi
