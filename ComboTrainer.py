# coding: utf-8

# Combo Trainer

# In[ ]:

from playsound import playsound
import keyboard
import os
import time
import json

from PyQt5.QtWidgets import QApplication, QLabel, QWidget, QPushButton, QVBoxLayout

#app = QApplication([])
#window = QWidget()
#layout = QVBoxLayout()
#layout.addWidget(QPushButton('Top'))
#layout.addWidget(QPushButton('Bottom'))
#window.setLayout(layout)
#label = QLabel('Hello World')
#label.show()
#window.show()
#app.exec_()

combo_file_location_path = os.path.join(os.path.pardir, 'Universal_Combo_Trainer', 'combos')
#combo_data_file_path = os.path.join(combo_file_location_path, 'IAD.json')
#combo_data_file_path = os.path.join(combo_file_location_path, 'dbfz_universal_bnb.json')
#combo_data_file_path = os.path.join(combo_file_location_path, 'Fireball.json')
combo_data_file_path = os.path.join(combo_file_location_path, 'dbfz_vanish_into_2M.json')

print(combo_data_file_path)

sound_file_location_path = os.path.join(os.path.pardir, 'Universal_Combo_Trainer', 'resources')
sound_file_path = os.path.join(sound_file_location_path, 'click.wav')

print(sound_file_path)
keyconfig_file_location_path = os.path.join(os.path.pardir, 'Universal_Combo_Trainer', 'keyconfig')
keyconfig_file_path = os.path.join(keyconfig_file_location_path, 'DragonBallFighterZ.json')

print(keyconfig_file_path)


class Timing:

    def __init__(self, i):
        self.scaling = i

    def get_scaling(self):
        return self.scaling

    def set_scaling(self, i):
        self.scaling = i

    def frame_to_seconds(self, frames):
        return frames / 60 * self.scaling


class TechniqueHandler:

    def __init__(self):

        pass

    def tap(self, keyarray, sound, inwait, afterwait):
        tapArray = []
        for key in keyarray:
            tapArray.append("keyboard.press('" + str(keysBound[key]) + "')")
        if sound == 1:
            tapArray.append("playsound(sound_file_path)")
        if inwait > 0:
            tapArray.append("time.sleep(" + str(t.frame_to_seconds(inwait)) + ")")
        for key in keyarray:
            tapArray.append("keyboard.release('" + str(keysBound[key]) + "')")
        if afterwait > 0:
            tapArray.append("time.sleep(" + str(t.frame_to_seconds(afterwait)) + ")")

        return tapArray


class TapHandler(TechniqueHandler):

    def __init__(self, combodict):
        self.key = combodict['key']
        self.bip = combodict['bip']
        self.inwait = combodict['in']
        self.afterwait = combodict['aft']

    def get_action(self):
        return super().tap(self.key, int(self.bip), int(self.inwait), int(self.afterwait))


class SlurHandler(TechniqueHandler):

    def __init__(self, combodict):
        self.dirs = combodict['dirs']
        self.key = combodict['key']
        self.bip = combodict['bip']
        self.inwait = combodict['in']
        self.afterwait = combodict['aft']

    def slur(self, dirs, key, bip, inwait, afterwait):
        slurArray = []
        for i in range(len(self.dirs) + 1):
            if i < len(self.dirs):
                slurArray.append("keyboard.press('" + str(keysBound[self.dirs[i]]) + "')")
            if inwait > 0:
                slurArray.append("time.sleep(" + str(t.frame_to_seconds(inwait)) + ")")
            if i > 0 and i != len(dirs):
                slurArray.append("keyboard.release('" + str(keysBound[self.dirs[i-1]]) + "')")
        slurArray.extend(super().tap(self.key, int(self.bip), int(self.inwait), 0))
        slurArray.append("keyboard.release('" + str(keysBound[self.dirs[len(dirs) - 1]]) + "')")

        return slurArray

    def get_action(self):
        return self.slur(self.dirs, self.key, int(self.bip), int(self.inwait), int(self.afterwait))

class PressHandler(TechniqueHandler):

    def __init__(self, combodict):
        self.key = combodict['key']
        self.afterwait = combodict['aft']

    def get_action(self):
        pressArray = []
        pressArray.append("keyboard.press('" + str(self.key) + "')")
        pressArray.append("time.wait(" + str(self.afterwait) + ")")

        return pressArray

class ReleaseHandler(TechniqueHandler):

    def __init__(self, combodict):
        self.key = combodict['key']
        self.afterwait = combodict['aft']

    def get_action(self):
        releaseArray = []
        releaseArray.append("keyboard.release('" + str(self.key) + "')")
        releaseArray.append("time.wait(" + str(self.afterwait) + ")")

        return releaseArray


# get key configuration file and pack it into jsonfile library
with open(keyconfig_file_path, 'r') as f:
    jsonfile = json.load(f)

# build library of command: keypress objects
keysBound = {}
for move, letter in jsonfile["buttonConfig"].items():
    keysBound[move] = letter

t = Timing(1)

with open(combo_data_file_path, 'r') as f:
    combofile = json.load(f)
    print("combofile is: ", combofile)
    print("length of combofile[combo] is: ", len(combofile["combo"]))

actionsarray = []
for i in range(len(combofile["combo"])):
    for technique in combofile["combo"][i]:
        if technique == 'tap':
            #print('calling TapHandler')
            actionsarray.append(TapHandler(combofile["combo"][i]['tap']))

        if technique == 'slur':
            #print('calling SlurHandler')
            actionsarray.append(SlurHandler(combofile["combo"][i]['slur']))

        if technique == 'press':
            #print('calling PressHandler')
            actionsarray.append(PressHandler(combofile["combo"][i]['press']))

        if technique == 'release':
            #print('calling ReleaseHandler')
            actionsarray.append(ReleaseHandler(combofile["combo"][i]['release']))

outputScriptArray = []
for i in actionsarray:
    outputScriptArray.extend(i.get_action())
print("ready")
keyboard.wait('esc')

for j in outputScriptArray:
    exec(j)
