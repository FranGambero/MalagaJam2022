using MJam22.Stress;
using UnityEngine;
using UnityEngine.UI;

namespace MJam22.States
{
    public abstract class State
    {
        protected StressBehaviour stressBehaviour;
        protected Image background;

        protected State(StressBehaviour stressBehaviour, Image background)
        {
            this.background = background;
            this.stressBehaviour = stressBehaviour;
        }
        public virtual void Start()
        {
        }
    }

    public class OfficeState : State
    {
        public OfficeState(StressBehaviour stressBehaviour, Image background) : base(stressBehaviour, background)
        {
        }
        
        public override void Start()
        {
            stressBehaviour.SetTimeStress(1);
            stressBehaviour.SetMissStress(3);
            stressBehaviour.SetHitStress(0);

            background.color = Color.cyan;
        }
    }
    
    public class ClubState : State
    {
        public ClubState(StressBehaviour stressBehaviour, Image background) : base(stressBehaviour, background)
        {
        }
        
        public override void Start()
        {
            stressBehaviour.SetTimeStress(0);
            stressBehaviour.SetMissStress(0);
            stressBehaviour.SetHitStress(5);

            background.color = Color.magenta;
        }
    }
}