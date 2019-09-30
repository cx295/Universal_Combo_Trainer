
# coding: utf-8

# Combo Trainer

# In[ ]:


from playsound import playsound
import keyboard
import pandas
import os
import time
import json
from PyQt5.QtWidgets import QApplication, QLabel

combo_file_location_path = os.path.join(os.path.pardir, 'ComboTrainer', 'combos')
#combo_data_file_path = os.path.join(combo_file_location_path, 'IAD.json')
#combo_data_file_path = os.path.join(combo_file_location_path, 'dbfz_universal_bnb.json')
combo_data_file_path = os.path.join(combo_file_location_path, 'dbfz_vanish_into_2M.json')

print(combo_data_file_path)

sound_file_location_path = os.path.join(os.path.pardir, 'ComboTrainer', 'resources')
sound_file_path = os.path.join(sound_file_location_path, 'click.wav')

print(sound_file_path)

keyconfig_file_location_path = os.path.join(os.path.pardir, 'ComboTrainer', 'keyconfig')
keyconfig_file_path = os.path.join(keyconfig_file_location_path, 'DragonBallFighterZ.json')

print(keyconfig_file_path)

class Timing:
    
    def __init__(self,i):
        self.scaling = i
    
    def get_scaling(self):
        return self.scaling
    
    def set_scaling(self,i):
        self.scaling = i
    
    def frame_to_seconds(self,frames):
        return frames/60 * self.scaling
    
    def wait_frames(self,frames):
        time.sleep(self.frame_to_seconds(frames))
        
class KeyboardAction:
    def __init__(self,key_to_press):
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
       
class TapHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.key = combodict['key']
        self.bip = combodict['bip']
        self.inwait = combodict['in']
        self.afterwait = combodict['aft']
        
    def tap(self,keyarray,sound,inwait,afterwait):
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
            
    def do_actions(self):
        tap(key, int(bip), int(inwait), int(afterwait))

class SlurHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.dirs = combodict['dirs']
        self.key = combodict['key']
        self.bip = combodict['bip']
        self.inwait = combodict['in']
        self.afterwait = combodict['aft']
    
    def slur(directionarray, keyarray, sound, inwait, afterwait):
        for i in range(len(directionarray)+1):
            if i<len(directionarray):
                keysBound[directionarray[i]].pressKey()
                if inwait > 0:
                    t.wait_frames(inwait)
            if i>0 and i != len(directionarray):
                keysBound[directionarray[i-1]].releaseKey()
        tap(keyarray,sound,inwait,0)
        keysBound[directionarray[len(directionarray)-1]].releaseKey()
        
    def do_action(self):
        slur(dirs, key, int(bip), int(inwait), int(afterwait))
        
class PressHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.key = combodict['key']
        self.afterwait = combodict['aft']
        
    def do_action(self):
        keysBound[key].pressKey()
        t.wait_frames(int(afterwait))

class ReleaseHandler(TechniqueHandler):
    def __init__(self, combodict):
        self.key = combodict['key']
        self.afterwait = combodict['aft']
        
    def do_action(self):
        keysBound[key].releaseKey()
        t.wait_frames(int(afterwait))
        
#get key configuration file and pack it into jsonfile library
with open(keyconfig_file_path, 'r') as f:
    jsonfile = json.load(f)

#build library of command: keypress objects
keysBound = {}
for move, letter in jsonfile["buttonConfig"].items():
    keysBound[move] = KeyboardAction(letter)

def play_sound(i):
    playsound(sound_file_path)        

t=Timing(1)

with open(combo_data_file_path, 'r') as f:
    combofile = json.load(f)    

keyboard.wait('esc')

for i in range(len(combofile["combo"])):
    for technique in combofile["combo"][i]:
        if technique == 'tap':
            #build a tap combo with combofile["combo"][i]['tap']
            
        if technique == 'slur':
            #build a slur combo with combofile["combo"][i]['slur']
            
        if technique == 'press':
            #build a press with combofile["combo"][i]['press']
            
        if technique == 'release':
            #build a release with combofile["combo"][i]['slur']['release']

