using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Types.Rules
{
    public class Movement
    {
        private List<Condition> _conditions = new List<Condition>();
        public List<Condition> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                _conditions = value;
            }
        }

        /// <summary>
        /// If you have no requirements, then pass a Condition with a type of "none"
        /// This is done this way because I want you to always pay attention to what conditions are
        /// necessary for movement. One condition that would be required in a lot of chess variants
        /// would be that the king is not in check.
        /// </summary>
        /// <param name="movementCondition"></param>
        public Movement(Condition movementCondition)
        {
            this.Conditions.Add(movementCondition);
        }
        public Movement(List<Condition> movementConditions)
        {
            if (movementConditions.Count == 0)
            {
                //throw new MovementException("Must Pass a condition first");
            }

            this.Conditions = movementConditions;
        }
        public Movement()
        {
            this.CheckZeroConditions();
        }

        private void CheckZeroConditions()
        {
            if (this.Conditions.Count == 0)
            {
                //throw new MovementException("Must Pass a condition first");
            }
        }
    }
}
