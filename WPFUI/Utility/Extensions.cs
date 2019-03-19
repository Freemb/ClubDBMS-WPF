using DataLibrary.Models;
using DataLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace WPFUI.Utility
{
    public static class ExtensionMethods
	{
        public static int GetBoundCollectionIndex<T>(this ObservableCollection<T> source, int? ID) where T : IModel<T>
        {
            if (source == null) return 0;
            T temp = source.Where(model => model.ID == ID).FirstOrDefault();
            return source.IndexOf(temp);

        }
        public static MemberModel GetMemberDetails(this List<MemberModel> memlist, double memno)
        {
            MemberModel member = memlist.Where(model => model.MemNo == memno).FirstOrDefault();
            return member;
        }
        public static bool IsWeekendBankHoliday(this DateTime date)
        {
            if ((int)(date.DayOfWeek) < 5) return false;
            //TODO institute checking of list of BH

            return true;
        }
        
		
		

        
        

      
    }
}
