using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RequestTimeOff.Core.Models.Requests
{
    public class PendingTimeOff : TimeOff
    {
		private ObservableCollection<TimeOff> _teamMemberTimeOffs;

		public ObservableCollection<TimeOff> TeamMemberTimeOffs
        {
			get { return _teamMemberTimeOffs; }
			set { _teamMemberTimeOffs = value; }
		}

	}
}
