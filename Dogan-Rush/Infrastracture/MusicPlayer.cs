using Plugin.Maui.Audio;

namespace Dogan_Rush.Infrastracture
{
    public class MusicPlayer
    {
        private readonly IAudioManager _audioManager;
        private IAudioPlayer? _player;

        public MusicPlayer(IAudioManager audioManager = null)
        {
            if (audioManager == null)
            {
                audioManager = new AudioManager();
                _audioManager = audioManager;
            }
            else
            {
                _audioManager = audioManager;
            }
        }

        private bool _isPlaying;

        public void PlayMusic()
        {
            if (_player == null)
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("background.mp3").Result;
                using var reader = new StreamReader(stream);
                _player = _audioManager.CreatePlayer(stream);
            }

            _player.Play();
            _isPlaying = true;
        }
    }
}