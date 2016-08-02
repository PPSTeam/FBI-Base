using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;

  public class AxisConfigurationModel : SimpleCRUDModel<AxisConfiguration>
  {
    static AxisConfigurationModel s_instance = null;
    public static AxisConfigurationModel Instance 
    { get { return (s_instance == null) ? s_instance = new AxisConfigurationModel() : s_instance; } }

    AxisConfigurationModel() : base(NetworkManager.Instance)
    {
      Init();
    }

    public AxisConfigurationModel(NetworkManager p_netMgr) : base(p_netMgr)
    {
      Init();
    }

    void Init()
    {
      ListSMSG = ServerMessage.SMSG_AXIS_CONFIGURATION_LIST;

      Build = AxisConfiguration.BuildAxisConfiguration;

      InitCallbacks();
    }
  }
}
