using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Wac2015.iOS.Controls;
using Wac2015.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MenuTableView), typeof(MenuTableViewRenderer))]
namespace Wac2015.iOS.Controls
{
    public class MenuTableViewRenderer : TableViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            var tableView = Control as UITableView;

            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

            //tableView.BackgroundColor = UIColor.FromRGB(0x2C, 0x3E, 0x50);
        }
    }
}