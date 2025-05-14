using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers.Admin
{
    [RoleAuthorization(URole.Admin)]
    [Route("Admin")]
    public class TablesController : Controller
    {
        private readonly ITable _tableLogic;

        public TablesController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _tableLogic = bl.GetTableBL();
        }

        public ActionResult Dashboard()
        {


            List<TableDTO> tableDto = _tableLogic.GetAllTables();

            List<TableViewModel> tables = tableDto.Select(t => new TableViewModel
            {

                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                Zone = t.Zone,
                Status = t.Status,

            }).ToList();

            return View("~/Views/Admin/Tables/Dashboard.cshtml",tables);
        }
        [HttpGet]


        public ActionResult Add()
        {

            return View("~/Views/Admin/Tables/Add.cshtml");
        }
        [HttpPost]


        public ActionResult Add(TableViewModel table)
        {
            TableDTO tableDto = new TableDTO
            {
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                Zone = table.Zone,
                Status = TStatus.Free
            };
            var resp = _tableLogic.AddTable(tableDto);
            if (resp.Status == true)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("~/Views/Admin/Tables/Add.cshtml", table);
            }

        }
        [HttpGet]


        public ActionResult Edit(int TableNumber)
        {
            TableDTO tableDto = _tableLogic.GetTableById(TableNumber);
            TableViewModel table = new TableViewModel
            {
                TableNumber = tableDto.TableNumber,
                Capacity = tableDto.Capacity,
                Zone = tableDto.Zone,
                Status = tableDto.Status,
            };

            return View("~/Views/Admin/Tables/Edit.cshtml", table);
        }

        [HttpPost]


        public ActionResult Edit(TableViewModel t)
        {
            var tableDto = new TableDTO
            {
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                Status = t.Status,
                Zone = t.Zone,
            };

            var resp = _tableLogic.EditTable(tableDto);
            if (resp.Status == true)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("~/Views/Admin/Tables/Edit.cshtml", t);
            }
        }


        public ActionResult Delete(int TableNumber)
        {
            _tableLogic.DeleteTable(TableNumber);
            return RedirectToAction("Dashboard");

        }
    }
}