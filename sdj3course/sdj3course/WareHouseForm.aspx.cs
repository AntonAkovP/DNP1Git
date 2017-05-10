using sdj3course.testReferance;
using System;
using System.Linq;
using System.Web.UI.WebControls;
namespace sdj3course
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private testMethodClient proxy;

        protected void addB_Click(object sender, EventArgs e)
        {
            orderList.Items.Add(inventoryList.SelectedValue);
            inventoryList.Items.Remove(inventoryList.SelectedItem);
        }

        protected void refreshB_Click(object sender, EventArgs e)
        {
            String[] tempList = proxy.getCont().Split('|');

            inventoryList.Items.Clear();
            orderList.Items.Clear();

            foreach (string temp in tempList)
                inventoryList.Items.Add(temp);
        }

        protected void removeB_Click(object sender, EventArgs e)
        {
            inventoryList.Items.Add(orderList.SelectedValue);
            orderList.Items.Remove(orderList.SelectedItem);
        }

        protected void orderB_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Order info", "alert('" + "Order successful" + "');", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             proxy = new testMethodClient();

        }
    }
}