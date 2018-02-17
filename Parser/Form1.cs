using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace Parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpRequest request = new HttpRequest();
            HttpResponse response;
            HtmlParser parser = new HtmlParser();
            for (int i = 1; i < 19; i++)
            {
                response = request.Get($"http://hserv.net.ua/page/{i}");

                IHtmlDocument document = await parser.ParseAsync(response.ToString());

                var result = document.QuerySelectorAll("td.strog.a").Select(item => item.TextContent);

                foreach (var item in result)
                {
                    if (!String.IsNullOrEmpty(item) && item != "Место свободно, подробнее")
                        textBox1.Text += item + "\r\n";
                }

            }
        }
    }
}
