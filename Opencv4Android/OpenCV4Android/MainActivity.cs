using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System;

using Org.Opencv.Android;
using Org.Opencv.Core;
using Org.Opencv.Utils;
using Org.Opencv.Imgproc;

namespace OpenCV4Android
{
	[Activity (Label = "OpenCV4Android", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		Mat grayM;

		public MainActivity()
		{
			
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			if (!OpenCVLoader.InitDebug ()) {
				Console.WriteLine ("Init OpenCV failed!!");	
			} else {
				Console.WriteLine ("Init OpenCV succefuly!!");	
			}

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				//button.Text = string.Format ("{0} clicks!", count++);

				SetImage();
			};
		}

		void SetImage()
		{
			ImageView iView = FindViewById<ImageView> (Resource.Id.imageView1);

			using(Bitmap img = BitmapFactory.DecodeResource(Resources, Resource.Drawable.lena))
			{
				if (img != null) {
					Mat m = new Mat ();
					grayM = new Mat ();

					Utils.BitmapToMat (img, m);

					Imgproc.CvtColor (m, grayM, Imgproc.ColorBgr2gray);

					Imgproc.CvtColor (grayM, m, Imgproc.ColorGray2rgba);

					using (Bitmap bm = Bitmap.CreateBitmap (m.Cols(), m.Rows(), Bitmap.Config.Argb8888)) {
						Utils.MatToBitmap (m, bm);

						iView.SetImageBitmap (bm);
					}

					m.Release ();
					grayM.Release ();
				}
			}
		}
	}
}


