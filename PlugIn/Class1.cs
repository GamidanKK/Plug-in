using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
namespace Lab1PlaceGroup
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Получение объектов приложений(Revit) и документов
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            //Контейнер, в котором в последующем будет храниться, выбранный нами, обьект
            Reference pickedref = null;

            //Выбор объекта
            Selection sel = uiapp.ActiveUIDocument.Selection;
            pickedref = sel.PickObject(ObjectType.Element, "Please select a group");
            Element elem = doc.GetElement(pickedref);
            Group group = elem as Group;

            //Выбор точки для копирования
            XYZ point = sel.PickPoint("Please pick a point to place group");

            //Создание транзакции
            Transaction trans = new Transaction(doc);
            trans.Start("Lab");
            doc.Create.PlaceGroup(point, group.GroupType);
            trans.Commit();

            return Result.Succeeded;
        }
    }
}