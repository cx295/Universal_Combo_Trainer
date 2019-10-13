using System;
using WindowsInput.Native;
using WindowsInput;
using System.Collections.Generic;

namespace TestProject01
{
    public class TimingHandler
    {
        private float scaling;

        public TimingHandler(float i)
        {
            scaling = i;
        }

        public float getTimeScaling()
        {
            return scaling;
        }

        public void setTimeScaling(float i)
        {
            scaling = i;
        }

        public float frame_to_ms(int frames)
        {
            return (frames / 60) * scaling * 1000;
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
            for (int i = 0; i < actionList.Count; i++)
            {
                actionList[i]();
            }
        }
    }

    public class TechniqueHandler
    {
        public TechniqueHandler(ActionListHandler alh, TimingHandler timing)
        {
            InputSimulator sim = new InputSimulator();

            void pressdownSkey()
            {
                sim.Keyboard.KeyDown(VirtualKeyCode.VK_S);
            }

            alh.addToActionList(pressdownSkey);

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
