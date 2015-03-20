namespace Shared.Common
{
	public class PSColor
	{
		public int R { get; set; }
		public int G { get; set; }
		public int B { get; set; }
		public int A { get; set; }

		public PSColor (int r, int g, int b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = 1;
		}

		public PSColor (int r, int g, int b, int a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}
	}
}

