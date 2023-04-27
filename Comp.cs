using System;

namespace exam_pk111
{
    internal class Comp
    {
        int id;
        String motherboard;
        String processor;
        String videocard;
        String power;

        public Comp(int _id, String _motherboard, String _processor, String _videocard, String _power)
        {
            id = _id;
            motherboard = _motherboard;
            processor = _processor;
            videocard = _videocard;
            power = _power;
        }
        public int getId() { return id; }

        public String PrintInfo()
        {
            return String.Concat(motherboard,"|",processor,"|",videocard,"|",power);
        }
    }
}
