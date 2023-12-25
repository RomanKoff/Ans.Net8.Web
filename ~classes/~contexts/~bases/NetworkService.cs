using Ans.Net8.Common;
using System.Net;

namespace Ans.Net8.Web
{

	public class NetworkService
		: _Current_Base
	{

		/* ctor */


		public NetworkService(
			ICurrentContext current)
			: base(current)
		{
			RemoteIpAddress = _current.HttpContext.Connection.RemoteIpAddress;
			var _subnets1 = _current.Options.Subnets;
			if (_subnets1 != null)
			{
				AdminSubnets = _subnets1.GetAdminSubnets();
				SafeSubnets = _subnets1.GetSafeSubnets();
				UnsafeSubnets = _subnets1.GetUnsafeSubnets();
				AllowSubnets = _subnets1.GetAllowSubnets();
				DenySubnets = _subnets1.GetDenySubnets();
				IsAdmin = AdminSubnets != null && IsNetwork(AdminSubnets);
				IsSafe = SafeSubnets != null && IsNetwork(SafeSubnets);
				IsUnsafe = UnsafeSubnets != null && IsNetwork(UnsafeSubnets);
				IsAllow = AllowSubnets != null && IsNetwork(AllowSubnets);
				IsDeny = DenySubnets != null && IsNetwork(DenySubnets);
			}
		}


		/* readonly properties */


		public IPAddress RemoteIpAddress { get; private set; }
		public IpSubnetsList AdminSubnets { get; private set; }
		public IpSubnetsList SafeSubnets { get; private set; }
		public IpSubnetsList UnsafeSubnets { get; private set; }
		public IpSubnetsList AllowSubnets { get; private set; }
		public IpSubnetsList DenySubnets { get; private set; }
		public bool IsAdmin { get; private set; }
		public bool IsSafe { get; private set; }
		public bool IsUnsafe { get; private set; }
		public bool IsAllow { get; private set; }
		public bool IsDeny { get; private set; }


		/* functions */


		public bool IsNetwork(
			IpSubnetsList subnets)
		{
			if (subnets != null && subnets.Any())
				foreach (var item1 in subnets)
					if (RemoteIpAddress.IsInSubnet(item1))
						return true;
			return false;
		}


		public bool IsNotNetwork(
			IpSubnetsList subnets)
		{
			if (subnets != null && subnets.Any())
				foreach (var item1 in subnets)
					if (RemoteIpAddress.IsInSubnet(item1))
						return false;
			return true;
		}

	}

}
