using System.Windows.Forms;
using System;

namespace WinUIParts
{
    public class StatusColumn : DataGridViewColumn
    {
        public StatusColumn()
            : base(new StatusCell())
        {
        }
        private StatusImage m_DefaultStatus = StatusImage.Red;

        public StatusImage DefaultStatus
        {
            get { return m_DefaultStatus; }
            set { m_DefaultStatus = value; }
        }

        public override object Clone()
        {
            StatusColumn col = base.Clone() as StatusColumn;
            col.DefaultStatus = m_DefaultStatus;
            return col;
        }

        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if ((value == null) || !(value is StatusCell))
                {
                    throw new ArgumentException(
         "Invalid cell type, StatusColumns can only contain StatusCells");
                }
            }
        }
    }
}