using Ans.Net8.Common;
using System.Net;

namespace Ans.Net8.Web
{

	public class NetworkService
	{

		/* ctor */


		public NetworkService(
			CurrentContext current)
		{
			RemoteIpAddress = current.HttpContext.Connection.RemoteIpAddress;
			var _subnets1 = current.Options.Subnets;
			if (_subnets1 != null)
			{
				AdminSubnets = _subnets1.GetAdminSubnets();
				SafeSubnets = _subnets1.GetSafeSubnets();
				UnsafeSubnets = _subnets1.GetUnsafeSubnets();
				AllowSubnets = _subnets1.GetAllowSubnets();
				DenySubnets = _subnets1.GetDenySubnets();
				IsAdmin = AdminSubnets != null && IsRelate(AdminSubnets);
				IsSafe = SafeSubnets != null && IsRelate(SafeSubnets);
				IsUnsafe = UnsafeSubnets != null && IsRelate(UnsafeSubnets);
				IsAllow = AllowSubnets != null && IsRelate(AllowSubnets);
				IsDeny = DenySubnets != null && IsRelate(DenySubnets);
			}
		}


		/* readonly properties */


		public IPAddress RemoteIpAddress { get; }
		public IPSubnetsList AdminSubnets { get; }
		public IPSubnetsList SafeSubnets { get; }
		public IPSubnetsList UnsafeSubnets { get; }
		public IPSubnetsList AllowSubnets { get; }
		public IPSubnetsList DenySubnets { get; }
		public bool IsAdmin { get; }
		public bool IsSafe { get; }
		public bool IsUnsafe { get; }
		public bool IsAllow { get; }
		public bool IsDeny { get; }


		/* functions */


		public bool IsRelate(
			IPSubnetsList subnets)
		{
			if (subnets?.Count > 0)
				foreach (var item1 in subnets)
					if (RemoteIpAddress.IsInSubnet(item1))
						return true;
			return false;
		}


		public bool IsNotRelate(
			IPSubnetsList subnets)
		{
			if (subnets?.Count > 0)
				foreach (var item1 in subnets)
					if (RemoteIpAddress.IsInSubnet(item1))
						return false;
			return true;
		}

	}

}
