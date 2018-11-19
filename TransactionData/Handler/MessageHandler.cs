using System.Web.UI.HtmlControls;


namespace TransactionData.Handler
{
    abstract class MessageHandler
    {
        public static void HandleMsg(HtmlGenericControl control, string _class, string _msg)
        {
            control.Style.Value = "display:block;";
            control.Attributes.Add("class", _class);
            control.InnerHtml = _msg;
        }
    }
}