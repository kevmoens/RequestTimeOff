using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RequestTimeOff.Core.Models.Requests
{
    public class PendingTimeOff : TimeOff
    {
		private ObservableCollection<TimeOff> _teamMemberTimeOffs;

        [ExcludeFromCodeCoverage]
        public ObservableCollection<TimeOff> TeamMemberTimeOffs
        {
			get { return _teamMemberTimeOffs; }
			set { _teamMemberTimeOffs = value; }
		}

	}
}
