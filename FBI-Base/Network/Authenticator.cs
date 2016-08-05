using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Network
{
  using Utils;

  public class Authenticator
  {
    static Authenticator s_instance = null;
    public static Authenticator Instance 
    { get { return ((s_instance == null) ? s_instance = new Authenticator() : s_instance); } }
    public static string Username { get; private set; }
    static string m_password;
    public const string FBIVersionId = "1.0.3";
    public event AuthenticationEventHandler AuthenticationEvent;
    public delegate void AuthenticationEventHandler(ErrorMessage p_status);
    NetworkManager m_mgr;

    public Authenticator()
    {
      m_mgr = NetworkManager.Instance;
      Init();
    }

    public Authenticator(NetworkManager p_mgr)
    {
      m_mgr = p_mgr;
      Init();
    }

    void Init()
    {
      m_mgr.SetCallback((UInt16)ServerMessage.SMSG_AUTH_REQUEST_ANSWER, OnAuthRequestAnswer);
      m_mgr.SetCallback((UInt16)ServerMessage.SMSG_AUTH_ANSWER, OnAuthAnswer);
    }
    
    public bool AskAuthentication(string p_username, string p_password)
    {
      ByteBuffer l_packet = new ByteBuffer((UInt16)ClientMessage.CMSG_AUTH_REQUEST);
      Username = (p_username != "") ? p_username : Username;
      m_password = (p_password != "") ? Hash.GetSHA1(p_password + p_username) : m_password;
      l_packet.WriteString(FBIVersionId);
      l_packet.Release();
      return m_mgr.Send(l_packet);
    }

    void OnAuthRequestAnswer(ByteBuffer p_packet)
    {
      string l_authToken;
      ByteBuffer l_answer = new ByteBuffer((UInt16)ClientMessage.CMSG_AUTHENTIFICATION);

      if (p_packet.GetError() != ErrorMessage.SUCCESS)
      {
        if (AuthenticationEvent != null)
          AuthenticationEvent(p_packet.GetError());
        return;
      }
      l_authToken = p_packet.ReadString();
      l_answer.WriteString(Username);
      l_answer.WriteString(Hash.GetSHA1(m_password + l_authToken));
      l_answer.Release();

      m_mgr.Send(l_answer);
    }

    void OnAuthAnswer(ByteBuffer p_packet)
    {
      if (AuthenticationEvent != null)
        AuthenticationEvent(p_packet.GetError());
    } 
  }
}
