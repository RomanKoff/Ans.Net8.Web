namespace Ans.Net8.Web
{

	public class StyleBuilder
	{

		private readonly Dictionary<string, string> _style = new();
		private string _join;


		/* ctors */


		public StyleBuilder()
		{
		}


		public StyleBuilder(
			string style)
			: this()
		{
			Append(style);
		}



		/* functions */


		public override string ToString()
		{
			return _join ??= string.Join("", _style.Select(x => $"{x.Key}:{x.Value};"));
		}


		public string GetJoin()
		{
			_join = null;
			return ToString();
		}


		public bool IsExists(
			string name)
		{
			return _style.ContainsKey(name);
		}


		/* methods */


		public void Append(
			string style)
		{
			if (!string.IsNullOrEmpty(style))
				foreach (var s1 in style.Split(';',
				StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
				{
					var a1 = s1.Split(':');
					AppendIf((a1.Length == 2), a1[0], a1[1]);
				}
		}


		public void Append(
			string name,
			string value)
		{
			if (!string.IsNullOrEmpty(value))
				_style[name] = value;
		}


		public void AppendIf(
			bool check,
			string name,
			string value)
		{
			if (check)
				Append(name, value);
		}


		public void Remove(
			string name)
		{
			_style.Remove(name);
		}


		public void Clear()
		{
			_style.Clear();
		}

	}

}
