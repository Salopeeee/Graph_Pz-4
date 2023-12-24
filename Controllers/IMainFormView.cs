using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Controllers.GraphController;

namespace Controllers
{
    public interface IMainFormView
    {
        void DisplayGraphInfo(GraphInfoDTO graphInfo);
        void UpdateGraphDisplay();
    }
}
