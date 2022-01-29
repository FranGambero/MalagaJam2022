using UnityEngine;
using UnityEngine.Events;

namespace MJam22.Conductor
{
    public class Conductor
    {
        float dspSongTime;
        float songBpm;
        float secPerBeat;
        float songPosSec;
        float songPosBeat;
        AudioSource musicSource;

        int lastBeat = 0;

        UnityEvent<int> onNewBeat;
        UnityEvent<float> onUpdatedSongTemp;

        public Conductor(float songBpm, AudioSource musicSource, UnityEvent<int> onNewBeat, UnityEvent<float> onUpdatedSongTemp)
        {
            this.songBpm = songBpm;
            this.musicSource = musicSource;
            this.onNewBeat = onNewBeat;
            this.onUpdatedSongTemp = onUpdatedSongTemp;
            
            dspSongTime = (float)AudioSettings.dspTime;
            Init();
        }

        void Init()
        {
            secPerBeat = 60f / songBpm;
        }

        public void StartSong()
        {
            musicSource.Play();
            dspSongTime = (float)AudioSettings.dspTime;
        }

        public void UpdateSongPosition(float firstBeatOffset)
        {
            songPosSec = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
            songPosBeat = songPosSec / secPerBeat;

            onUpdatedSongTemp.Invoke(songPosBeat);
            var roundToInt = Mathf.RoundToInt(songPosBeat);
            
            if(roundToInt!=lastBeat)
            {
                lastBeat = roundToInt;
                onNewBeat.Invoke(lastBeat);
            }
        }
    }
}