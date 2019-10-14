using System;
using WindowsInput.Native;
using WindowsInput;
using System.Collections.Generic;

namespace TestProject01
{
    public class TimingHandler
    {
        private float scaling;

        public TimingHandler(float s)
        {
            scaling = s;
        }

        public float Scaling
        {
            get { return scaling; }
            set { scaling = value; }
        }

        public int frame_to_ms(int frames)
        {
            return (int)((frames / 60) * scaling * 1000);
        }
    }

    public class ActionListHandler
    {
        private List<Action> actionList;

        public ActionListHandler()
        {
            actionList = new List<Action>();
        }

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
            dict.Add("up", VirtualKeyCode.VK_W);
            dict.Add("down", VirtualKeyCode.VK_S);
            dict.Add("left", VirtualKeyCode.VK_A);
            dict.Add("right", VirtualKeyCode.VK_D);
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
            TimingHandler t = new TimingHandler(1);
            ActionListHandler alh = new ActionListHandler();
            TechniqueHandler th = new TechniqueHandler(alh, t);
            InputSimulator sim = new InputSimulator();

            th.press("up");
            th.press("right");
            th.waitframe(1);
            th.release("up");
            th.release("right");
            th.waitframe(3);
            th.press("right");
            th.waitframe(1);
            th.release("right");

            int i = 0;
            while(i < 1)
            {
                sim.Keyboard.Sleep(20);
                if (sim.InputDeviceState.IsKeyDown(VirtualKeyCode.ESCAPE))
                {
                    break;
                }
            }
            alh.runActionList();
        }
    }
}
