using ASAP.Assets.Service;

using System;
using System.Web.UI;

namespace ASAP.Assets.WebApp
{
    public partial class _Default : Page
    {
        private readonly GoogleSheetsService _sheetsService;

        public _Default()
        {
            _sheetsService = new GoogleSheetsService();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                _sheetsService.ExportFromSheet();
            }

        }
    }
}