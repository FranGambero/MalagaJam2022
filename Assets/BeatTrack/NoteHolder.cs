using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MJam22.Beat
{
    public class NoteHolder
    {
        List<NoteBehaviour> currentNotes;
        float distance;

        public NoteHolder(float distance)
        {
            currentNotes = new List<NoteBehaviour>();
            this.distance = distance;
        }

        public void AddNote(NoteBehaviour note) => currentNotes.Add(note);

        public void MoveNotes(float secondsToArrive)
        {
            foreach(var note in currentNotes)
            {
                note.transform.position += new Vector3(0f, distance / secondsToArrive * Time.deltaTime, 0f);
            }
        }

        public IEnumerable<NoteBehaviour> GetActivatedNotes()
        {
            return currentNotes.Where(note => note.CanBePressed);
        }

        public void RemoveNotes(NoteBehaviour note) => currentNotes.Remove(note);

        public void RemoveNotes(List<NoteBehaviour> notes)
        {
            foreach(var note in notes)
            {
                RemoveNotes(note);
            }
        }
    }
}