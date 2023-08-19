namespace BLEScan
{
    public class RollYawPitch
    {
        public RollYawPitch()
        {
        }

        public float Roll { get; set; }

        public float Yaw { get; set; }

        public float Pitch { get; set; }


        public override string ToString()
        {
            return $"roll: {Roll}, yaw: {Yaw}, heading: {Pitch}";
        }
    }
}
