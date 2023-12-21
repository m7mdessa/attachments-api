using Application.DTO;
using Domain.Entites;
using Domain.SharedKernel;
using Microsoft.AspNetCore.Http;


namespace Application.Services.Employees
{
    public class EmployeeService : BaseService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region get all employees
        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            try
            {
                var employeeRepository = UnitOfWork.EmployeeRepository();
                var employee = await employeeRepository.GetAllAsync();

                var employees = employee.Select(employee => new EmployeeDTO
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    EmployeeName = employee.EmployeeName,
                    City = employee.Address.City,
                    Street = employee.Address.Street,
                    Salary = employee.Salary,
                    Country = employee.Address.Country,
                    Phone = employee.Phone,
                    Image = employee.Image,
                    HireDate = employee.HireDate,
                    AttachmentGroupId= employee.AttachmentGroupId
                    
                }).ToList();

                return employees;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

// mohammaad

        #region get attachment

        public async Task<AttachmentDTO> GetAttachmentGroupAsync(int attachmentGroupId)
        {
            var attachmentRepository = UnitOfWork.AttachmentRepository();
            var attachment = await attachmentRepository.GetAttachmentGroupAsync(attachmentGroupId);

            var attachments = new AttachmentDTO
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FileData = attachment.FileData,
                AttachmentGroupId = attachment.AttachmentGroupId,
            };

            return attachments;

        }
        #endregion



        #region get employee attachments
        public async Task<IEnumerable<AttachmentDTO>> GetEmployeeAttachmentsAsync(int attachmentGroupId)
        {
            var attachmentRepository = UnitOfWork.AttachmentRepository();
            var attachment = await attachmentRepository.GetEmployeeAttachmentsAsync(attachmentGroupId);

            var attachments = attachment.Select(attachment => new AttachmentDTO
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FileData = attachment.FileData,
                AttachmentGroupId = attachment.AttachmentGroupId,
            }).ToList();

            return attachments;
        }


        #endregion



        #region single attachment

        //public async Task AddEmployeeAsync(IFormFile attachmentData, EmployeeDTO employeeData)
        //{
        //    try
        //    {
        //        if (attachmentData == null || attachmentData.Length == 0 || employeeData == null)
        //        {
        //            throw new ArgumentException("Invalid data provided.");
        //        }

        //        var attachment = new Attachment( employeeData.AttachmentTypeId, employeeData.EmployeeId);

        //        var address = new Address(employeeData.Street, employeeData.City, employeeData.State, employeeData.Country, employeeData.ZipCode);

        //        var employee = new Employee(employeeData.EmployeeName, employeeData.FirstName, employeeData.LastName, employeeData.Email,

        //            employeeData.Phone, employeeData.Image, employeeData.Salary, address);



        //        var employeeRepository = UnitOfWork.EmployeeRepository();
        //        await employeeRepository.AddAsync(employee);
        //        await UnitOfWork.SaveChangesAsync();


        //        if (attachmentData != null && attachmentData.Length > 0)
        //        {
        //            var fileName = Guid.NewGuid().ToString() + "_" + attachmentData.FileName;
        //            var fullPath = Path.Combine("C:\\Users\\m.moien.ext\\Desktop\\attachments-api\\Application\\Files\\", fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                attachmentData.CopyTo(stream);
        //            }

        //            using (var fileStream = new FileStream(fullPath, FileMode.Open))
        //            {

        //                byte[] fileData = new byte[fileStream.Length];
        //                fileStream.Read(fileData, 0, (int)fileStream.Length);

        //                attachment.SetFileData(fileData);
        //                attachment.SetFileName(fileName);
        //            }

        //        }

        //        var attachmentRepository = UnitOfWork.AttachmentRepository();
        //        await attachmentRepository.UploodAttachmentAsync(attachment);

        //        await UnitOfWork.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion


        #region multiple attachments

        public async Task AddEmployeeAsync(List<IFormFile> attachmentDataList, List<IFormFile> employeeImage, EmployeeDTO employeeData)
        {
            try
            {
                if (attachmentDataList == null || attachmentDataList.All(a => a.Length == 0) || employeeData == null)
                {
                    throw new ArgumentException("Invalid data provided.");
                }

                if (attachmentDataList.Any(a => a.Length > employeeData.MaxFileSize) || employeeImage.Any(a => a.Length > employeeData.MaxFileSize))

                {
                    throw new ArgumentException("One or more attachments exceed the maximum file size.");
                }

                var address = new Address(employeeData.Street, employeeData.City, employeeData.Country);
                var attachmentGroup = new AttachmentGroup();
                var employee = new Employee(employeeData.EmployeeName, employeeData.FirstName, employeeData.LastName, employeeData.Email,
                    employeeData.Phone, employeeData.Salary, address, attachmentGroup);

                foreach (var image in employeeImage)
                {
                    if (image != null && image.Length > 0)
                    {

                        var fileName = /*Guid.NewGuid().ToString() + "_" +*/ image.FileName;
                        var fullPath = Path.Combine("C:\\Users\\m.moien.ext\\Desktop\\attachments-frontend\\src\\assets\\Files", fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }

                        employee.SetImage(fileName);
                    }
                }

                var employeeRepository = UnitOfWork.EmployeeRepository();
                await employeeRepository.AddAsync(employee);
                await UnitOfWork.SaveChangesAsync();




                var attachmentRepository = UnitOfWork.AttachmentRepository();

                foreach (var attachmentData in attachmentDataList)
                {
                    if (attachmentData != null && attachmentData.Length > 0)
                    {
                        if (attachmentData.Length > employeeData.MaxFileSize)
                        {
                            throw new ArgumentException($"Attachment '{attachmentData.FileName}' exceeds the maximum file size.");
                        }

                        var fileName = /*Guid.NewGuid().ToString() + "_" +*/ attachmentData.FileName;
                   
                        var fullPath = Path.Combine("C:\\Users\\m.moien.ext\\Desktop\\attachments-frontend\\src\\assets\\Files", fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            attachmentData.CopyTo(stream);
                        }

                        var attachment = new Attachment(employeeData.AttachmentGroupId = attachmentGroup.Id);
                        attachment.SetFileName(attachmentData.FileName);
                        await attachmentRepository.AddAsync(attachment);

                        using (var fileStream = new FileStream(fullPath, FileMode.Open))
                        {
                            byte[] fileData = new byte[fileStream.Length];
                            fileStream.Read(fileData, 0, (int)fileStream.Length);

                           
                            attachment.SetFileData(fileData);
                            

                        }
                    }
                }

                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region download attachment
        public async Task DownloadAttachmentAsync(int employeeId)
        {
            try
            {
                var attachmentRepository = UnitOfWork.AttachmentRepository();
                var attachment = await attachmentRepository.GetByIdAsync(employeeId);



                var downloadFolder = @"C:\Users\m.moien.ext\Downloads";

                var directoryPath = Path.Combine(downloadFolder);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, attachment.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.WriteAsync(attachment.FileData, 0, attachment.FileData.Length);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion




    }
}
