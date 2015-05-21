using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOS.Phone
{
	partial class DoctorLookupCell : UITableViewCell
	{
		public static readonly string Key = "DoctorLookupCell";
		public string Name { get { return NameLabel.Text; } set { NameLabel.Text = value; } }
		public string Address { get { return AddressLabel.Text; } set { AddressLabel.Text = value; } }

		public string Specialty { 
			get 
			{ 
				return SpecialtyLabel.Text; 
			} 
			set 
			{ 
				if (!string.IsNullOrEmpty (value)) {
					SpecialtyLabel.Text = value.ToUpper ();
				} else {
					SpecialtyLabel.Text = value;
				}
			} 
		}

		public DoctorLookupCell (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("creating cell");
		}

		//TODO: delete constr
		private DoctorLookupCell()
		{
		}
	}
}
