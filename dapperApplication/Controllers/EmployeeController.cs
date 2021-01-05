using Dapper;
using dapperApplication.Models;
using dapperApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dapperApplication.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDapper _dapper;
        public EmployeeController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(Employee data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Studid", data.Studid, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Add_Article]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        [HttpGet(nameof(GetAll))]
        public async  Task <IActionResult> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Employee>($"Select * from [StudentPrimaryInfo]", null, commandType: CommandType.Text));
            IActionResult response = Unauthorized();
            response = Ok(new
            {
                studCollection = result
            });
            return response;
        }

    }
}
