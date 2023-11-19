namespace Ans.Net8.Web
{

	public class _Wrapper_Base
		: IDisposable
	{
		public virtual void WrapperStart() { }
		public virtual void WrapperStop() { }

		private bool disposedValue;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
					WrapperStop();
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}

}
