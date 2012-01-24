#!/bin/sh

#  NoteFly a note application.
#  Copyright (C) 2012  Tom
#
#  This program is free software: you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation, either version 3 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  along with this program.  If not, see <http://www.gnu.org/licenses/>.


# cleanup
if [ -d ~/notefly-builddeb ]
then
	echo 'cleaning up old notefly compile first.'
	sudo rm -r ~/notefly-builddeb
fi

# Copy and rename lastest release compile of NoteFly
cp ./bin/Release/NoteFly.exe ./builddeb/usr/bin/NoteFly
cp ./bin/Release/langs.xml ./builddeb/usr/bin/langs.xml
cp ./bin/Release/license.txt ./builddeb/usr/share/doc/notefly/copyright

# Move notefly debian build folder to home folder, a NTFS formatted parition
# has permission problems if it's build from there. That's why we copy it to /home/$user/ first.
mkdir ~/notefly-builddeb/
sudo chmod 755 ~/notefly-builddeb/
cp -r ./builddeb ~/notefly-builddeb/notefly-2.5.1_all/
sudo chmod 755 -R ~/notefly-builddeb/

##sudo chmod 644 ~/notefly-builddeb/usr/bin/langs.xml
sudo chmod 644 ~/notefly-builddeb/notefly-2.5.1_all/usr/share/doc/notefly/changelog
sudo chmod 644 ~/notefly-builddeb/notefly-2.5.1_all/usr/share/doc/notefly/copyright

# fix E: notefly: wrong-file-owner-uid-or-gid
sudo chown root:root -R ~/notefly-builddeb/
# building package, root/sudo required for this
sudo dpkg-deb --build ~/notefly-builddeb/notefly-2.5.1_all/

# checking if it's done all right.
lintian -c ~/notefly-builddeb/notefly-2.5.1_all.deb
