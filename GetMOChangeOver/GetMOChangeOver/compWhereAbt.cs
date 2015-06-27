using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMOChangeOver
{
    public class compWhereAbt 
    {

        
        public compWhereAbt(string pNo, int arealoc, string arealocex)
        {
            PartNumber = pNo;
            AreaLocationEX = arealocex;

            switch (arealoc)
	        {
		        case 0:
                    AreaLocation = "Undefined";
                    break;
                case 1:
                    AreaLocation = "BookedIn";
                    break;
                case 2:
                    AreaLocation = "BookedOut";
                    break;
                case 3:
                    AreaLocation = "OnActiveFeederPool_Offline";    
                    break;
                case 4:
                    AreaLocation = "OnActiveFeederPool";
                    break;
             
	        }
        }
        
        private string m_AreaLocation;

	    public string AreaLocation
        {
		    get { return m_AreaLocation;}
		    set { m_AreaLocation = value;}
	    }

        private string m_PartNumber;

        public string PartNumber
        {
            get { return m_PartNumber; }
            set { m_PartNumber = value; }
        }

        private string m_AreaLocationEX;

        public string AreaLocationEX
        {
            get { return m_AreaLocationEX; }
            set { m_AreaLocationEX = value; }
        }
        

     }
        

}
