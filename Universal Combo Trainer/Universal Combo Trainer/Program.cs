using System;
using WindowsInput.Native;
using WindowsInput;
using System.Collections.Generic;

namespace TestProject01
{
    public class TimingHandler
    {
        private float scaling;
        public float Scaling
        {
            get
            {
                return scaling;
            }
            set
            {
                scaling = value;
            }
        }

        public int frame_to_ms(int frames)
        {
            return (int)((frames / 60) * scaling * 1000);
        }
    }

    public class ActionListHandler
    {
        private List<Action> actionList;

        public void addToActionList(Action item)
        {
            actionList.Add(item);
        }

        public void runActionList()
        {
            foreach (Action act in actionList)
            {
                act();
            }
        }
    }

    public class TechniqueHandler
    {
        private ActionListHandler alh;
        private TimingHandler timing;
        private InputSimulator sim = new InputSimulator();
        Dictionary<string, VirtualKeyCode> dict = new Dictionary<string, VirtualKeyCode>();

        public TechniqueHandler(ActionListHandler alhandler, TimingHandler timinghandler)
        {
            alh = alhandler;
            timing = timinghandler;

            dict.Add("A", VirtualKeyCode.VK_A);
        }

        public void press (string key)
        {
            alh.addToActionList(() => sim.Keyboard.KeyDown(dict[key]));
        }

        public void release (string key)
        {
            alh.addToActionList(() => sim.Keyboard.KeyUp(dict[key]));
        }

        public void waitframe (int frames)
        {
            alh.addToActionList(() => sim.Keyboard.Sleep(timing.frame_to_ms(frames)));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            InputSimulator sim = new InputSimulator();

            while (!sim.InputDeviceState.IsKeyDown(VirtualKeyCode.ESCAPE))
            {
                if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.ESCAPE)) { break; }
                sim.Keyboard.Sleep(16);
            }
            sim.Keyboard.KeyDown(VirtualKeyCode.VK_S);
            sim.Keyboard.KeyDown(VirtualKeyCode.VK_I);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_S);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_I);

            sim.Keyboard.Sleep(176);

            sim.Keyboard.KeyDown(VirtualKeyCode.VK_I);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_I);

            sim.Keyboard.Sleep(300);

            sim.Keyboard.KeyDown(VirtualKeyCode.VK_W);
            sim.Keyboard.KeyDown(VirtualKeyCode.VK_D);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_W);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_D);

            sim.Keyboard.Sleep(176);

            sim.Keyboard.KeyDown(VirtualKeyCode.VK_I);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_I);

            sim.Keyboard.Sleep(176);

            sim.Keyboard.KeyDown(VirtualKeyCode.VK_U);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_U);

            sim.Keyboard.Sleep(176);

            sim.Keyboard.KeyDown(VirtualKeyCode.VK_U);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_U);

            sim.Keyboard.Sleep(176);

            sim.Keyboard.KeyDown(VirtualKeyCode.VK_S);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyDown(VirtualKeyCode.VK_P);
            sim.Keyboard.Sleep(16);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_S);
            sim.Keyboard.KeyUp(VirtualKeyCode.VK_P);
        }
    }
}
