namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddSingleton<IMapActionsProvider, MapActionsProvider_Fake>();
	 */



	public interface IMapActionsProvider
	{
		MapActions GetMap();
	}



	public class MapActionsProvider_Fake()
		: IMapActionsProvider
	{
		public MapActions GetMap()
			=> null;
	}

}
