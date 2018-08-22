using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if __IOS__
using AudioToolbox;
using AVFoundation;
using Foundation;
#else
using Android.Content;
using Android.Media;
#endif
using Xamarin.Forms;

namespace KNEO
{
    public partial class MainPage : ContentPage
    {
#if __IOS__
        AVPlayer player;
#else
        MediaPlayer player;
#endif

        public MainPage()
        {
            InitializeComponent();

#if __IOS__
            player = new AVPlayer();
            player = AVPlayer.FromUrl(NSUrl.FromString("http://noasrv.caster.fm:10135/live"));
            player.Play();
            VolumeSlider.Value = player.Volume;
#else
            player = new MediaPlayer();
            var audioAttributesBuilder = new AudioAttributes.Builder();
            audioAttributesBuilder.SetContentType(AudioContentType.Music);
            audioAttributesBuilder.SetLegacyStreamType(Stream.Music);
            audioAttributesBuilder.SetUsage(AudioUsageKind.Media);
            player.SetAudioAttributes(audioAttributesBuilder.Build());
            player.SetDataSource("http://noasrv.caster.fm:10135/live");
            player.Prepare();
            player.Start();
            VolumeSlider.Value = 1.0f;
#endif

        }

        private void BtnCall_OnClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:14174515636"));
        }

        private void BtnEmail_OnClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:adam@kneo.org"));
        }

        private void BtnWeb_OnClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://www.kneo.org"));
        }

        private void VolumeSlider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            float vol = (float)VolumeSlider.Value;

#if __IOS__
            player.Volume = vol;
#else
            player.SetVolume(vol, vol);
#endif
        }
    }
}
