using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WindowsInput.Native;

namespace UCT_v3
{
    class KeyBindingsHandler
    {
        private string keyBindingFilePath;
        private Dictionary<string, VirtualKeyCode> LtoVK = new Dictionary<string, VirtualKeyCode>();

        private class ButtonConfig
        {
            public string gameTitle { get; set; }
            public Dictionary<string, string> CmdtoL { get; set; } 
        }

        public KeyBindingsHandler() { 
            LtoVK.Add("A", VirtualKeyCode.VK_A); 
            LtoVK.Add("B", VirtualKeyCode.VK_B); 
            LtoVK.Add("C", VirtualKeyCode.VK_C); 
            LtoVK.Add("D", VirtualKeyCode.VK_D);
            LtoVK.Add("E", VirtualKeyCode.VK_E);
            LtoVK.Add("F", VirtualKeyCode.VK_F);
            LtoVK.Add("G", VirtualKeyCode.VK_G);
            LtoVK.Add("H", VirtualKeyCode.VK_H);
            LtoVK.Add("I", VirtualKeyCode.VK_I);
            LtoVK.Add("J", VirtualKeyCode.VK_J);
            LtoVK.Add("K", VirtualKeyCode.VK_K);
            LtoVK.Add("L", VirtualKeyCode.VK_L);
            LtoVK.Add("M", VirtualKeyCode.VK_M);
            LtoVK.Add("N", VirtualKeyCode.VK_N);
            LtoVK.Add("O", VirtualKeyCode.VK_O);
            LtoVK.Add("P", VirtualKeyCode.VK_P);
            LtoVK.Add("Q", VirtualKeyCode.VK_Q);
            LtoVK.Add("R", VirtualKeyCode.VK_R);
            LtoVK.Add("S", VirtualKeyCode.VK_S);
            LtoVK.Add("T", VirtualKeyCode.VK_T);
            LtoVK.Add("U", VirtualKeyCode.VK_U);
            LtoVK.Add("V", VirtualKeyCode.VK_V);
            LtoVK.Add("W", VirtualKeyCode.VK_W);
            LtoVK.Add("X", VirtualKeyCode.VK_X);
            LtoVK.Add("Y", VirtualKeyCode.VK_Y);
            LtoVK.Add("Z", VirtualKeyCode.VK_Z);
            LtoVK.Add("1", VirtualKeyCode.VK_1);
            LtoVK.Add("2", VirtualKeyCode.VK_2);
            LtoVK.Add("3", VirtualKeyCode.VK_3);
            LtoVK.Add("4", VirtualKeyCode.VK_4);
            LtoVK.Add("5", VirtualKeyCode.VK_5);
            LtoVK.Add("6", VirtualKeyCode.VK_6);
            LtoVK.Add("7", VirtualKeyCode.VK_7);
            LtoVK.Add("8", VirtualKeyCode.VK_8);
            LtoVK.Add("9", VirtualKeyCode.VK_9);
            LtoVK.Add("0", VirtualKeyCode.VK_0);
            LtoVK.Add("num1", VirtualKeyCode.NUMPAD1);
            LtoVK.Add("num2", VirtualKeyCode.NUMPAD2);
            LtoVK.Add("num3", VirtualKeyCode.NUMPAD3);
            LtoVK.Add("num4", VirtualKeyCode.NUMPAD4);
            LtoVK.Add("num5", VirtualKeyCode.NUMPAD5);
            LtoVK.Add("num6", VirtualKeyCode.NUMPAD6);
            LtoVK.Add("num7", VirtualKeyCode.NUMPAD7);
            LtoVK.Add("num8", VirtualKeyCode.NUMPAD8);
            LtoVK.Add("num9", VirtualKeyCode.NUMPAD9);
            LtoVK.Add("num0", VirtualKeyCode.NUMPAD0);
        }

        private void loadJSON(string keypath)
        {
            //ButtonConfig buttonConfig = JsonConvert.DeserializeObject<ButtonConfig>(JSONinputstring);
        }
    }
}
