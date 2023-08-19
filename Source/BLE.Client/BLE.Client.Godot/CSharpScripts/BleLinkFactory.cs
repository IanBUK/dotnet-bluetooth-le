using System;
namespace BLEScan
{
    public static class BleLinkFactory
    {
        public enum BleLinks { MacOS, Android, iOS, Fake}
        
        public static BleLinkBase GetBleLink(BleLinks link)
        {
            Main.sendDebugMessage("into factory");
            BleLinkBase result;
            switch (link)
            {
                case BleLinks.Android:
                    result = new BLEPluginBLE();
                   break;

                case BleLinks.iOS:
                    result = new BLELinkiOS();
                    break;

                case BleLinks.MacOS:
                     result = new BLELinkMacOS();
                    break;

                default:
                    result = new BLELinkFake();
                    break;
            }
            return result;
        }
    }
}

