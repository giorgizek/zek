using System.Net;
using System.Net.Sockets;

namespace Zek.Net
{
    public class IpAddressRange
    {
        readonly AddressFamily _addressFamily;
        readonly byte[] _lowerBytes;
        readonly byte[] _upperBytes;

        public IpAddressRange(IPAddress lowerInclusive, IPAddress upperInclusive)
        {
            // Assert that lower.AddressFamily == upper.AddressFamily

            _addressFamily = lowerInclusive.AddressFamily;
            _lowerBytes = lowerInclusive.GetAddressBytes();
            _upperBytes = upperInclusive.GetAddressBytes();
        }

        public bool IsInRange(IPAddress address)
        {
            if (address.AddressFamily != _addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < _lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < _lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > _upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == _lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == _upperBytes[i]);
            }

            return true;
        }
    }
}
