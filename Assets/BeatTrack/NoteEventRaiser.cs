using UnityEngine.Events;

namespace MJam22.Beat
{
    public class NoteEventRaiser
    {
        public UnityEvent OnNoteOutOfSight { get; } = new UnityEvent();
    }
}