using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main.XmlDoc;
using System.IO;
using System.Windows.Controls;

namespace JoshuaWebAPI
{
    public class Controls
    {
        
    }
    public class ActionModel
    {
        public enum Action { Obtain, StoreNewSubj, StoreNewCat }

        public string Choice { get; set; }

        public Action ThisAction { get; set; }

        public Button ActionButton { get; set; }

        public TextBox Categories { get; set; }

        public TextBox Category { get; set; }

        public TextBox Subject { get; set; }

        public TextBox Fact { get; set; }

        public TextBox Value { get; set; }

        public TextBox Result { get; set; }

        public ExEmEl xml;

        public ActionModel()
        {

        }
    }
    public partial class WebForm2 : System.Web.UI.Page
    {
        static ActionModel CurrentModel { get; set; }

        string ThisResult { get; set; }


        protected void ActionButton_Click(object sender, EventArgs e)
        {
            if (CurrentModel.ThisAction == ActionModel.Action.Obtain)
            {
                CurrentModel.xml.XmlSearchInfo(infotofind: $"{Category.Text},{Subject.Text},{Fact.Text}");
                var Answer = CurrentModel.xml.GetXmlInfo();
                Result.Text = Answer ?? "";
            }
            else if(CurrentModel.ThisAction == ActionModel.Action.StoreNewCat)
            {
                CurrentModel.xml.XmlSearchInfo(fromroot: $"{Categories.Text}",
                                               child: $"{Category.Text},{Subject.Text}",
                                               node: $"{Fact.Text}, {Value.Text}");
                CurrentModel.xml.WriteNodeToXml();
            }
            else if(CurrentModel.ThisAction == ActionModel.Action.StoreNewSubj)
            {
                CurrentModel.xml.XmlSearchInfo(child: $"{Category.Text},{Subject.Text}",
                                               node: $"{Fact.Text}");
                CurrentModel.xml.WriteNodeToXml();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cm = new ActionModel
            {
                ThisAction = DropDownList1.SelectedItem.Value.Contains("Obtain") ?
                                            ActionModel.Action.Obtain :
                             DropDownList1.SelectedItem.Value.Contains("Store New Category") ?
                                            ActionModel.Action.StoreNewCat :
                             DropDownList1.SelectedItem.Value.Contains("Store New Subject") ?
                                            ActionModel.Action.StoreNewSubj :
                                            ActionModel.Action.Obtain,
                ActionButton = ActionButton,
                Categories = Categories,
                Category = Categories,
                Subject = Subject,
                Fact = Fact,
                Value = Value,
                Result = Result
            };
            CurrentModel = cm;
            GetSettings();
        }

        protected void Categories_TextChanged(object sender, EventArgs e)
        {
        }

        protected void Value_TextChanged(object sender, EventArgs e)
        {
        }

        protected void Fact_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Subject_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Category_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Result_TextChanged(object sender, EventArgs e)
        {
        }

        public void GetSettings()
        {
            string Choice = CurrentModel.ThisAction == ActionModel.Action.Obtain ? 
                            "Obtain" :
                            CurrentModel.ThisAction == ActionModel.Action.StoreNewCat ? 
                            "Store New Category" :
                            CurrentModel.ThisAction == ActionModel.Action.StoreNewSubj ? 
                            "Store New Subject" : null;
            CurrentModel.xml = new ExEmEl(@"C:\Users\DELL\Documents\GitRepoJosh\Data.xml");
            CurrentModel.xml.XmlSearchInfo(infotofind: $"InformationString,All,{Choice}");
            var Model = CurrentModel.xml.GetXmlInfo();
            var AllObjectInfo = Model.Replace("\n\t", "").Replace("\r", "").Split(new[] { " NEWPROP " }, StringSplitOptions.None).ToList().
                Select(x => x.Split(new[] { " NEWOPT " }, StringSplitOptions.None).ToList()).ToList();
            /*var AllProperties = CurrentModel.GetType().GetProperties().
                Where(prop => prop.PropertyType.Name.Equals("Button") ||
                              prop.PropertyType.Name.Equals("TextBox")).
                              Select(x => x.GetType().GetFields().
                                          Where(y => y.Name.Equals("Visible")).ToList()).ToList();*/

            
            CurrentModel.ActionButton.Text = CurrentModel.ActionButton.Text.Count() < 1 ? "" : AllObjectInfo[0][0];
            CurrentModel.ActionButton.Visible = bool.Parse(AllObjectInfo[0][1]);
            CurrentModel.Categories.Text = CurrentModel.Categories.Text.Count() < 1 ? "" : AllObjectInfo[1][0]; ;
            CurrentModel.Categories.Visible = bool.Parse(AllObjectInfo[1][1]);
            CurrentModel.Category.Text = CurrentModel.Category.Text.Count() < 1 ? "" : AllObjectInfo[2][0];
            CurrentModel.Category.Visible = bool.Parse(AllObjectInfo[2][1]);
            CurrentModel.Subject.Text = CurrentModel.Subject.Text.Count() < 1 ? "" : AllObjectInfo[3][0];
            CurrentModel.Subject.Visible = bool.Parse(AllObjectInfo[3][1]);
            CurrentModel.Fact.Text = CurrentModel.Fact.Text.Count() < 1 ? "" : AllObjectInfo[4][0];
            CurrentModel.Fact.Visible = bool.Parse(AllObjectInfo[4][1]);
            CurrentModel.Value.Text = CurrentModel.Value.Text.Count() < 1 ? "" : AllObjectInfo[5][0];
            CurrentModel.Value.Visible = bool.Parse(AllObjectInfo[5][1]);
            CurrentModel.Result.Text = CurrentModel.Result.Text.Count() < 1 ? "" : AllObjectInfo[6][0];
            CurrentModel.Result.Visible = bool.Parse(AllObjectInfo[6][1]);
        }
    }
}