using SFML.Audio;

namespace OOPS_Assessment
{
    // Base audio class
    public abstract class GameAudio
    {
        protected SoundBuffer soundBuffer;
        protected Sound sound;

        public GameAudio(string soundPath)
        {
            soundBuffer = new SoundBuffer(soundPath);
            sound = new Sound(soundBuffer);
        }

        public abstract void Play();
        public abstract void Stop();
        public virtual void SetVolume(float volume)
        {
            // Volume in SFML is from 0 (mute) to 100 (full volume)
            sound.Volume = volume;
        }

        public float GetVolume()
        {
            return sound.Volume;
        }
    }

    // Background music implementation - loops continuously
    public class BackgroundMusic : GameAudio
    {
        public BackgroundMusic(string soundPath) : base(soundPath)
        {
            sound.Loop = true; // Background music loops
        }

        public override void Play()
        {
            sound.Play();
        }

        public override void Stop()
        {
            sound.Stop();
        }
    }

    // Sound effect implementation - plays once
    public class SoundEffect : GameAudio
    {
        public SoundEffect(string soundPath) : base(soundPath)
        {
            sound.Loop = false; // Sound effects don't loop
        }

        public override void Play()
        {
            sound.Play();
        }

        public override void Stop()
        {
            sound.Stop();
        }
    }

    // Audio manager to handle all game audio
    public class AudioManager
    {
        private static AudioManager instance;

        // Default volume levels
        private const float DEFAULT_MUSIC_VOLUME = 50.0f; // Default 50
        private const float DEFAULT_JUMP_VOLUME = 20.0f; // Default 70
        private const float DEFAULT_CRATE_VOLUME = 20.0f; // Default 80

        // Singleton pattern
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AudioManager();
                return instance;
            }
        }

        public BackgroundMusic BackgroundMusic { get; private set; }
        public SoundEffect JumpSound { get; private set; }
        public SoundEffect CrateMoveSound { get; private set; }

        private AudioManager()
        {
            // Initialize all game sounds
            BackgroundMusic = new BackgroundMusic("resources/Game_music_2.ogg");
            JumpSound = new SoundEffect("resources/jump.ogg");
            CrateMoveSound = new SoundEffect("resources/moving_create.ogg");

            // Set default volumes
            BackgroundMusic.SetVolume(DEFAULT_MUSIC_VOLUME);
            JumpSound.SetVolume(DEFAULT_JUMP_VOLUME);
            CrateMoveSound.SetVolume(DEFAULT_CRATE_VOLUME);
        }

        public void PlayBackgroundMusic()
        {
            BackgroundMusic.Play();
        }

        public void StopBackgroundMusic()
        {
            BackgroundMusic.Stop();
        }

        public void PlayJumpSound()
        {
            JumpSound.Play();
        }

        public void PlayCrateMoveSound()
        {
            CrateMoveSound.Play();
        }
    }
}