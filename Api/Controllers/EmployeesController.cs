using Application.DTO;
using Application.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        #region Get All
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();

                return Ok(employees);
            }
            catch (Exception)
            {
                throw;
            }
        }




        #endregion


        #region employee attachments

        [HttpGet("GetAttachmentGroup/{attachmentGroupId}")]
        public async Task<IActionResult> GetAttachmentGroupAsync(int attachmentGroupId)
        {
            try
            {
                var attachmentGroup = await _employeeService.GetAttachmentGroupAsync( attachmentGroupId);

                return Ok(attachmentGroup);

            }
            catch (Exception)
            {
                throw;
            }

        }


        #endregion


        #region employee attachments
        [HttpGet("EmployeeAttachments/{attachmentGroupId}")]
        public async Task<IActionResult> GetEmployeeAttachmentsAsync(int attachmentGroupId)
        {
            try
            {
                var employeeAttachments = await _employeeService.GetEmployeeAttachmentsAsync(attachmentGroupId);

                return Ok(employeeAttachments);

            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region download attachments

        [HttpGet("DownloadAttachment/{employeeId}")]
        public async Task<IActionResult> DownloadAttachmentAsync(int employeeId)
        {

            await _employeeService.DownloadAttachmentAsync(employeeId);

            return Ok();


        }

        #endregion

        #region Add

        [HttpPost]
        public async Task<ActionResult> AddAsync(List<IFormFile> attachmentDataList, List<IFormFile> employeeImage, [FromForm] EmployeeDTO employeeData )
        {

            try
            {
                await _employeeService.AddEmployeeAsync(attachmentDataList, employeeImage, employeeData);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion




    }
}
