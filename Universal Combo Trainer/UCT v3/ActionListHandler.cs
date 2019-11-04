using System;
using System.Collections.Generic;
using System.Text;

namespace UCT_v3
{
    class ActionListHandler
    {
        private List<Action> actionList;

        public ActionListHandler()
        {
            actionList = new List<Action>();
        }

        public void addToActionList(Action item)
        {
            actionList.Add(item);
        }

        public void runActionList()
        {
            foreach (Action act in actionList)
            {
                act();
            }
        }
    }
}
