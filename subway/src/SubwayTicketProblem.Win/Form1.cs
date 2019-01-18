using SubwayTicketProblem.Library;
using SubwayTicketProblem.Library.ServiceDefines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubwayTicketProblem.Win
{
    public partial class Form1 : Form
    {
        private UserCard _currentUserCard ;
        private ICardService _cardService;

        public Form1()
        {
            InitializeComponent();

            InitControl();
            InitBaseData();
        }

        private void InitBaseData()
        {
            _cardService = new CardService();
            _currentUserCard = new UserCard();
            Refresh();
        }

        private void InitControl()
        {
            cmb_EntryStation.DataSource = StationCollection.AllStation();
            cmb_EntryStation.DisplayMember = "Name";

            cmb_GoOutStation.DataSource = StationCollection.AllStation();
            cmb_GoOutStation.DisplayMember = "Name";
        }

        private void btn_NewCard_Click(object sender, EventArgs e)
        {
            _currentUserCard = new UserCard();
            Refresh();
            LogOperation("Yeah, 新发了一张地铁卡!");
        }

        private void LogOperation(string msg)
        {
            txtOpenDisplay.AppendText(msg + "\r\n");
            Refresh();
        }

        private void Refresh()
        {
            txt_Balance.Text = _currentUserCard.Balance.ToString();
        }

        private void btn_Charge_Click(object sender, EventArgs e)
        {
            var chargeAmount = string.IsNullOrEmpty(txt_ChargeAmount.Text) ? 0 : Convert.ToDecimal(txt_ChargeAmount.Text);
            _cardService.Charge(_currentUserCard, chargeAmount);
            LogOperation("谢谢小姐姐的打赏, 充值金额: " + chargeAmount + ", 卡余额: " + _currentUserCard.Balance);

        }

        private void btn_EntryStation_Click(object sender, EventArgs e)
        {
            var entryStation = (Station)cmb_EntryStation.SelectedValue;
            _cardService.Entry(_currentUserCard, entryStation);

            LogOperation("刷卡进站: " + entryStation.Name);
        }

        private void btn_GoOut_Click(object sender, EventArgs e)
        {
            var outStation = (Station)cmb_GoOutStation.SelectedValue;
            var payAmount = _cardService.GoOut(_currentUserCard, outStation);

            LogOperation("刷卡出站: " + outStation.Name + ", 费用: " + payAmount);
        }
    }
}
