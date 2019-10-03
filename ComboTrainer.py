# coding: utf-8

# Combo Trainer

# In[ ]:

from playsound import playsound
import keyboard
import pandas
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
# combo_data_file_path = os.path.join(combo_file_location_path, 'IAD.json')
combo_data_file_path = os.path.join(combo_file_location_path, 'dbfz_universal_bnb.json')
#combo_data_file_path = os.path.join(combo_file_location_path, 'Fireball.json')
#combo_data_file_path = os.path.join(combo_file_location_path, 'dbfz_vanish_into_2M.json')

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

    def wait_frames(self, frames):
        time.sleep(self.frame_to_seconds(frames))


class KeyboardAction:
    def __init__(self, key_to_press):
        self.key_to_press = key_to_press

    def getKey(self):
        return self.key_to_press

    def pressKey(self):
        keyboard.press(self.key_to_press)

    def releaseKey(self):
        keyboard.release(self.key_to_press)


class TechniqueHandler:
    def __init__(self):
        pass

    def tap(self, keyarray, sound, inwait, afterwait):
        for key in keyarray:
            keysBound[key].pressKey()
        if sound == 1:
            play_sound(1)
        if inwait > 0:
            t.wait_frames(inwait)
        for key in keyarray:
            keysBound[key].releaseKey()
        if afterwait > 0:
            t.wait_frames(afterwait)


class TapHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.key = combodict['key']
        self.bip = combodict['bip']
        self.inwait = combodict['in']
        self.afterwait = combodict['aft']

    def do_action(self):
        self.tap(self.key, int(self.bip), int(self.inwait), int(self.afterwait))


class SlurHandler(TechniqueHandler):

    def __init__(self, combodict):
        self.dirs = combodict['dirs']
        self.key = combodict['key']
        self.bip = combodict['bip']
        self.inwait = combodict['in']
        self.afterwait = combodict['aft']

    def slur(self, dirs, key, bip, inwait, afterwait):
        for i in range(len(self.dirs) + 1):
            if i < len(self.dirs):
                keysBound[self.dirs[i]].pressKey()
                if inwait > 0:
                    t.wait_frames(inwait)
            if i > 0 and i != len(dirs):
                keysBound[dirs[i - 1]].releaseKey()
        super().tap(self.key, int(self.bip), int(self.inwait), 0)
        keysBound[dirs[len(dirs) - 1]].releaseKey()

    def do_action(self):
        self.slur(self.dirs, self.key, int(self.bip), int(self.inwait), int(self.afterwait))


class PressHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.key = combodict['key']
        self.afterwait = combodict['aft']

    def do_action(self):
        keysBound[self.key].pressKey()
        t.wait_frames(int(self.afterwait))


class ReleaseHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.key = combodict['key']
        self.afterwait = combodict['aft']

    def do_action(self):
        keysBound[self.key].releaseKey()
        t.wait_frames(int(self.afterwait))


# get key configuration file and pack it into jsonfile library
with open(keyconfig_file_path, 'r') as f:
    jsonfile = json.load(f)

# build library of command: keypress objects
keysBound = {}
for move, letter in jsonfile["buttonConfig"].items():
    keysBound[move] = KeyboardAction(letter)


def play_sound(i):
    playsound(sound_file_path)


t = Timing(1)

with open(combo_data_file_path, 'r') as f:
    combofile = json.load(f)
    print("combofile is: ", combofile)
    print("length of combofile[combo] is: ", len(combofile["combo"]))

keyboard.wait('esc')

actionsarray = []
for i in range(len(combofile["combo"])):
    for technique in combofile["combo"][i]:
        if technique == 'tap':
            print('calling TapHandler')
            actionsarray.append(TapHandler(combofile["combo"][i]['tap']))

        if technique == 'slur':
            print('calling SlurHandler')
            actionsarray.append(SlurHandler(combofile["combo"][i]['slur']))

        if technique == 'press':
            print('calling PressHandler')
            actionsarray.append(PressHandler(combofile["combo"][i]['press']))

        if technique == 'release':
            print('calling ReleaseHandler')
            actionsarray.append(ReleaseHandler(combofile["combo"][i]['release']))

for i in actionsarray:
    i.do_action()
