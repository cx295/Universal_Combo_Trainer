using System;
using System.Collections.Generic;
using System.Text;

namespace UCT_v3
{
    class TimingHandler
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
}
