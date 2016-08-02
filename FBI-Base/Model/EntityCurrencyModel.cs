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

  public class EntityCurrencyModel : SimpleCRUDModel<EntityCurrency>
  {
    static EntityCurrencyModel s_instance = null;
    public static EntityCurrencyModel Instance
    { get { return (s_instance == null) ? s_instance = new EntityCurrencyModel() : s_instance; } }

    EntityCurrencyModel() : base(NetworkManager.Instance)
    {
      Init();
    }

    public EntityCurrencyModel(NetworkManager p_netMgr) : base(p_netMgr)
    {
      Init();
    }

    void Init()
    {
      ReadCMSG = ClientMessage.CMSG_READ_ENTITY_CURRENCY;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_ENTITY_CURRENCY;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_ENTITY_CURRENCY;
      ListCMSG = ClientMessage.CMSG_LIST_ENTITY_CURRENCY;

      ReadSMSG = ServerMessage.SMSG_READ_ENTITY_CURRENCY_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_ENTITY_CURRENCY_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_ENTITY_CURRENCY_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_ENTITY_CURRENCY_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_ENTITY_CURRENCY_ANSWER;

      Build = EntityCurrency.BuildEntityCurrency;

      InitCallbacks();
    }
  }
}
