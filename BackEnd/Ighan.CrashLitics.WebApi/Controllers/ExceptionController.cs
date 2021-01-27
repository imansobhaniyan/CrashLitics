using Ighan.CrashLitics.DataAccessLayer;
using Ighan.CrashLitics.WebApi.Models.Common;
using Ighan.CrashLitics.WebApi.Models.ExceptionModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        private readonly CrashLiticsDbContext dbContext;

        public ExceptionController(CrashLiticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ApiResult<ExceptionModel>> Post(ExceptionModel model)
        {
            try
            {
                var device = await dbContext.Devices.FirstOrDefaultAsync(f => f.DeviceUniqueIdentifier == model.DeviceId);
                if (device == null)
                {
                    device = new StorageModels.Device
                    {
                        DeviceUniqueIdentifier = model.DeviceId,
                        Model = await GetModelAsync(model),
                        Release = model.Release,
                        SDK = model.SDK
                    };

                    await dbContext.Devices.AddAsync(device);
                }

                device.ExceptionLogs.Add(new StorageModels.ExceptionLog
                {
                    InnerExceptionLogs = GetInnerExceptionLogs(model),
                    Message = model.Message,
                    StackTrace = model.StackTrace
                });

                await dbContext.SaveChangesAsync();

                return new ApiResult<ExceptionModel> { IsSuccess = true, Data = model };
            }
            catch (Exception exception)
            {
                return new ApiResult<ExceptionModel> { ErrorMessage = exception.Message };
            }
        }

        private List<StorageModels.InnerExceptionLog> GetInnerExceptionLogs(ExceptionModel model)
        {
            var index = 0;

            return model.InnerExceptionModels.ConvertAll(f => new StorageModels.InnerExceptionLog
            {
                Message = f.Message,
                StackTrace = f.StackTrace,
                Index = index++
            });
        }

        private async Task<StorageModels.Model> GetModelAsync(ExceptionModel model)
        {
            var dbModel = await dbContext.Models.FirstOrDefaultAsync(f => f.Name == model.Model);
            if (dbModel == null)
            {
                dbModel = new StorageModels.Model
                {
                    Name = model.Model,
                    Brand = await GetBrandAsync(model)
                };
            }
            return dbModel;
        }

        private async Task<StorageModels.Brand> GetBrandAsync(ExceptionModel model)
        {
            var dbBrand = await dbContext.Brands.FirstOrDefaultAsync(f => f.Name == model.Brand);
            if (dbBrand == null)
            {
                dbBrand = new StorageModels.Brand
                {
                    Name = model.Brand,
                    Manufacturer = await GetManufacturerAsync(model)
                };
            }
            return dbBrand;
        }

        private async Task<StorageModels.Manufacturer> GetManufacturerAsync(ExceptionModel model)
        {
            var manufacturer = await dbContext.Manufacturers.FirstOrDefaultAsync(f => f.Name == model.Manufacturer);
            if (manufacturer == null)
            {
                manufacturer = new StorageModels.Manufacturer
                {
                    Name = model.Manufacturer
                };
            }
            return manufacturer;
        }
    }
}
