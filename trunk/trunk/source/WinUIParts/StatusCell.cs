using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;

namespace WinUIParts
{
    public enum StatusImage
    {
        Green,
        Yellow,
        Red
    }

    public class StatusCell : DataGridViewImageCell
    {
        public StatusCell()
        {
            this.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        protected override object GetFormattedValue(object value,
           int rowIndex, ref DataGridViewCellStyle cellStyle,
           TypeConverter valueTypeConverter,
           TypeConverter formattedValueTypeConverter,
           DataGridViewDataErrorContexts context)
        {

            string resource = "CustomColumnAndCell.Red.bmp";
            StatusImage status = StatusImage.Red;
            // Try to get the default value from the containing column
            StatusColumn owningCol = OwningColumn as StatusColumn;
            if (owningCol != null)
            {
                status = owningCol.DefaultStatus;
            }
            if (value is StatusImage || value is int)
            {
                status = (StatusImage)value;
            }
            switch (status)
            {
                case StatusImage.Green:
                    resource = "CustomColumnAndCell.Green.bmp";
                    break;
                case StatusImage.Yellow:
                    resource = "CustomColumnAndCell.Yellow.bmp";
                    break;
                case StatusImage.Red:
                    resource = "CustomColumnAndCell.Red.bmp";
                    break;
                default:
                    break;
            }
            Assembly loadedAssembly = Assembly.GetExecutingAssembly();
            Stream stream = loadedAssembly.GetManifestResourceStream(resource);
            Image img = Image.FromStream(stream);
            cellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            return img;
        }
    }
}