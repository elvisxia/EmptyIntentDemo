using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Runtime;
using Android.Net;
using Android.Provider;
using Android.Database;

namespace EmptyIntentDemo
{
    [Activity(Label = "EmptyIntentDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView audioFileNameText;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Button btnClick = FindViewById<Button>(Resource.Id.btnClick);
            audioFileNameText = FindViewById<TextView>(Resource.Id.audioFileNameText);
            btnClick.Click += BtnClick_Click;
        }

        private void BtnClick_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(Intent.ActionPick, Android.Provider.MediaStore.Audio.Media.ExternalContentUri);

            intent.SetType("audio/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(
            Intent.CreateChooser(intent, "Select audio file"), 1);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            string abc= getRealPathFromURI(this,data.Data);
        }

        
        public string getRealPathFromURI(Context context, Uri contentUri)
        {
            ICursor cursor = null;
            try
            {
                string[] proj = { MediaStore.Audio.AudioColumns.Data};
                cursor = ContentResolver.Query(contentUri, proj, null, null, null);
                int column_index = cursor.GetColumnIndexOrThrow(MediaStore.Audio.AudioColumns.Data);
                cursor.MoveToFirst();
                return cursor.GetString(column_index);
            }
            finally
            {
                if (cursor != null)
                {
                    cursor.Close();
                }
            }
        }



    }
}

